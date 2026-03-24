using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace Thermal_and_hydraulic_calculation_of_the_reactor
{
    /// <summary>
    /// Форма просмотра детальных данных исследования администратором
    /// </summary>
    public partial class ShowResearchAdmin : Window
    {
        static int _id;
        static string title_research;
        static string status_research;
        static string date_research;
        static double value;
        static string[] temps;

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
        public ShowResearchAdmin(int id)
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

            // Получение детальных данных исследования из базы данных

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
        }
        /// <summary>
        /// Возвращение на форму "Просмотр исследований администратором"
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
