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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using MySql.Data.MySqlClient;

namespace Thermal_and_hydraulic_calculation_of_the_reactor
{
    /// <summary>
    /// Логика взаимодействия для AdminDatabase.xaml
    /// </summary>
    public partial class AdminDatabase : Window
    {
        DispatcherTimer _timer;
        TimeSpan _time;

        Authorization _page;
        public AdminDatabase(Authorization page)
        {
            InitializeComponent();

            _time = TimeSpan.FromSeconds(30);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                if (_time == TimeSpan.Zero) _timer.Stop();
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();

            ComponentDispatcher.ThreadIdle += new System.EventHandler(ComponentDispatcher_ThreadIdle);

            list_of_tables.ItemsSource = new List<string> { "role", "user", "reactor_characteristics", "control_values", "temperature_charts", "research" };
            _page = page;
        }
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

                    System.Windows.MessageBox.Show($"База данных была восстановлена!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    MessageBox.Show($"{ex.ToString()}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void import_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                string host = currentConfig.AppSettings.Settings["host"].Value;
                string uid = currentConfig.AppSettings.Settings["uid"].Value;
                string pwd = currentConfig.AppSettings.Settings["pwd"].Value;
                string database = currentConfig.AppSettings.Settings["database"].Value;
                string connectionString = $"host={host};uid={uid};pwd={pwd};database={database};";

                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();

                dialog.DefaultExt = ".png";
                dialog.Filter = "CSV Files (*.csv)|*.csv";

                Nullable<bool> result = dialog.ShowDialog();

                if (System.Windows.MessageBox.Show($"Вы уверены, что хотите импортировать данные?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    try
                    {
                        if (result == true)
                        {
                            string filename = dialog.FileName;

                            string query = $"LOAD DATA LOCAL INFILE '{filename}' INTO TABLE {list_of_tables.SelectedItem} FIELDS TERMINATED BY ';' LINES TERMINATED BY '\n';";

                            MySqlCommand new_db = new MySqlCommand(query, connection);

                            int count_strings = new_db.ExecuteNonQuery();

                            System.Windows.MessageBox.Show($"Было успешно импортированно {count_strings} строк!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    catch (MySql.Data.MySqlClient.MySqlException ex)
                    {
                        MessageBox.Show($"Ошибка импорта данных (используйте разделитель - ;)", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                connection.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show($"{ex.ToString()}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
                _page.ShowDialog();
            }
            catch (System.InvalidOperationException)
            {
            }
        }

        private void list_of_tables_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            import.IsEnabled = true;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
        }
    }
}
