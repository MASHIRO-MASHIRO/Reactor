using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Thermal_and_hydraulic_calculation_of_the_reactor
{
    /// <summary>
    /// Форма создание исследования - характеристики зоны теплоносителя
    /// </summary>
    public partial class Research_energy_characteristics : Window
    {
        static Research_caset _page;
        static private string _login;

        static int count_caset;
        static int tvel;
        static double radius;
        static double heigth;
        static double effective;
        static double step;
        static double thickness;
        public Research_energy_characteristics(Research_caset page, int count_caset_value, int tvel_value, double radius_value, double heigth_value, double effective_value, double step_value, double thickness_value, string login)
        {
            _page = page;

            _login = login;

            count_caset = count_caset_value;
            tvel = tvel_value;
            radius = radius_value;
            heigth = heigth_value;
            effective = effective_value;
            step = step_value;
            thickness = thickness_value;

            InitializeComponent();
        }
        /// <summary>
        /// Возвращение на форму "Создание исследования - характеристики активной зоны реактора"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Hide();
                _page.ShowDialog();
            }
            catch (System.InvalidOperationException)
            {
            }
        }
        /// <summary>
        /// Переход на форму "Завершение исследования"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calculate_energy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int power_value = Convert.ToInt32(power.Content);
                int temp_in_value = Convert.ToInt32(temp_in.Content);
                int temp_out_value = Convert.ToInt32(temp_out.Content);
                int expenditure_value = Convert.ToInt32(expenditure.Content);
                double pressure_value = Math.Round(Convert.ToDouble(pressure.Content), 1);
                double radius_kz_value = Math.Round(Convert.ToDouble(radius_kz.Content), 2);
                double heigth_kr_value = Math.Round(Convert.ToDouble(heigth_kr.Content), 2);

                this.Hide();
                Research_resume page = new Research_resume(_page, this, count_caset, tvel, radius, heigth, effective, step, thickness, power_value, temp_in_value, temp_out_value, expenditure_value, pressure_value, radius_kz_value, heigth_kr_value, _login);
                page.ShowDialog();
            }
            catch (Exception)
            {
                MessageBox.Show($"Контрольные параметры для ядерной реакторной установки не могут быть вычислины.\nПроверьте корректность ввода начальных характеристик", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                this.Close();
                _page.ShowDialog();
            }
        }
        /// <summary>
        /// Обработка закрытия формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                _page.ShowDialog();
            }
            catch (System.InvalidOperationException)
            {
            }
        }
    }
}
