using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Excel;
using Application = System.Windows.Application;
using static System.Collections.Specialized.BitVector32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using MySqlX.XDevAPI.Relational;
using System.Windows.Markup;

namespace Thermal_and_hydraulic_calculation_of_the_reactor
{
    /// <summary>
    /// Форма просмотра данных исследования сотрудником
    /// </summary>
    public partial class ShowResearch : System.Windows.Window
    {
        static int _id;
        static string title_research;
        static string status_research;
        static string date_research;
        static double value;
        static string[] temps;
        static string s;

        //Характеристики
        static string power;
        static string expenditure;
        static string temp_in;
        static string temp_out;
        static string pressure;
        static string heigth_kr;
        static string radius_kz;
        static string radius;
        static string heigth;
        static string effective;
        static string count_caset;
        static string tvel;
        static string step;
        static string thickness;
        static string V;
        static string mode;
        static string q;
        static string speed_t;
        static string c;
        static string temp;
        static string effective_h;
        static string effective_r;
        static string G0;
        static string ql0;
        static string de;
        static string a;
        static string delt;
        static string tvm;
        public ShowResearch(int id)
        {
            InitializeComponent();

            _id = id;

            Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            string host = currentConfig.AppSettings.Settings["host"].Value;
            string uid = currentConfig.AppSettings.Settings["uid"].Value;
            string pwd = currentConfig.AppSettings.Settings["pwd"].Value;
            string database = currentConfig.AppSettings.Settings["database"].Value;
            string connectionString = $"host={host};uid={uid};pwd={pwd};database={database}";

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string select_personal_data = $"SELECT CONCAT_WS(' ', last_name_user, first_name_user, middle_name_user) as FIO, DATE_FORMAT(date_birth_user, '%M %d %Y') as date, mail_user as mail FROM user WHERE id_user = (SELECT user_id_research FROM research WHERE id_research = {id});";

            MySqlCommand select_personal = new MySqlCommand(select_personal_data, connection);

            MySqlDataAdapter adapter_personal = new MySqlDataAdapter(select_personal);

            System.Data.DataTable data_personal = new System.Data.DataTable();
            adapter_personal.Fill(data_personal);

            FIO.Text = Convert.ToString(data_personal.Rows[0][0]);
            date.Text = Convert.ToString(data_personal.Rows[0][1]);
            mail_per.Text = Convert.ToString(data_personal.Rows[0][2]);

            // Получение данных исследования из базы данных

            string data_query = $"SELECT title_research, success_research, date_research FROM research WHERE id_research = {_id};";

            MySqlCommand select = new MySqlCommand(data_query, connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(select);

            System.Data.DataTable data = new System.Data.DataTable();
            adapter.Fill(data);

            string data_rc_query = $"SELECT thermal_power_rc, coolant_consumption_rc, temperature_coolant_exit_rc, temperature_coolant_entrance_rc, coolant_pressure_rc, coefficient_unevenness_heigth_rc, coefficient_unevenness_radius_rc, radius_rc, heigth_rc, effective_supplement_rc, cassettes_in_active_zone_rc, tvel_in_cassette_rc, step_cassette_arrangement_rc, cassette_wall_thickness_rc FROM reactor_characteristics INNER JOIN research ON id_rс = (SELECT reactor_characteristics_id_research FROM research WHERE id_research = {_id}) LIMIT 1;";

            MySqlCommand select_rc = new MySqlCommand(data_rc_query, connection);

            MySqlDataAdapter adapter_rc = new MySqlDataAdapter(select_rc);

            System.Data.DataTable data_rc = new System.Data.DataTable();
            adapter_rc.Fill(data_rc);

            string data_cv_query = $"SELECT reactor_core_volume_cv, mode_cv, specific_volumetric_energy_cv, velocity_coolant_core_cv, average_heat_coolant_core_cv, average_temperature_core_cv, effective_height_cv, effective_radius_cv, coolant_consumption_cv, specific_linear_center_cv, heat_transfer_coefficient_outer_cv, heat_transfer_coefficient_from_cv, temperature_difference_cv, temperature_outer_shell_cv FROM control_values INNER JOIN research ON id_cv = (SELECT control_values_id_research FROM research WHERE id_research = {_id}) LIMIT 1;";

            MySqlCommand select_cv = new MySqlCommand(data_cv_query, connection);

            MySqlDataAdapter adapter_cv = new MySqlDataAdapter(select_cv);

            System.Data.DataTable data_cv = new System.Data.DataTable();
            adapter_cv.Fill(data_cv);

            string data_tc_query = $"SELECT temperature_heat_carrier_height_tc, temperature_values_tc, temperature_values_tvel_tc FROM temperature_charts INNER JOIN research ON id_tc = (SELECT temperature_charts_id_research FROM research WHERE id_research = {_id}) LIMIT 1;";

            MySqlCommand select_tc = new MySqlCommand(data_tc_query, connection);

            MySqlDataAdapter adapter_tc = new MySqlDataAdapter(select_tc);

            System.Data.DataTable data_tc = new System.Data.DataTable();
            adapter_tc.Fill(data_tc);

            System.Data.DataTable data_values = new System.Data.DataTable();
            data_values.Columns.Add("Название", typeof(string));
            data_values.Columns.Add("Значение", typeof(string));
            data_values.Rows.Add("Тепловая мощность реактора", Convert.ToString(data_rc.Rows[0][0]) + " МВт");
            data_values.Rows.Add("Расход теплоносителя через активную зону", Convert.ToString(data_rc.Rows[0][1]) + " кг/с");
            data_values.Rows.Add("Темп. на входе в активную зону", Convert.ToString(data_rc.Rows[0][2]) + " °C");
            data_values.Rows.Add("Темп. на выходе из активной зоны", Convert.ToString(data_rc.Rows[0][3]) + " °C");
            data_values.Rows.Add("Давление теплоносителя", Convert.ToString(data_rc.Rows[0][4]) + " МПа");
            data_values.Rows.Add("Коэфф. энерговыделения (высота)", Convert.ToString(data_rc.Rows[0][5]));
            data_values.Rows.Add("Коэфф. энерговыделения (радиус)", Convert.ToString(data_rc.Rows[0][6]));
            data_values.Rows.Add("Радиус", Convert.ToString(data_rc.Rows[0][7]) + " м");
            data_values.Rows.Add("Высота", Convert.ToString(data_rc.Rows[0][8]) + " м");
            data_values.Rows.Add("Эффективная добавка", Convert.ToString(data_rc.Rows[0][9]) + " м");
            data_values.Rows.Add("Количество кассет в активной зоне", Convert.ToString(data_rc.Rows[0][10]) + " шт");
            data_values.Rows.Add("Количество ТВЭЛ в кассете", Convert.ToString(data_rc.Rows[0][11]) + " шт");
            data_values.Rows.Add("Шаг расположения кассет", Convert.ToString(data_rc.Rows[0][12]) + " м");
            data_values.Rows.Add("Толщина стенки кассеты", Convert.ToString(data_rc.Rows[0][13]) + " м");
            data_values.Rows.Add("Объем активной зоны реактора", Convert.ToString(data_cv.Rows[0][0]) + " м³");
            data_values.Rows.Add("Режим работы реактора", Convert.ToString(data_cv.Rows[0][1]));
            data_values.Rows.Add("Удельное объемное энерговыделение", Convert.ToString(data_cv.Rows[0][2]) + " МВт/м³");
            data_values.Rows.Add("Скорость теплоносителя в активной зоне", Convert.ToString(data_cv.Rows[0][3]) + " м/с");
            data_values.Rows.Add("Средняя теплоемкость теплоносителя", Convert.ToString(data_cv.Rows[0][4]) + " кДж/кг·град");
            data_values.Rows.Add("Средняя температура", Convert.ToString(data_cv.Rows[0][5]) + " °C");
            data_values.Rows.Add("Эффективная высота", Convert.ToString(data_cv.Rows[0][6]) + " м");
            data_values.Rows.Add("Эффективный радиус", Convert.ToString(data_cv.Rows[0][7]) + " м");
            data_values.Rows.Add("Расход теплоносителя", Convert.ToString(data_cv.Rows[0][8]) + " кг/с");
            data_values.Rows.Add("Удельный линейный тепловой поток", Convert.ToString(data_cv.Rows[0][9]) + "  кВт/м");
            data_values.Rows.Add("Коэффициент теплоотдачи", Convert.ToString(data_cv.Rows[0][10]));
            data_values.Rows.Add("Коэффициент теплоотдачи от оболочки ТВЭЛ", Convert.ToString(data_cv.Rows[0][11]));
            data_values.Rows.Add("Температурный перепад", Convert.ToString(data_cv.Rows[0][12]) + " °C");
            data_values.Rows.Add("Максимальная температура оболочки ТВЭЛ", Convert.ToString(data_cv.Rows[0][13]) + " °C");

            //Задание характеристик
            power = Convert.ToString(data_rc.Rows[0][0]);
            expenditure = Convert.ToString(data_rc.Rows[0][1]);
            temp_in = Convert.ToString(data_rc.Rows[0][2]);
            temp_out = Convert.ToString(data_rc.Rows[0][3]);
            pressure = Convert.ToString(data_rc.Rows[0][4]);
            heigth_kr = Convert.ToString(data_rc.Rows[0][5]);
            radius_kz = Convert.ToString(data_rc.Rows[0][6]);
            radius = Convert.ToString(data_rc.Rows[0][7]);
            heigth = Convert.ToString(data_rc.Rows[0][8]);
            effective = Convert.ToString(data_rc.Rows[0][9]);
            count_caset = Convert.ToString(data_rc.Rows[0][10]);
            tvel = Convert.ToString(data_rc.Rows[0][11]);
            step = Convert.ToString(data_rc.Rows[0][12]);
            thickness = Convert.ToString(data_rc.Rows[0][13]);
            V = Convert.ToString(data_cv.Rows[0][0]);
            mode = Convert.ToString(data_cv.Rows[0][1]);
            q = Convert.ToString(data_cv.Rows[0][2]);
            speed_t = Convert.ToString(data_cv.Rows[0][3]);
            c = Convert.ToString(data_cv.Rows[0][4]);
            temp = Convert.ToString(data_cv.Rows[0][5]);
            effective_h = Convert.ToString(data_cv.Rows[0][6]);
            effective_r = Convert.ToString(data_cv.Rows[0][7]);
            G0 = Convert.ToString(data_cv.Rows[0][8]);
            ql0 = Convert.ToString(data_cv.Rows[0][9]);
            de = Convert.ToString(data_cv.Rows[0][10]);
            a = Convert.ToString(data_cv.Rows[0][11]);
            delt = Convert.ToString(data_cv.Rows[0][12]);
            tvm = Convert.ToString(data_cv.Rows[0][13]);

            DataResearch.ItemsSource = data_values.DefaultView;

            title_research = Convert.ToString(data.Rows[0][0]);
            title.Text = title_research;

            date_research = Convert.ToString(data.Rows[0][2]);

            if (Convert.ToString(data.Rows[0][1]) == "Провальное")
            {
                status.Text = "Провальное";
                status.Foreground = new SolidColorBrush(Colors.IndianRed);
            }

            status_research = Convert.ToString(data.Rows[0][1]);

            value = Convert.ToDouble(data_tc.Rows[0][0]);
            temps = (Convert.ToString(data_tc.Rows[0][1]).Replace(".", ",")).Split(' ');

            //График температур
            chart1.ChartAreas.Add(new System.Windows.Forms.DataVisualization.Charting.ChartArea("Default"));

            chart1.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series("Series1"));
            chart1.Series["Series1"].ChartArea = "Default";
            chart1.Series["Series1"].ChartType = SeriesChartType.Spline;
            chart1.Series["Series1"].BorderWidth = 3;
            chart1.BackColor = System.Drawing.Color.LightGray;
            chart1.ChartAreas[0].AxisY.Title = "Температура";
            chart1.ChartAreas[0].AxisX.Title = "Высота";
            chart1.ChartAreas[0].AxisY.Minimum = 250;
            chart1.ChartAreas[0].AxisY.Maximum = Math.Round((Convert.ToDouble(temps[2]) - (Convert.ToDouble(temps[2]) % 100)) + 100);
            chart1.Titles.Add("Распределение температуры теплоносителя по высоте самого энергонапряжённого канала");
            chart1.Series["Series1"].Color = System.Drawing.Color.SkyBlue;

            double[] axisXData1 = new double[] { -value / 2, -value / 4, 0, value / 4, value / 2 };
            double[] axisYData1 = new double[] { Convert.ToDouble(temps[0]), Convert.ToDouble(temps[1]), Convert.ToDouble(temps[2]), Convert.ToDouble(temps[3]), Convert.ToDouble(temps[4]) };
            chart1.Series["Series1"].Points.DataBindXY(axisXData1, axisYData1);
            chart1.Series["Series1"].MarkerStyle = MarkerStyle.Circle;
            chart1.Series["Series1"].IsValueShownAsLabel = true;

            double temp_tvel_max = Convert.ToInt32(data_rc.Rows[0][2]) + ((Convert.ToInt32(data_rc.Rows[0][3]) - Convert.ToInt32(data_rc.Rows[0][2])) / 2) + Convert.ToDouble(data_cv.Rows[0][12]) * Math.Sqrt(1 + Math.Pow(((Convert.ToInt32(data_rc.Rows[0][3]) - Convert.ToInt32(data_rc.Rows[0][2])) / 2) / (2 * Convert.ToDouble(data_cv.Rows[0][12])), 2) * Math.Pow(Math.Sin((Math.PI * Convert.ToDouble(data_rc.Rows[0][8])) / (2 * Convert.ToDouble(data_cv.Rows[0][6]))), -2));
            temp_tvel_max *= 1.01;
            temp_tvel_max = Math.Round(temp_tvel_max, 1);

            // График температур ТВЭЛ
            chart2.ChartAreas.Add(new System.Windows.Forms.DataVisualization.Charting.ChartArea("Default"));

            chart2.Series.Add(new System.Windows.Forms.DataVisualization.Charting.Series("Series1"));
            chart2.Series["Series1"].ChartArea = "Default";
            chart2.Series["Series1"].ChartType = SeriesChartType.Spline;
            chart2.BackColor = System.Drawing.Color.LightGray;
            chart2.Series["Series1"].BorderWidth = 3;
            chart2.ChartAreas[0].AxisY.Title = "Температура";
            chart2.ChartAreas[0].AxisX.Title = "Высота";
            chart2.ChartAreas[0].AxisY.Minimum = 250;
            if (temp_tvel_max > 1000)
                chart2.ChartAreas[0].AxisY.Maximum = Math.Round((temp_tvel_max - (temp_tvel_max % 1000)) + 1000);
            else
                chart2.ChartAreas[0].AxisY.Maximum = Math.Round((temp_tvel_max - (temp_tvel_max % 100)) + 100);
            chart2.Titles.Add("Распределение температуры наружной поверхности оболочки ТВЭЛ по высоте самого энергонапряжённого канала");
            chart2.Series["Series1"].Color = System.Drawing.Color.SkyBlue;

            string[] temps_tvel = (Convert.ToString(data_tc.Rows[0][2]).Replace(".", ",")).Split(' ');

            double z_max = Math.Round((Convert.ToDouble(data_cv.Rows[0][6]) / Math.PI) * (Math.Atan((Convert.ToInt32(data_rc.Rows[0][3]) - Convert.ToInt32(data_rc.Rows[0][2])) / (2 * Math.Sin((Math.PI * Convert.ToDouble(data_rc.Rows[0][8])) / (2 * Convert.ToDouble(data_cv.Rows[0][6]))) * Convert.ToDouble(data_cv.Rows[0][12])))), 3);

            double[] axisXData2 = new double[] { -value / 2, -value / 4, 0, z_max, value / 4, value / 2 };
            double[] axisYData2 = new double[] { Convert.ToDouble(temps_tvel[0]), Convert.ToDouble(temps_tvel[1]), Convert.ToDouble(temps_tvel[2]), temp_tvel_max, Convert.ToDouble(temps_tvel[3]), Convert.ToDouble(temps_tvel[4]) };
            chart2.Series["Series1"].Points.DataBindXY(axisXData2, axisYData2);
            chart2.Series["Series1"].MarkerStyle = MarkerStyle.Circle;
            chart2.Series["Series1"].IsValueShownAsLabel = true;

            s = $"В результате теплогидравлического расчета активной зоны корпусного ядерного реактора, работающего в режиме '{mode}' выяснилось: температура оболочки ТВЭЛ ({tvm}) не первышает 350 °C (допустимую для циркония), геометрические характеристики удовлетворяют допустимым, параметры теплоносителя не экстремальны.";

            if (status_research == "Провальное")
                s = $"В результате теплогидравлического расчета активной зоны корпусного ядерного реактора, работающего в режиме '{mode}' выяснилось: температура оболочки ТВЭЛ ({tvm}) первышает 350 °C (допустимую для циркония), геометрические характеристики не удовлетворяют допустимым, параметры теплоносителя экстремальны.";
        }
        /// <summary>
        /// Возвращение на форму "Просмотр исследований сотрудником"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (System.InvalidOperationException)
            {
            }
        }
        /// <summary>
        /// Создание отчета в Word
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void report_word_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = DateTime.Now;

            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();

