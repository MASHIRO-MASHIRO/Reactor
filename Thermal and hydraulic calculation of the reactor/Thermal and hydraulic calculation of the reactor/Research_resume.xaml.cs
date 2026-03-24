using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;
using System.Windows.Threading;
using System.Windows.Interop;

namespace Thermal_and_hydraulic_calculation_of_the_reactor
{
    /// <summary>
    /// Форма завершения исследования
    /// </summary>
    public partial class Research_resume : Window
    {
        System.Windows.Threading.DispatcherTimer _timer;
        TimeSpan _time;

        double ql0;
        double effective_h;
        double G0;
        double c;
        double delt;

        public Regex regex = new Regex(@"[^а-яА-Яa-zA-Z0-9]\._%");

        static Research_caset _page1;
        static Research_energy_characteristics _page2;
        static private string _login;

        static int count_caset = 1;
        static int tvel;
        static double radius;
        static double heigth;
        static double effective;
        static double step;
        static double thickness;
        static int power;
        static int temp_in;
        static int temp_out;
        static int expenditure;
        static double pressure;
        static double radius_kz;
        static double heigth_kr;
        void ComponentDispatcher_ThreadIdle(object sender, EventArgs e)
        {
            if (_time.TotalSeconds == 0)
            {
                _timer.Stop();
                _time = _time.Add(TimeSpan.FromSeconds(-1));
                foreach (System.Windows.Window w in App.Current.Windows)
                {
                    if (w != this)
                        w.Hide();
                }
                Authorization page = new Authorization();
                this.Close();
                page.ShowDialog();
            }

        }
        public Research_resume(Research_caset page1, Research_energy_characteristics page2, int count_caset_value, int tvel_value, double radius_value, double heigth_value, double effective_value, double step_value, double thickness_value, int power_value, int temp_in_value, int temp_out_value, int expenditure_value, double pressure_value, double radius_kz_value, double heigth_kr_value, string login)
        {
            _page1 = page1;
            _page2 = page2;

            _login = login;

            if(count_caset_value != 0)
                count_caset = count_caset_value;
            
            tvel = tvel_value;
            radius = radius_value;
            heigth = heigth_value;
            effective = effective_value;
            step = step_value;
            thickness = thickness_value;
            power = power_value;
            temp_in = temp_in_value;
            temp_out = temp_out_value;
            expenditure = expenditure_value;
            pressure = pressure_value;
            radius_kz = radius_kz_value;
            heigth_kr = heigth_kr_value;
            
            InitializeComponent();

            _time = TimeSpan.FromSeconds(30);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                if (_time == TimeSpan.Zero) _timer.Stop();
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();

            ComponentDispatcher.ThreadIdle += new System.EventHandler(ComponentDispatcher_ThreadIdle);

            Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            string host = currentConfig.AppSettings.Settings["host"].Value;
            string uid = currentConfig.AppSettings.Settings["uid"].Value;
            string pwd = currentConfig.AppSettings.Settings["pwd"].Value;
            string database = currentConfig.AppSettings.Settings["database"].Value;
            string connectionString = $"host={host};uid={uid};pwd={pwd};database={database}";

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            // Получение данных исследования из базы данных

            string query_count_strings = $"SELECT COUNT(*) FROM research;";

            MySqlCommand command = new MySqlCommand(query_count_strings, connection);

            int count = int.Parse(command.ExecuteScalar().ToString());

            title.Text = $"Исследование {count + 1} ({login})";

            Dictionary<int, double> Kwater = new Dictionary<int, double>();
            Kwater.Add(0, 1.78);
            Kwater.Add(20, 1.006);
            Kwater.Add(40, 0.65);
            Kwater.Add(60, 0.47);
            Kwater.Add(80, 0.36);
            Kwater.Add(100, 0.29);
            Kwater.Add(120, 0.25);
            Kwater.Add(140, 0.21);
            Kwater.Add(160, 0.19);
            Kwater.Add(180, 0.17);
            Kwater.Add(200, 0.15);
            Kwater.Add(220, 0.14);
            Kwater.Add(240, 0.14);
            Kwater.Add(260, 0.13);
            Kwater.Add(280, 0.13);
            Kwater.Add(300, 0.12);

            Dictionary<int, double> Dwater = new Dictionary<int, double>();
            Dwater.Add(0, 1788);
            Dwater.Add(20, 1004);
            Dwater.Add(40, 653.3);
            Dwater.Add(60, 469.9);
            Dwater.Add(80, 355.1);
            Dwater.Add(100, 282.5);
            Dwater.Add(120, 237.4);
            Dwater.Add(140, 201.1);
            Dwater.Add(160, 173.6);
            Dwater.Add(180, 153.0);
            Dwater.Add(200, 136.4);
            Dwater.Add(220, 124.6);
            Dwater.Add(240, 114.8);
            Dwater.Add(260, 105.9);
            Dwater.Add(280, 98.1);
            Dwater.Add(300, 91.2);

            try
            {
                // Объем активной зоны реактора +
                double V = Math.PI * Math.Pow(radius, 2) * heigth;
                V = Math.Round(V, 2);

                // Удельное объемное энерговыделение +
                double q = power / V;
                q = Math.Round(q, 2);

                // Скорость теплоносителя в активной зоне +
                double Fm = count_caset * 6 * ((0.5 * 0.238) / Math.Cos(Math.PI * 30 / 180)) * ((step - 0.238) / 2);
                Fm = Math.Round(Fm, 6);
                double Fc = 6 * (Math.Sqrt(3) / 4) * Math.Pow((0.5 * 0.238) / Math.Cos(Math.PI * 30 / 180), 2);
                Fc = Math.Round(Fc, 6);
                double Fctk = 6 * ((0.5 * 0.238) / Math.Cos(Math.PI * 30 / 180)) * thickness;
                Fctk = Math.Round(Fctk, 6);
                double Ftc = tvel * ((Math.PI * Math.Pow(0.0091, 2)) / 4);
                Ftc = Math.Round(Ftc, 6);
                double Fctc = (Math.PI * Math.Pow(0.0103, 2)) / 4;
                Fctc = Math.Round(Fctc, 2);
                double Fk = 13 * ((Math.PI * Math.Pow(0.0126, 2)) / 4);
                Fk = Math.Round(Fk, 6);
                double F = Fc - Fctk - Ftc - Fctc - Fk;
                F = Math.Round(F, 6);
                double speed_t = expenditure / (716.9 * (Fm + (count_caset * F)));
                speed_t = Math.Round(speed_t, 2);

                // Средняя температура +
                double temp = (temp_in + temp_out) / 2;
                temp = Math.Round(temp, 0);

                // Средняя теплоемкость теплоносителя в активной зоне +
                double c = (pressure * 107) / temp;
                c = Math.Round(c, 2);

                // Эффективная высота +
                effective_h = heigth + (2 * effective);
                effective_h = Math.Round(effective_h, 3);

                // Эффективный радиус +
                double effective_r = radius + effective;
                effective_r = Math.Round(effective_r, 3);

                // Расход теплоносителя через самый энергонапряженный канал +
                double G0 = (Convert.ToDouble(expenditure) / Convert.ToDouble((count_caset * (tvel + 14)))) * radius_kz;
                G0 = Math.Round(G0, 3);

                // Удельный линейный тепловой поток в центре
                double ql0 = ((power * 1000) / (count_caset * tvel * heigth)) * heigth_kr * radius_kz;
                ql0 = Math.Round(ql0, 5);

                // Температура теплоносителя по высоте самого энергонапряженного канала
                double fz = temp_in + (((ql0 * effective_h) / (G0 * c * Math.PI)) * (Math.Sin((Math.PI * c) / effective_h) + Math.Sin((Math.PI * heigth) / (2 * effective_h))));

                // Коэффициент теплоотдачи от наружной поверхности оболочки ТВЭЛ к теплоносителю
                double de = Math.Sqrt((4 * Math.Pow(radius, 2)) / (count_caset * 331));
                de = Math.Round(de, 5);

                // Динамическая вязкость
                double D = 0;
                foreach (KeyValuePair<int, double> element in Dwater.Reverse())
                {
                    if (temp == element.Key)
                    { D = element.Value; D *= Math.Pow(10, -6); break; }
                    if (Math.Abs(temp - element.Key) < 20)
                    { D = element.Value; D *= Math.Pow(10, -6); break; }
                }

                // Кинематическая вязкость
                double K = 0;
                foreach (KeyValuePair<int, double> element in Kwater.Reverse())
                {
                    if (temp == element.Key)
                    { K = element.Value; break; }
                    if (Math.Abs(temp - element.Key) < 20)
                    { K = element.Value; break; }
                }
            ;
                K *= Math.Pow(10, -6);

                // Число Прандтля +
                double P = ((c * D) / 0.58) * 1000;
                P = Math.Round(P, 4);

                // Число Рейнольдса +
                double R = (speed_t * de) / K;
                R = Math.Round(R, 0);

                string mode = "Ламинарный";

                if (R > Math.Pow(10, 5))
                    mode = "Тарбулентный";

                // Коэффициент теплоотдачи от оболочки ТВЭЛ к теплоносителю +
                double a = 0.021 * (0.55 / de) * Math.Pow(R, 0.8) * Math.Pow(P, 0.43);
                a = Math.Round(a, 0);

                // Температурный перепад
                delt = (0.93 * power * 1000 * heigth_kr * radius_kz) / (Math.PI * 0.0091 * a * Math.Pow(10, -3) * count_caset * tvel * heigth);
                delt = Math.Round(delt, 1);

                // Графики
                // Значения высоты z
                double z0 = -heigth / 2;
                double z1 = -heigth / 4;
                double z2 = 0;
                double z3 = heigth / 4;
                double z4 = heigth / 2;

                // Значения температур для z
                double f1 = Math.Round(temp_in + (((power * 1000 * heigth_kr * effective_h) / (expenditure * heigth * c * Math.PI)) * (Math.Sin((Math.PI * z0) / effective_h) + Math.Sin((Math.PI * heigth) / (2 * effective_h)))), 1);
                double f2 = Math.Round(temp_in + (((power * 1000 * heigth_kr * effective_h) / (expenditure * heigth * c * Math.PI)) * (Math.Sin((Math.PI * z1) / effective_h) + Math.Sin((Math.PI * heigth) / (2 * effective_h)))), 1);
                double f3 = Math.Round(temp_in + (((power * 1000 * heigth_kr * effective_h) / (expenditure * heigth * c * Math.PI)) * (Math.Sin((Math.PI * z2) / effective_h) + Math.Sin((Math.PI * heigth) / (2 * effective_h)))), 1);
                double f4 = Math.Round(temp_in + (((power * 1000 * heigth_kr * effective_h) / (expenditure * heigth * c * Math.PI)) * (Math.Sin((Math.PI * z3) / effective_h) + Math.Sin((Math.PI * heigth) / (2 * effective_h)))), 1);
                double f5 = Math.Round(temp_in + (((power * 1000 * heigth_kr * effective_h) / (expenditure * heigth * c * Math.PI)) * (Math.Sin((Math.PI * z4) / effective_h) + Math.Sin((Math.PI * heigth) / (2 * effective_h)))), 1);

                // Температура наружной поверхности оболочки ТВЭЛ
                double temp_tvel1 = Math.Round(f1 + (delt * Math.Cos((Math.PI * z0) / effective_h)), 0);
                double temp_tvel2 = Math.Round(f2 + (delt * Math.Cos((Math.PI * z1) / effective_h)), 0);
                double temp_tvel3 = Math.Round(f3 + (delt * Math.Cos((Math.PI * z2) / effective_h)), 0);
                double temp_tvel4 = Math.Round(f4 + (delt * Math.Cos((Math.PI * z3) / effective_h)), 0);
                double temp_tvel5 = Math.Round(f5 + (delt * Math.Cos((Math.PI * z4) / effective_h)), 0);

                // График температур
                chart1.ChartAreas.Add(new ChartArea("Default"));

                chart1.Series.Add(new Series("Series1"));
                chart1.Series["Series1"].ChartArea = "Default";
                chart1.Series["Series1"].ChartType = SeriesChartType.Spline;
                chart1.Series["Series1"].BorderWidth = 3;
                chart1.BackColor = System.Drawing.Color.LightGray;
                chart1.ChartAreas[0].AxisY.Title = "Температура";
                chart1.ChartAreas[0].AxisX.Title = "Высота";
                chart1.ChartAreas[0].AxisY.Minimum = 250;
                chart1.ChartAreas[0].AxisY.Maximum = Math.Round((f3 - (f3 % 100)) + 100);
                chart1.Titles.Add("Распределение температуры теплоносителя по высоте самого энергонапряжённого канала");
                chart1.Series["Series1"].Color = System.Drawing.Color.SkyBlue;

                double[] axisXData1 = new double[] { z0, z1, z2, z3, z4 };
                double[] axisYData1 = new double[] { f1, f2, f3, f4, f5 };
                chart1.Series["Series1"].Points.DataBindXY(axisXData1, axisYData1);
                chart1.Series["Series1"].MarkerStyle = MarkerStyle.Circle;
                chart1.Series["Series1"].IsValueShownAsLabel = true;

                // Коорддината максимальной точки
                double z_max = Math.Round((effective_h / Math.PI) * (Math.Atan((temp_out - temp_in) / (2 * Math.Sin((Math.PI * heigth) / (2 * effective_h)) * delt))), 3);

                // Максимальная температура поверхности оболочки ТВЭЛ
                double temp_tvel_max = temp_in + ((temp_out - temp_in) / 2) + delt * Math.Sqrt(1 + Math.Pow(((temp_out - temp_in) / 2) / (2 * delt), 2) * Math.Pow(Math.Sin((Math.PI * heigth) / (2 * effective_h)), -2));
                temp_tvel_max *= 1.01;
                temp_tvel_max = Math.Round(temp_tvel_max, 1);

                DataTable data = new DataTable();
                data.Columns.Add("Название", typeof(string));
                data.Columns.Add("Значение", typeof(string));
                data.Rows.Add("Тепловая мощность реактора", power + " МВт");
                data.Rows.Add("Расход теплоносителя через активную зону", expenditure + " кг/с");
                data.Rows.Add("Темп. на входе в активную зону", temp_in + " °C");
                data.Rows.Add("Темп. на выходе из активной зоны", temp_out + " °C");
                data.Rows.Add("Давление теплоносителя", pressure + " МПа");
                data.Rows.Add("Коэфф. энерговыделения (высота)", heigth_kr);
                data.Rows.Add("Коэфф. энерговыделения (радиус)", radius_kz);
                data.Rows.Add("Радиус", radius + " м");
                data.Rows.Add("Высота", heigth + " м");
                data.Rows.Add("Эффективная добавка", effective + " м");
                data.Rows.Add("Количество кассет в активной зоне", count_caset + " шт");
                data.Rows.Add("Количество ТВЭЛ в кассете", tvel + " шт");
                data.Rows.Add("Шаг расположения кассет", step + " м");
                data.Rows.Add("Толщина стенки кассеты", thickness + " м");
                data.Rows.Add("Объем активной зоны реактора", V + " м³");
                data.Rows.Add("Режим работы реактора", mode);
                data.Rows.Add("Удельное объемное энерговыделение", q + " МВт/м³");
                data.Rows.Add("Скорость теплоносителя в активной зоне", speed_t + " м/с");
                data.Rows.Add("Средняя теплоемкость теплоносителя", c + " кДж/кг·град");
                data.Rows.Add("Средняя температура", temp + " °C");
                data.Rows.Add("Эффективная высота", effective_h + " м");
                data.Rows.Add("Эффективный радиус", effective_r + " м");
                data.Rows.Add("Расход теплоносителя", G0 + " кг/с");
                data.Rows.Add("Удельный линейный тепловой поток", ql0 + "  кВт/м");
                data.Rows.Add("Коэффициент теплоотдачи", de);
                data.Rows.Add("Коэффициент теплоотдачи от оболочки ТВЭЛ", a);
                data.Rows.Add("Температурный перепад", delt + " °C");
                data.Rows.Add("Максимальная температура оболочки ТВЭЛ", temp_tvel_max + " °C");

                DataResearch.ItemsSource = data.DefaultView;

                // Результат исследования
                int result = temp_tvel_max <= 350 ? 1 : 0;

                if (result == 0)
                {
                    status.Text = "Провальное";
                    status.Foreground = new SolidColorBrush(Colors.IndianRed);
                }

                // График температур ТВЭЛ
                chart2.ChartAreas.Add(new ChartArea("Default"));

                chart2.Series.Add(new Series("Series1"));
                chart2.Series["Series1"].ChartArea = "Default";
                chart2.Series["Series1"].ChartType = SeriesChartType.Spline;
                chart2.BackColor = System.Drawing.Color.LightGray;
                chart2.Series["Series1"].BorderWidth = 3;
                chart2.ChartAreas[0].AxisY.Title = "Температура";
                chart2.ChartAreas[0].AxisX.Title = "Высота";
                chart2.ChartAreas[0].AxisY.Minimum = 250;
                if(temp_tvel_max >  1000)
                    chart2.ChartAreas[0].AxisY.Maximum = Math.Round((temp_tvel_max - (temp_tvel_max % 1000)) + 1000);
                else
                    chart2.ChartAreas[0].AxisY.Maximum = Math.Round((temp_tvel_max - (temp_tvel_max % 100)) + 100);
                chart2.Titles.Add("Распределение температуры наружной поверхности оболочки ТВЭЛ по высоте самого энергонапряжённого канала");
                chart2.Series["Series1"].Color = System.Drawing.Color.SkyBlue;

                double[] axisXData2 = new double[] { z0, z1, z2, z_max, z3, z4 };
                double[] axisYData2 = new double[] { temp_tvel1, temp_tvel2, temp_tvel3, temp_tvel_max, temp_tvel4, temp_tvel5 };
                chart2.Series["Series1"].Points.DataBindXY(axisXData2, axisYData2);
                chart2.Series["Series1"].MarkerStyle = MarkerStyle.Circle;
                chart2.Series["Series1"].IsValueShownAsLabel = true;
            }
            catch
            {
            }
        }
        /// <summary>
        /// Возвращение на форму "Создание исследования - характеристики зоны теплоносителя"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
                _page2.ShowDialog();
            }
            catch (System.InvalidOperationException)
            {
            }
        }
        /// <summary>
        /// Создание исследования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calculate_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<int, double> Kwater = new Dictionary<int, double>();
            Kwater.Add(0, 1.78);
            Kwater.Add(20, 1.006);
            Kwater.Add(40, 0.65);
            Kwater.Add(60, 0.47);
            Kwater.Add(80, 0.36);
            Kwater.Add(100, 0.29);
            Kwater.Add(120, 0.25);
            Kwater.Add(140, 0.21);
            Kwater.Add(160, 0.19);
            Kwater.Add(180, 0.17);
            Kwater.Add(200, 0.15);
            Kwater.Add(220, 0.14);
            Kwater.Add(240, 0.14);
            Kwater.Add(260, 0.13);
            Kwater.Add(280, 0.13);
            Kwater.Add(300, 0.12);

            Dictionary<int, double> Dwater = new Dictionary<int, double>();
            Dwater.Add(0, 1788);
            Dwater.Add(20, 1004);
            Dwater.Add(40, 653.3);
            Dwater.Add(60, 469.9);
            Dwater.Add(80, 355.1);
            Dwater.Add(100, 282.5);
            Dwater.Add(120, 237.4);
            Dwater.Add(140, 201.1);
            Dwater.Add(160, 173.6);
            Dwater.Add(180, 153.0);
            Dwater.Add(200, 136.4);
            Dwater.Add(220, 124.6);
            Dwater.Add(240, 114.8);
            Dwater.Add(260, 105.9);
            Dwater.Add(280, 98.1);
            Dwater.Add(300, 91.2);

            try
            {
                // Объем активной зоны реактора +
                double V = Math.PI * Math.Pow(radius, 2) * heigth;
                V = Math.Round(V, 2);

                // Удельное объемное энерговыделение +
                double q = power / V;
                q = Math.Round(q, 2);

                // Скорость теплоносителя в активной зоне +
                double Fm = count_caset * 6 * ((0.5 * 0.238) / Math.Cos(Math.PI * 30 / 180)) * ((step - 0.238) / 2);
                Fm = Math.Round(Fm, 6);
                double Fc = 6 * (Math.Sqrt(3) / 4) * Math.Pow((0.5 * 0.238) / Math.Cos(Math.PI * 30 / 180), 2);
                Fc = Math.Round(Fc, 6);
                double Fctk = 6 * ((0.5 * 0.238) / Math.Cos(Math.PI * 30 / 180)) * thickness;
                Fctk = Math.Round(Fctk, 6);
                double Ftc = tvel * ((Math.PI * Math.Pow(0.0091, 2)) / 4);
                Ftc = Math.Round(Ftc, 6);
                double Fctc = (Math.PI * Math.Pow(0.0103, 2)) / 4;
                Fctc = Math.Round(Fctc, 2);
                double Fk = 13 * ((Math.PI * Math.Pow(0.0126, 2)) / 4);
                Fk = Math.Round(Fk, 6);
                double F = Fc - Fctk - Ftc - Fctc - Fk;
                F = Math.Round(F, 6);
                double speed_t = expenditure / (716.9 * (Fm + (count_caset * F)));
                speed_t = Math.Round(speed_t, 2);

                // Средняя температура +
                double temp = (temp_in + temp_out) / 2;
                temp = Math.Round(temp, 0);

                // Средняя теплоемкость теплоносителя в активной зоне +
                double c = (pressure * 107) / temp;
                c = Math.Round(c, 2);

                // Эффективная высота +
                effective_h = heigth + (2 * effective);
                effective_h = Math.Round(effective_h, 3);

                // Эффективный радиус +
                double effective_r = radius + effective;
                effective_r = Math.Round(effective_r, 3);

                // Расход теплоносителя через самый энергонапряженный канал +
                double G0 = (Convert.ToDouble(expenditure) / Convert.ToDouble((count_caset * (tvel + 14)))) * radius_kz;
                G0 = Math.Round(G0, 3);

                // Удельный линейный тепловой поток в центре
                double ql0 = ((power * 1000) / (count_caset * tvel * heigth)) * heigth_kr * radius_kz;
                ql0 = Math.Round(ql0, 5);

                // Температура теплоносителя по высоте самого энергонапряженного канала
                double fz = temp_in + (((ql0 * effective_h) / (G0 * c * Math.PI)) * (Math.Sin((Math.PI * c) / effective_h) + Math.Sin((Math.PI * heigth) / (2 * effective_h))));

                // Коэффициент теплоотдачи от наружной поверхности оболочки ТВЭЛ к теплоносителю
                double de = Math.Sqrt((4 * Math.Pow(radius, 2)) / (count_caset * 331));
                de = Math.Round(de, 5);

                // Динамическая вязкость
                double D = 0;
                foreach (KeyValuePair<int, double> element in Dwater.Reverse())
                {
                    if (temp == element.Key)
                    { D = element.Value; D *= Math.Pow(10, -6); break; }
                    if (Math.Abs(temp - element.Key) < 20)
                    { D = element.Value; D *= Math.Pow(10, -6); break; }
                }

                // Кинематическая вязкость
                double K = 0;
                foreach (KeyValuePair<int, double> element in Kwater.Reverse())
                {
                    if (temp == element.Key)
                    { K = element.Value; break; }
                    if (Math.Abs(temp - element.Key) < 20)
                    { K = element.Value; break; }
                }
                ;
                K *= Math.Pow(10, -6);

                // Число Прандтля +
                double P = ((c * D) / 0.58) * 1000;
                P = Math.Round(P, 4);

                // Число Рейнольдса +
                double R = (speed_t * de) / K;
                R = Math.Round(R, 0);

                string mode = "Ламинарный";

                if (R > Math.Pow(10, 5))
                    mode = "Тарбулентный";

                // Коэффициент теплоотдачи от оболочки ТВЭЛ к теплоносителю +
                double a = 0.021 * (0.55 / de) * Math.Pow(R, 0.8) * Math.Pow(P, 0.43);
                a = Math.Round(a, 0);

                // Температурный перепад
                delt = (0.93 * power * 1000 * heigth_kr * radius_kz) / (Math.PI * 0.0091 * a * Math.Pow(10, -3) * count_caset * tvel * heigth);
                delt = Math.Round(delt, 1);

                // Графики
                // Значения высоты z
                double z0 = -heigth / 2;
                double z1 = -heigth / 4;
                double z2 = 0;
                double z3 = heigth / 4;
                double z4 = heigth / 2;

                // Значения температур для z
                double f1 = Math.Round(temp_in + (((power * 1000 * heigth_kr * effective_h) / (expenditure * heigth * c * Math.PI)) * (Math.Sin((Math.PI * z0) / effective_h) + Math.Sin((Math.PI * heigth) / (2 * effective_h)))), 1);
                double f2 = Math.Round(temp_in + (((power * 1000 * heigth_kr * effective_h) / (expenditure * heigth * c * Math.PI)) * (Math.Sin((Math.PI * z1) / effective_h) + Math.Sin((Math.PI * heigth) / (2 * effective_h)))), 1);
                double f3 = Math.Round(temp_in + (((power * 1000 * heigth_kr * effective_h) / (expenditure * heigth * c * Math.PI)) * (Math.Sin((Math.PI * z2) / effective_h) + Math.Sin((Math.PI * heigth) / (2 * effective_h)))), 1);
                double f4 = Math.Round(temp_in + (((power * 1000 * heigth_kr * effective_h) / (expenditure * heigth * c * Math.PI)) * (Math.Sin((Math.PI * z3) / effective_h) + Math.Sin((Math.PI * heigth) / (2 * effective_h)))), 1);
                double f5 = Math.Round(temp_in + (((power * 1000 * heigth_kr * effective_h) / (expenditure * heigth * c * Math.PI)) * (Math.Sin((Math.PI * z4) / effective_h) + Math.Sin((Math.PI * heigth) / (2 * effective_h)))), 1);

                // Температура наружной поверхности оболочки ТВЭЛ
                double temp_tvel1 = Math.Round(f1 + (delt * Math.Cos((Math.PI * z0) / effective_h)), 1);
                double temp_tvel2 = Math.Round(f2 + (delt * Math.Cos((Math.PI * z1) / effective_h)), 1);
                double temp_tvel3 = Math.Round(f3 + (delt * Math.Cos((Math.PI * z2) / effective_h)), 1);
                double temp_tvel4 = Math.Round(f4 + (delt * Math.Cos((Math.PI * z3) / effective_h)), 1);
                double temp_tvel5 = Math.Round(f5 + (delt * Math.Cos((Math.PI * z4) / effective_h)), 1);

                // Коорддината максимальной точки
                double z_max = Math.Round((effective_h / Math.PI) * (Math.Atan((temp_out - temp_in) / (2 * Math.Sin((Math.PI * heigth) / (2 * effective_h)) * delt))), 3);

                // Максимальная температура поверхности оболочки ТВЭЛ
                double temp_tvel_max = temp_in + ((temp_out - temp_in) / 2) + delt * Math.Sqrt(1 + Math.Pow(((temp_out - temp_in) / 2) / (2 * delt), 2) * Math.Pow(Math.Sin((Math.PI * heigth) / (2 * effective_h)), -2));
                temp_tvel_max *= 1.01;
                temp_tvel_max = Math.Round(temp_tvel_max, 1);

                // Результат исследования
                string result = temp_tvel_max <= 350 ? "Успешное" : "Провальное";

                Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                string host = currentConfig.AppSettings.Settings["host"].Value;
                string uid = currentConfig.AppSettings.Settings["uid"].Value;
                string pwd = currentConfig.AppSettings.Settings["pwd"].Value;
                string database = currentConfig.AppSettings.Settings["database"].Value;
                string connectionString = $"host={host};uid={uid};pwd={pwd};database={database}";

                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                if (MessageBox.Show("Вы уверены, что хотите создать исследование с текущими характеристиками?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    if (title.Text != "")
                    {
                        string query = $"SELECT MAX(id_research) FROM research";

                        MySqlCommand command = new MySqlCommand(query, connection);

                        int count = 1;

                        if (command.ExecuteScalar() != DBNull.Value)
                            count = Convert.ToInt32(command.ExecuteScalar()) + 1;

                        string query_insert_research = $"INSERT INTO research (id_research, title_research, success_research, user_id_research, reactor_characteristics_id_research, temperature_charts_id_research, control_values_id_research, date_research) VALUES ({count}, '{title.Text}', '{result}', (SELECT id_user FROM user WHERE login_user = '{_login}'), {count}, {count}, {count}, '{DateTime.Now.ToString()}');";

                        MySqlCommand commandIR = new MySqlCommand(query_insert_research, connection);

                        commandIR.ExecuteNonQuery();

                        string query_insert_reactor_characteristics = $"INSERT INTO reactor_characteristics (id_rс, thermal_power_rc, coolant_consumption_rc, temperature_coolant_exit_rc, temperature_coolant_entrance_rc, coolant_pressure_rc, coefficient_unevenness_heigth_rc, coefficient_unevenness_radius_rc, radius_rc, heigth_rc, effective_supplement_rc, cassettes_in_active_zone_rc, tvel_in_cassette_rc, step_cassette_arrangement_rc, cassette_wall_thickness_rc) VALUES ({count}, {power}, {expenditure}, {temp_in}, {temp_out}, '{pressure.ToString().Replace(",", ".")}', '{heigth_kr.ToString().Replace(",", ".")}', '{radius_kz.ToString().Replace(",", ".")}', '{radius.ToString().Replace(",", ".")}', '{heigth.ToString().Replace(",", ".")}', '{effective.ToString().Replace(",", ".")}', {count_caset}, {tvel}, '{step.ToString().Replace(",", ".")}', '{thickness.ToString().Replace(",", ".")}')";

                        MySqlCommand commandRC = new MySqlCommand(query_insert_reactor_characteristics, connection);

                        commandRC.ExecuteNonQuery();

                        string query_insert_control_values = $"INSERT INTO control_values (id_cv, reactor_core_volume_cv, mode_cv, specific_volumetric_energy_cv, velocity_coolant_core_cv, average_heat_coolant_core_cv, average_temperature_core_cv, effective_height_cv, effective_radius_cv, coolant_consumption_cv, specific_linear_center_cv, heat_transfer_coefficient_outer_cv, heat_transfer_coefficient_from_cv, temperature_difference_cv, temperature_outer_shell_cv) VALUES ({count}, '{V.ToString().Replace(",", ".")}', '{mode}', '{q.ToString().Replace(",", ".")}', '{speed_t.ToString().Replace(",", ".")}', '{c.ToString().Replace(",", ".")}', '{temp.ToString().Replace(",", ".")}', '{effective_h.ToString().Replace(",", ".")}', '{effective_r.ToString().Replace(",", ".")}', '{G0.ToString().Replace(",", ".")}', '{ql0.ToString().Replace(",", ".")}', '{de.ToString().Replace(",", ".")}', '{a.ToString().Replace(",", ".")}', '{delt.ToString().Replace(",", ".")}', '{temp_tvel_max.ToString().Replace(",", ".")}');";

                        MySqlCommand commandCV = new MySqlCommand(query_insert_control_values, connection);

                        commandCV.ExecuteNonQuery();

                        string query_insert_temperature_charts = $"INSERT INTO temperature_charts (id_tc, temperature_heat_carrier_height_tc, temperature_values_tc, temperature_values_tvel_tc) VALUES ({count}, '{heigth.ToString().Replace(",", ".")}', '{f1.ToString().Replace(",", ".") + " " + f2.ToString().Replace(",", ".") + " " + f3.ToString().Replace(",", ".") + " " + f4.ToString().Replace(",", ".") + " " + f5.ToString().Replace(",", ".")}', '{temp_tvel1.ToString().Replace(",", ".") + " " + temp_tvel2.ToString().Replace(",", ".") + " " + temp_tvel3.ToString().Replace(",", ".") + " " + temp_tvel4.ToString().Replace(",", ".") + " " + temp_tvel5.ToString().Replace(",", ".")}');";

                        MySqlCommand commandTC = new MySqlCommand(query_insert_temperature_charts, connection);

                        commandTC.ExecuteNonQuery();

                        MessageBox.Show($"Исследование успешно создано!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);

                        this.Close();
                        _page2.Close();
                        _page1.ShowDialog();
                    }
                    else
                        MessageBox.Show($"Недопустимое название!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch(System.InvalidOperationException)
            {
            }
        }
        /// <summary>
        /// Обработка ввода в поле "Название исследования"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void title_TextChanged(object sender, TextChangedEventArgs e)
        {
            int cursor = title.SelectionStart;

            if (regex.IsMatch(title.Text))
            {
                title.Text = regex.Replace(title.Text, "");
                title.SelectionStart = cursor - 1;
            }
        }
        /// <summary>
        /// Обработка закрытия формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void research_resume_Closed(object sender, EventArgs e)
        {
        }
    }
}