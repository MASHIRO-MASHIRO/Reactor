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
using System.IO;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Thermal_and_hydraulic_calculation_of_the_reactor
{
    /// <summary>
    /// Форма авторизации пользователя
    /// </summary>
    public partial class Authorization : Window
    {
        public Regex regexPassword = new Regex(@"[^a-zA-Z0-9]");

        public string save_path = AppDomain.CurrentDomain.BaseDirectory + "\\image";
        public Authorization()
        {
            InitializeComponent();

            password_show.BorderBrush = new SolidColorBrush(Color.FromArgb(100, 38, 44, 51));
            password_show.Background = new SolidColorBrush(Color.FromArgb(100, 38, 44, 51));

            Image newIcon = new Image();
            newIcon.Source = new BitmapImage(new Uri(save_path + "\\" + "password_hide.png"));
            password_visible.Content = newIcon;
        }
        /// <summary>
        /// Функция генерации соли для пароля
        /// </summary>
        /// <returns>
        /// Возвращает соль
        /// </returns>
        static byte[] GenerateSalt()
        {
            const int SaltLength = 64;
            byte[] salt = new byte[SaltLength];

            var rngRand = new RNGCryptoServiceProvider();
            rngRand.GetBytes(salt);

            return salt;
        }
        /// <summary>
        /// Функция шифрования пароля
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns>
        /// Возвращает зашифрованный пароль
        /// </returns>
        static byte[] GenerateSha256Hash(string password, byte[] salt)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltedPassword = new byte[salt.Length + passwordBytes.Length];

            var hash = new SHA256CryptoServiceProvider();

            return hash.ComputeHash(saltedPassword);
        }
        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void entrance_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (login.Text == "")
                {
                    login.BorderBrush = new SolidColorBrush(Colors.Red);
                    login.Background = new SolidColorBrush(Color.FromArgb(100, 151, 33, 37));
                }
                else if (password_show.Text == "" && password_show.Visibility == Visibility.Visible)
                {
                    password_show.BorderBrush = new SolidColorBrush(Colors.Red);
                    password_show.Background = new SolidColorBrush(Color.FromArgb(100, 151, 33, 37));

                    password.BorderBrush = new SolidColorBrush(Colors.Red);
                    password.Background = new SolidColorBrush(Color.FromArgb(100, 151, 33, 37));
                }
                else if (password.Password.ToString() == "")
                {
                    password.BorderBrush = new SolidColorBrush(Colors.Red);
                    password.Background = new SolidColorBrush(Color.FromArgb(100, 151, 33, 37));

                    password_show.BorderBrush = new SolidColorBrush(Colors.Red);
                    password_show.Background = new SolidColorBrush(Color.FromArgb(100, 151, 33, 37));
                }
                else
                {
                    if(password_show.Visibility == Visibility.Visible)
                        password.Password = password_show.Text;

                    Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                    string host = currentConfig.AppSettings.Settings["host"].Value;
                    string uid = currentConfig.AppSettings.Settings["uid"].Value;
                    string pwd = currentConfig.AppSettings.Settings["pwd"].Value;
                    string database = currentConfig.AppSettings.Settings["database"].Value;
                    string connectionString = $"host={host};uid={uid};pwd={pwd};database={database}";

                    MySqlConnection connection = new MySqlConnection(connectionString);
                    connection.Open();

                    DataSet data_table = new DataSet();

                    MySqlCommand users = new MySqlCommand($"SELECT password_user, first_name_user, last_name_user, DATE_FORMAT(date_birth_user,'%d/%m/%Y'), research_permit_user, (SELECT role.title_role FROM role WHERE role_id_user = id_role), mail_user, image_user FROM user WHERE login_user = '{login.Text}';");
                    users.Connection = connection;

                    users.ExecuteNonQuery();

                    MySqlDataAdapter adapter = new MySqlDataAdapter(users);

                    adapter.Fill(data_table);

                    string user_name = data_table.Tables[0].Rows[0][1].ToString() + " " + data_table.Tables[0].Rows[0][2].ToString();
                    string user_password = data_table.Tables[0].Rows[0][0].ToString();
                    string role = data_table.Tables[0].Rows[0][5].ToString();
                    string date_birth = data_table.Tables[0].Rows[0][3].ToString();
                    string mail = data_table.Tables[0].Rows[0][6].ToString();
                    int research_permit = Convert.ToInt32(data_table.Tables[0].Rows[0][4]);
                    string image = data_table.Tables[0].Rows[0][7].ToString();

                    byte[] salt = GenerateSalt();
                    byte[] sha256Hash = GenerateSha256Hash(password.Password, salt);
                    string sha256HashString = Convert.ToBase64String(sha256Hash);

                    if (sha256HashString == user_password)
                    {
                        MessageBox.Show($"Добро пожаловать, {user_name}!", "Вход", MessageBoxButton.OK, MessageBoxImage.Information);

                        string login_autorization = login.Text;

                        login.Text = "";
                        password.Password = "";
                        password_show.Text = "";

                        if(password_show.Visibility == Visibility.Visible)
                        {
                            password_show.Visibility = Visibility.Hidden;
                            password.Visibility = Visibility.Visible;

                            password_visible.Content = null;

                            Image newIcon = new Image();
                            newIcon.Source = new BitmapImage(new Uri(save_path + "\\" + "password_hide.png"));
                            password_visible.Content = newIcon;
                        }


                        if (role == "Администратор")
                        {
                            Administrator_page page = new Administrator_page(this, login_autorization, user_name, date_birth, mail, image);
                            this.Hide();

                            page.ShowDialog();
                        }
                        else
                        {
                            PageOfUser page = new PageOfUser(this, login_autorization, user_name, role, date_birth, mail, research_permit, image);
                            this.Hide();

                            page.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Вы ввели неверный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch(System.IndexOutOfRangeException)
            {
                MessageBox.Show($"Вы ввели неверный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(MySql.Data.MySqlClient.MySqlException)
            {
                MessageBox.Show($"Не удалось подключиться к серверу MySQL!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        /// <summary>
        /// Обработка ввода в поле "логин"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void login_TextChanged(object sender, TextChangedEventArgs e)
        {
            login.BorderBrush = new SolidColorBrush(Color.FromArgb(100, 38, 44, 51));
            login.Background = new SolidColorBrush(Color.FromArgb(100, 38, 44, 51));

            int cursor = login.SelectionStart;

            if (regexPassword.IsMatch(login.Text))
            {
                login.Text = regexPassword.Replace(login.Text, "");
                login.SelectionStart = cursor - 1;
            }
        }
        /// <summary>
        /// Обработка в поле "пароль"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void password_TextChanged(object sender, RoutedEventArgs e)
        {
            password.BorderBrush = new SolidColorBrush(Color.FromArgb(100, 38, 44, 51));
            password.Background = new SolidColorBrush(Color.FromArgb(100, 38, 44, 51));
        }
        /// <summary>
        /// Закрытие приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show($"Вы уверены, что хотите выйти из приложения?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                Application.Current.Shutdown();
            else
                e.Cancel = true;
        }

        private void password_visible_Click(object sender, RoutedEventArgs e)
        {
            if (password_show.Visibility == Visibility.Hidden)
            {
                password_show.Text = password.Password;
                password_show.Visibility = Visibility.Visible;
                password.Visibility = Visibility.Hidden;

                password_visible.Content = null;

                Image newIcon = new Image();
                newIcon.Source = new BitmapImage(new Uri(save_path + "\\" + "password_show.png"));
                password_visible.Content = newIcon;
            }
            else
            {
                password.Password = password_show.Text;
                password_show.Visibility = Visibility.Hidden;
                password.Visibility = Visibility.Visible;

                password_visible.Content = null;

                Image newIcon = new Image();
                newIcon.Source = new BitmapImage(new Uri(save_path + "\\" + "password_hide.png"));
                password_visible.Content = newIcon;
            }
        }

        private void password_show_TextChanged(object sender, TextChangedEventArgs e)
        {
            password.Password = password_show.Text;

            password_show.BorderBrush = new SolidColorBrush(Color.FromArgb(100, 38, 44, 51));
            password_show.Background = new SolidColorBrush(Color.FromArgb(100, 38, 44, 51));
        }
    }
}