            object missing = System.Reflection.Missing.Value;

            Microsoft.Office.Interop.Word.Document document = wordApp.Documents.Add(ref missing, ref missing, ref missing, ref missing);

            Microsoft.Office.Interop.Word.Paragraph report = document.Paragraphs.Add();
            report.Range.Text = "ОТЧЕТ ПО ТЕПЛОГИДРАВЛИЧЕСКОМУ РАСЧЕТУ";
            report.Range.Font.Size = 14;
            report.Range.Font.Bold = 1;
            report.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
            report.Range.InsertParagraphAfter();

            Microsoft.Office.Interop.Word.Paragraph date_report = document.Paragraphs.Add();
            date_report.Range.Text = "Дата отчета: " + date.ToString("MM.dd.yyyy HH:mm:ss");
            date_report.Range.Font.Size = 12;
            date_report.Range.Font.Bold = 0;
            date_report.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
            date_report.Range.InsertParagraphAfter();

            Microsoft.Office.Interop.Word.Paragraph research_title = document.Paragraphs.Add();
            research_title.Range.Text = "Название исследования: " + title_research;
            research_title.Range.Font.Size = 12;
            research_title.Range.Font.Bold = 0;
            research_title.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
            research_title.Range.InsertParagraphAfter();

            Microsoft.Office.Interop.Word.Paragraph research_status = document.Paragraphs.Add();
            research_status.Range.Text = "Статус исследования: " + status_research;
            research_status.Range.Font.Size = 12;
            research_status.Range.Font.Bold = 0;
            research_status.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
            research_status.Range.InsertParagraphAfter();

