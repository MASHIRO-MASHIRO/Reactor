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
    /// Форма удаления пользователя
    /// </summary>
    public partial class RemoveUser : Window
    {
        DispatcherTimer _timer;
        TimeSpan _time;

        static private Administrator_page _page;
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
        public RemoveUser(Administrator_page page, string login)
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

            _page = page;

            Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            string host = currentConfig.AppSettings.Settings["host"].Value;
            string uid = currentConfig.AppSettings.Settings["uid"].Value;
            string pwd = currentConfig.AppSettings.Settings["pwd"].Value;
            string database = currentConfig.AppSettings.Settings["database"].Value;
            string connectionString = $"host={host};uid={uid};pwd={pwd};database={database}";

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            DataSet data_table = new DataSet();

            MySqlCommand users = new MySqlCommand($"SELECT login_user FROM user");
            users.Connection = connection;

            users.ExecuteNonQuery();

            MySqlDataAdapter adapter = new MySqlDataAdapter(users);

            adapter.Fill(data_table);

            for (int i = 0; i < data_table.Tables[0].Rows.Count; i++)
            {
                if(login != data_table.Tables[0].Rows[i]["login_user"].ToString())
                    user_list.Items.Add(data_table.Tables[0].Rows[i]["login_user"].ToString());
            }

            user_list.SelectedItem = user_list.Items[user_list.Items.Count - 1];

            count_user.Text = user_list.Items.Count.ToString();
        }
        /// <summary>
        /// Возвращение на форму "Страница администратора"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void remove_user_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Вы уверены, что хотите удалить пользователя?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                string host = currentConfig.AppSettings.Settings["host"].Value;
                string uid = currentConfig.AppSettings.Settings["uid"].Value;
                string pwd = currentConfig.AppSettings.Settings["pwd"].Value;
                string database = currentConfig.AppSettings.Settings["database"].Value;
                string connectionString = $"host={host};uid={uid};pwd={pwd};database={database}";

                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string query_delete = "DELETE FROM user WHERE login_user = @value";

                using (MySqlCommand command = new MySqlCommand(query_delete, connection))
                {
                    command.Parameters.AddWithValue("@value", user_list.SelectedItem.ToString());

                    string login = user_list.SelectedItem.ToString();

                    command.ExecuteNonQuery();

                    MessageBox.Show($"Пользователь {login} успешно удалён!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);

                    user_list.Items.Remove(login);
                    user_list.SelectedItem = user_list.Items[user_list.Items.Count - 1];

                    count_user.Text = user_list.Items.Count.ToString();
                }
            }
        }
        /// <summary>
        /// Обработка закрытия формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
        }
    }
}
