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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Thermal_and_hydraulic_calculation_of_the_reactor
{
    /// <summary>
    /// Форма страницы администратора
    /// </summary>
    public partial class Administrator_page : Window
    {
        DispatcherTimer _timer;
        TimeSpan _time;

        Authorization page_;
        public string save_path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) + "\\save_image";
        void ComponentDispatcher_ThreadIdle(object sender, EventArgs e)
        {
            if(_time.TotalSeconds == 0)
            {
                this.Close();
            }

        }
        public Administrator_page(Authorization page, string login, string user_name, string date_birth, string mail, string image_admin)
        {
            InitializeComponent();

            _time = TimeSpan.FromSeconds(5);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                if (_time == TimeSpan.Zero) _timer.Stop();
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();

            ComponentDispatcher.ThreadIdle += new System.EventHandler(ComponentDispatcher_ThreadIdle);

            page_ = page;

            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { time.Content = DateTime.Now.ToString(); };
            timer.Start();

            login_user.Text = login;
            username.Text = user_name;
            role_user.Text = "Администратор";
            birth.Text = date_birth;
            mail_user.Text = mail;

            image.Source = BitmapFrame.Create(new Uri(save_path + "\\" + image_admin));
        }
        /// <summary>
        /// Возвращение на форму "Авторизация"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();
                page_.ShowDialog();
            }
            catch(System.InvalidOperationException)
            {
            }
        }
        /// <summary>
        /// Переход на форму "Создание пользователя"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Create_user_Click(object sender, RoutedEventArgs e)
        {
            CreateUser page = new CreateUser(this, login_user.Text);
            this.Hide();
            page.ShowDialog();
        }
        /// <summary>
        /// Переход на форму "Удаление пользователя"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Remove_user_Click(object sender, RoutedEventArgs e)
        {
            RemoveUser page = new RemoveUser(this, login_user.Text);
            this.Hide();
            page.ShowDialog();
        }
        /// <summary>
        /// Переход на форму "Изменение пользователя"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Change_user_Click(object sender, RoutedEventArgs e)
        {
            ChangeUser page = new ChangeUser(this, login_user.Text);
            this.Hide();
            page.ShowDialog();
        }
        /// <summary>
        /// Переход на форму "Просмотр исследований"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void archive_Click(object sender, RoutedEventArgs e)
        {
            ViewingResearchAdmin page = new ViewingResearchAdmin(this);
            this.Hide();
            page.ShowDialog();
        }
        /// <summary>
        /// Удаление всех исследовательских данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void archive_remove_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show($"После подтверждения данные будут безвозвратно удалены.\nВы уверены, что хотите очистить исследовательскую базу?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                string host = currentConfig.AppSettings.Settings["host"].Value;
                string uid = currentConfig.AppSettings.Settings["uid"].Value;
                string pwd = currentConfig.AppSettings.Settings["pwd"].Value;
                string database = currentConfig.AppSettings.Settings["database"].Value;
                string connectionString = $"host={host};uid={uid};pwd={pwd};database={database}";

                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string query = $"DELETE FROM research; DELETE FROM control_values; DELETE FROM reactor_characteristics; DELETE FROM temperature_charts;";

                MySqlCommand delete = new MySqlCommand(query, connection);

                delete.ExecuteNonQuery();

                MessageBox.Show($"Исследовательская база была очищена", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        /// <summary>
        /// Обработка закрытия окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            page_.ShowDialog();
        }
    }
}