            Microsoft.Office.Interop.Word.Paragraph research_date = document.Paragraphs.Add();
            research_date.Range.Text = "Дата исследования: " + date_research;
            research_date.Range.Font.Size = 12;
            research_date.Range.Font.Bold = 0;
            research_date.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
            research_date.Range.InsertParagraphAfter();

            Microsoft.Office.Interop.Word.Paragraph char_title = document.Paragraphs.Add();
            char_title.Range.Text = "Теплогидравлические характеристики:";
            char_title.Range.Font.Size = 12;
            char_title.Range.Font.Bold = 0;
            char_title.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
            char_title.Range.InsertParagraphAfter();

            Microsoft.Office.Interop.Word.Table table = document.Tables.Add(document.Paragraphs[7].Range, 29, 2);
            table.Range.Font.Size = 10;
            table.Borders.Enable = 1;
            table.Cell(1, 1).Range.Text = "Название";
            table.Cell(1, 2).Range.Text = "Значение";
            table.Cell(2, 1).Range.Text = "Тепловая мощность реактора";
            table.Cell(2, 2).Range.Text = power + " МВт";
            table.Cell(3, 1).Range.Text = "Расход теплоносителя через активную зону";
            table.Cell(3, 2).Range.Text = expenditure + " кг/с";
            table.Cell(4, 1).Range.Text = "Темп. на входе в активную зону";
            table.Cell(4, 2).Range.Text = temp_in + " °C";
            table.Cell(5, 1).Range.Text = "Темп. на выходе из активной зоны";
            table.Cell(5, 2).Range.Text = temp_out + " °C";
            table.Cell(6, 1).Range.Text = "Давление теплоносителя";
            table.Cell(6, 2).Range.Text = pressure + " МПа";
            table.Cell(7, 1).Range.Text = "Коэфф. энерговыделения (высота)";
            table.Cell(7, 2).Range.Text = heigth_kr;
            table.Cell(8, 1).Range.Text = "Коэфф. энерговыделения (радиус)";
            table.Cell(8, 2).Range.Text = radius_kz;
            table.Cell(9, 1).Range.Text = "Радиус";
            table.Cell(9, 2).Range.Text = radius;
            table.Cell(10, 1).Range.Text = "Высота";
            table.Cell(10, 2).Range.Text = heigth;
            table.Cell(11, 1).Range.Text = "Эффективная добавка";
            table.Cell(11, 2).Range.Text = effective + " м";
            table.Cell(12, 1).Range.Text = "Количество кассет в активной зоне";
            table.Cell(12, 2).Range.Text = count_caset + " шт";
            table.Cell(13, 1).Range.Text = "Количество ТВЭЛ в кассете";
            table.Cell(13, 2).Range.Text = tvel + " шт";
            table.Cell(14, 1).Range.Text = "Шаг расположения кассет";
            table.Cell(14, 2).Range.Text = step + " м";
            table.Cell(15, 1).Range.Text = "Толщина стенки кассеты";
            table.Cell(15, 2).Range.Text = thickness + " м";
            table.Cell(16, 1).Range.Text = "Объем активной зоны реактора";
            table.Cell(16, 2).Range.Text = V + " м³";
            table.Cell(17, 1).Range.Text = "Режим работы реактора";
            table.Cell(17, 2).Range.Text = mode;
            table.Cell(18, 1).Range.Text = "Удельное объемное энерговыделение";
            table.Cell(18, 2).Range.Text = q + " МВт/м³";
            table.Cell(19, 1).Range.Text = "Скорость теплоносителя в активной зоне";
            table.Cell(19, 2).Range.Text = speed_t + " м/с";
            table.Cell(20, 1).Range.Text = "Средняя теплоемкость теплоносителя";
            table.Cell(20, 2).Range.Text = c + " кДж/кг·град";
            table.Cell(21, 1).Range.Text = "Средняя температура";
            table.Cell(21, 2).Range.Text = temp + " °C";
            table.Cell(22, 1).Range.Text = "Эффективная высота";
            table.Cell(22, 2).Range.Text = effective_h + " м";
            table.Cell(23, 1).Range.Text = "Эффективный радиус";
            table.Cell(23, 2).Range.Text = effective_r + " м";
            table.Cell(24, 1).Range.Text = "Расход теплоносителя";
            table.Cell(24, 2).Range.Text = G0 + " кг/с";
            table.Cell(25, 1).Range.Text = "Удельный линейный тепловой поток";
            table.Cell(25, 2).Range.Text = ql0 + " кВт/м";
            table.Cell(26, 1).Range.Text = "Коэффициент теплоотдачи";
            table.Cell(26, 2).Range.Text = de;
            table.Cell(27, 1).Range.Text = "Коэффициент теплоотдачи от оболочки ТВЭЛ";
            table.Cell(27, 2).Range.Text = a;
            table.Cell(28, 1).Range.Text = "Температурный перепад";
            table.Cell(28, 2).Range.Text = delt + " °C";
            table.Cell(29, 1).Range.Text = "Максимальная температура оболочки ТВЭЛ";
            table.Cell(29, 2).Range.Text = tvm + " °C";
            table.Range.InsertParagraphAfter();

