using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
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
    /// Форма страницы пользователя
    /// </summary>
    public partial class PageOfUser : Window
    {
        Authorization page_;
        public string save_path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) + "\\save_image";
        public PageOfUser(Authorization page, string login, string user_name, string role, string date_birth, string mail, int research_permit, string image_user)
        {
            InitializeComponent();

            page_ = page;

            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { time.Content = DateTime.Now.ToString(); };
            timer.Start();

            login_user.Text = login;
            username.Text = user_name;
            role_user.Text = role;
            birth.Text = date_birth;
            mail_user.Text = mail;

            if (research_permit == 1)
            {
                warning.Visibility = Visibility.Hidden;
            }

            image.Source = BitmapFrame.Create(new Uri(save_path + "\\" + image_user));
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
            catch (System.InvalidOperationException)
            {

            }
        }
        /// <summary>
        /// Переход на форму "Создание исследования"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void research_init_Click(object sender, RoutedEventArgs e)
        {
            if (warning.IsVisible == false)
            {
                Research_caset page = new Research_caset(this, login_user.Text);
                this.Hide();
                page.ShowDialog();
            }
            else
            {
                MessageBox.Show($"{username.Text} ваши права доступа к проведению исследований были отклонены!\n(обратитеть к администратору для восстановления доступа)", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// Переход на форму "Просмотр исследований"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void archive_Click(object sender, RoutedEventArgs e)
        {
            ViewingResearch page = new ViewingResearch(this);
            this.Hide();
            page.ShowDialog();
        }
        /// <summary>
        /// Обработка закрытия формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
            page_.ShowDialog();
        }
    }
}
