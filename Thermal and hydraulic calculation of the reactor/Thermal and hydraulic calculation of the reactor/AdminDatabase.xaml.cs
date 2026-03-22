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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace Thermal_and_hydraulic_calculation_of_the_reactor
{
    /// <summary>
    /// Логика взаимодействия для AdminDatabase.xaml
    /// </summary>
    public partial class AdminDatabase : Window
    {
        public AdminDatabase()
        {
            InitializeComponent();
        }

        private void recovery_Click(object sender, RoutedEventArgs e)
        {
            if(System.Windows.MessageBox.Show($"Вы уверены, что хотите восстановить базу данных (данные будут удалены)?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                try
                {
                    Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                    string host = currentConfig.AppSettings.Settings["host"].Value;
                    string uid = currentConfig.AppSettings.Settings["uid"].Value;
                    string pwd = currentConfig.AppSettings.Settings["pwd"].Value;
                    string connectionString = $"host={host};uid={uid};pwd={pwd};";

                    MySqlConnection connection = new MySqlConnection(connectionString);
                    connection.Open();

                    string query = @System.IO.File.ReadAllText(@"restoring the database.sql", Encoding.GetEncoding(1251));

                    MySqlCommand new_db = new MySqlCommand(query, connection);

                    new_db.ExecuteNonQuery();

                    connection.Close();

                    System.Windows.MessageBox.Show($"База данных была восстановлена", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    MessageBox.Show($"{ex.ToString()}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void import_Click(object sender, RoutedEventArgs e)
        {

        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