            Microsoft.Office.Interop.Word.Paragraph temps_k = document.Paragraphs.Add();
            temps_k.Range.Text = s;
            temps_k.Range.Font.Size = 12;
            temps_k.Range.Font.Bold = 0;
            temps_k.Range.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphLeft;
            temps_k.Range.InsertParagraphAfter();

            wordApp.Visible = true;
        }
        /// <summary>
        /// Создание отчета в Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void report_excel_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelApp.Visible = true;
            excelApp.WindowState = XlWindowState.xlMaximized;

            Workbook wb = excelApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = wb.Worksheets[1];
            DateTime currentDate = DateTime.Now;

            ws.Range["A1"].Value = "ОТЧЕТ ПО ТЕПЛОГИДРАВЛИЧЕСКОМУ РАСЧЕТУ";

            ws.Range["A2"].Value = "Дата отчета:";
            ws.Range["B2"].Value = currentDate;

            ws.Range["A4"].Value = "Название исследования:";
            ws.Range["B4"].Value = title_research;

            ws.Range["A5"].Value = "Статус исследования:";
            ws.Range["B5"].Value = status_research;

            ws.Range["A6"].Value = "Дата исследования:";
            ws.Range["B6"].Value = date_research;

            ws.Range["A8"].Value = "Теплогидравлические характеристики:";

            ws.Range["A9"].Value = "Тепловая мощность реактора";
            ws.Range["B9"].Value = power + " МВт";
            ws.Range["A10"].Value = "Расход теплоносителя через активную зону";
            ws.Range["B10"].Value = expenditure + " кг/с";
            ws.Range["A11"].Value = "Темп. на входе в активную зону";
            ws.Range["B11"].Value = temp_in + " °C";
            ws.Range["A12"].Value = "Темп. на выходе из активной зоны";
            ws.Range["B12"].Value = temp_out + " °C";
            ws.Range["A13"].Value = "Давление теплоносителя";
            ws.Range["B13"].Value = pressure + " МПа";
            ws.Range["A14"].Value = "Коэфф. энерговыделения (высота)";
            ws.Range["B14"].Value = heigth_kr;
            ws.Range["A15"].Value = "Коэфф. энерговыделения (радиус)";
            ws.Range["B15"].Value = radius_kz;
            ws.Range["A16"].Value = "Радиус";
            ws.Range["B16"].Value = radius;
            ws.Range["A17"].Value = "Высота";
            ws.Range["B17"].Value = heigth;
            ws.Range["A18"].Value = "Эффективная добавка";
            ws.Range["B18"].Value = effective + " м";
            ws.Range["A19"].Value = "Количество кассет в активной зоне";
            ws.Range["B19"].Value = count_caset + " шт";
            ws.Range["A20"].Value = "Количество ТВЭЛ в кассете";
            ws.Range["B20"].Value = tvel + " шт";
            ws.Range["A21"].Value = "Шаг расположения кассет";
            ws.Range["B21"].Value = step + " м";
            ws.Range["A22"].Value = "Толщина стенки кассеты";
            ws.Range["B22"].Value = thickness + " м";
            ws.Range["A23"].Value = "Объем активной зоны реактора";
            ws.Range["B23"].Value = V + " м³";
            ws.Range["A24"].Value = "Режим работы реактора";
            ws.Range["B24"].Value = mode;
            ws.Range["A25"].Value = "Удельное объемное энерговыделение";
            ws.Range["B25"].Value = q + " МВт/м³";
            ws.Range["A26"].Value = "Скорость теплоносителя в активной зоне";
            ws.Range["B26"].Value = speed_t + " м/с";
            ws.Range["A27"].Value = "Средняя теплоемкость теплоносителя";
            ws.Range["B27"].Value = c + " кДж/кг·град";
            ws.Range["A28"].Value = "Средняя температура";
            ws.Range["B28"].Value = temp + " °C";
            ws.Range["A29"].Value = "Эффективная высота";
            ws.Range["B29"].Value = effective_h + " м";
            ws.Range["A30"].Value = "Эффективный радиус";
            ws.Range["B30"].Value = effective_r + " м";
            ws.Range["A31"].Value = "Расход теплоносителя";
            ws.Range["B31"].Value = G0 + " кг/с";
            ws.Range["A32"].Value = "Удельный линейный тепловой поток";
            ws.Range["B32"].Value = ql0 + " кВт/м";
            ws.Range["A33"].Value = "Коэффициент теплоотдачи";
            ws.Range["B33"].Value = de + " ";
            ws.Range["A34"].Value = "Коэффициент теплоотдачи от оболочки ТВЭЛ";
            ws.Range["B34"].Value = a;
            ws.Range["A35"].Value = "Температурный перепад";
            ws.Range["B35"].Value = delt + " °C";
            ws.Range["A36"].Value = "Максимальная температура оболочки ТВЭЛ";
            ws.Range["B36"].Value = tvm + " °C";

            ws.Range["A38"].Value = s;

            var range = ws.Range["A38", System.Type.Missing];
            range.EntireColumn.ColumnWidth = 42;

            range.WrapText = true;

            range = ws.Range["B4", System.Type.Missing];
            range.EntireColumn.ColumnWidth = 25;

            range.WrapText = true;

            var range_border = ws.Range["A9:B36"];

            range_border.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            range_border.Borders.Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
        }
        /// <summary>
        /// Обработка закрытия формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
