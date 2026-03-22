using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
using Microsoft.Office.Interop.Excel;
using MySql.Data.MySqlClient;

namespace Thermal_and_hydraulic_calculation_of_the_reactor
{
    /// <summary>
    /// Форма создания пользователя
    /// </summary>
    public partial class CreateUser : System.Windows.Window
    {
        static public Regex regexPassword = new Regex(@"[^a-zA-Z0-9]");
        public Regex regexR = new Regex(@"[^а-яА-Я-]");

        public string path = AppDomain.CurrentDomain.BaseDirectory + "\\image";
        public string save_path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) + "\\save_image";

        public string image_name = "user_image.png";

        static private Administrator_page _page;
        /// <summary>
        /// Генерация соли для пароля
        /// </summary>
        /// <returns></returns>
        static byte[] GenerateSalt()
        {
            const int SaltLength = 64;
            byte[] salt = new byte[SaltLength];

            var rngRand = new RNGCryptoServiceProvider();
            rngRand.GetBytes(salt);

            return salt;
        }
        public string GeneratePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder password = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                password.Append(valid[rnd.Next(valid.Length)]);
            }
            return password.ToString();
        }
        /// <summary>
        /// Шифрования пароля
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
        /// <returns>
        /// Возвращает true, если пароль валиден
        /// false, в противном случае
        /// </returns>
        static bool PasswordValide(string password)
        {
            bool char_lower_valide = false;
            bool char_upper_valise = false;
            bool digit_valide = false;
            bool regex_valide = false;

            foreach (char c in password)
            {
                if (char.IsLower(c))
                    char_lower_valide = true;
                if (char.IsUpper(c))
                    char_upper_valise = true;
                if (char.IsDigit(c))
                    digit_valide = true;
            }

            if (!regexPassword.IsMatch(password))
                regex_valide = true;

            return char_lower_valide && char_upper_valise && digit_valide && regex_valide;
        }
        public CreateUser(Administrator_page page, string login)
        {
            InitializeComponent();

            Image newIcon = new Image();
            newIcon.Source = new BitmapImage(new Uri(path + "\\" + "password_hide.png"));
            password_visible.Content = newIcon;

            _page = page;

            image.Source = BitmapFrame.Create(new Uri(save_path + "\\" + image_name));
        }
        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void create_user_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                string host = currentConfig.AppSettings.Settings["host"].Value;
                string uid = currentConfig.AppSettings.Settings["uid"].Value;
                string pwd = currentConfig.AppSettings.Settings["pwd"].Value;
                string database = currentConfig.AppSettings.Settings["database"].Value;
                string connectionString = $"host={host};uid={uid};pwd={pwd};database={database}";

                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                MySqlCommand mails = new MySqlCommand($"SELECT COUNT(*) FROM user WHERE mail_user = '{mail.Text}'");
                mails.Connection = connection;

                int result = Convert.ToInt32(mails.ExecuteScalar());

                if (password_show.Visibility == Visibility.Visible)
                    password.Password = password_show.Text;

                TimeSpan time = (DateTime.Parse(date.Text) - DateTime.Now).Duration();

                if (login.Text == "" || password.Password == "" || mail.Text == "" || last_name.Text == "" || first_name.Text == "" || date.Text == "")
                    MessageBox.Show($"Данные не заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    if (!((mail.Text.Contains("@mail.ru") && mail.Text.Length >= 11) || (mail.Text.Contains("@gmail.com") && mail.Text.Length >= 13)))
                        MessageBox.Show($"Почта не соответствует шаблону или идентификатор слишком короткий!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    else if (password.Password.Length < 8 || !PasswordValide(password.Password))
                        MessageBox.Show($"Недопустимый пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    else if (result != 0)
                        MessageBox.Show($"Такая почта уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    else if (DateTime.Parse(date.Text) >= DateTime.Now)
                        MessageBox.Show($"Недопустимый возраст!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    else if ((time.TotalDays / 365.2425) <= 18)
                        MessageBox.Show($"Регистрация запрещена для лиц не достигших 18 лет!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                    {
                        DataSet data_table = new DataSet();

                        MySqlCommand users = new MySqlCommand($"SELECT login_user FROM user");
                        users.Connection = connection;

                        users.ExecuteNonQuery();

                        MySqlDataAdapter adapter = new MySqlDataAdapter(users);

                        adapter.Fill(data_table);

                        for (int i = 0; i < data_table.Tables[0].Rows.Count; i++)
                        {
                            if(login.Text == data_table.Tables[0].Rows[i]["login_user"].ToString())
                            {
                                throw new Exception();
                            }
                        }
                        if(MessageBox.Show("Вы уверены, что хотите создать пользователя?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                        {
                            string query_insert = "INSERT INTO user (role_id_user, login_user, password_user, last_name_user, first_name_user, middle_name_user, date_birth_user, mail_user, research_permit_user, image_user) VALUES (@value1, @value2, @value3, @value4, @value5, @value6, @value7, @value8, @value9, @value10)";

                            using (MySqlCommand command = new MySqlCommand(query_insert, connection))
                            {
                                if (check_admin.IsChecked == true)
                                    command.Parameters.AddWithValue("@value1", "2");
                                else
                                    command.Parameters.AddWithValue("@value1", "1");
                                command.Parameters.AddWithValue("@value2", login.Text);

                                byte[] salt = GenerateSalt();
                                byte[] sha256Hash = GenerateSha256Hash(password.Password, salt);
                                string sha256HashString = Convert.ToBase64String(sha256Hash);

                                command.Parameters.AddWithValue("@value3", sha256HashString);
                                command.Parameters.AddWithValue("@value4", last_name.Text);
                                command.Parameters.AddWithValue("@value5", first_name.Text);
                                command.Parameters.AddWithValue("@value6", middle_name.Text);
                                command.Parameters.AddWithValue("@value7", DateTime.Parse(date.Text));
                                command.Parameters.AddWithValue("@value8", mail.Text);

                                if (check_is.IsChecked == true)
                                    command.Parameters.AddWithValue("@value9", "1");
                                else
                                    command.Parameters.AddWithValue("@value9", "0");

                                command.Parameters.AddWithValue("@value10", image_name);

                                command.ExecuteNonQuery();

                                MessageBox.Show($"Пользователь {login.Text} успешно создан!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);

                                login.Text = "";
                                password.Password = "";
                                password_show.Text = "";
                                mail.Text = "";
                                last_name.Text = "";
                                first_name.Text = "";
                                middle_name.Text = "";
                                date.Text = "";

                                if(check_admin.IsChecked == true)
                                {
                                    check_admin.IsChecked = false;
                                    check_is.Visibility = Visibility.Visible;
                                    text_is.Visibility = Visibility.Visible;
                                }
                                else
                                {
                                    check_is.IsChecked = false;
                                    check_admin.Visibility = Visibility.Visible;
                                    text_admin.Visibility = Visibility.Visible;
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception)
            {
                MessageBox.Show($"Пользователь с таким логином уже есть в системе!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
        /// Обработка ввода в поле "Логин"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void login_TextChanged(object sender, TextChangedEventArgs e)
        {
            int cursor = login.SelectionStart;

            if (regexPassword.IsMatch(login.Text))
            {
                login.Text = regexPassword.Replace(login.Text, "");
                login.SelectionStart = cursor - 1;
            }
        }
        /// <summary>
        /// Обработка ввода в поле "Фамилия"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void last_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (last_name.Text.Length != 0)
                last_name_label.Visibility = Visibility.Hidden;
            else
                last_name_label.Visibility = Visibility.Visible;

            int cursor = last_name.SelectionStart;

            if (regexR.IsMatch(last_name.Text))
            {
                last_name.Text = regexR.Replace(last_name.Text, "");
                last_name.SelectionStart = cursor - 1;
            }

            if (last_name.Text.Length != 0)
            {
                last_name.Text = char.ToUpper(last_name.Text[0]) + last_name.Text.Substring(1, last_name.Text.Length - 1).ToLower();
                last_name.SelectionStart = cursor;
            }
        }
        /// <summary>
        /// Обработка ввода в поле "Имя"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void first_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (first_name.Text.Length != 0)
                first_name_label.Visibility = Visibility.Hidden;
            else
                first_name_label.Visibility = Visibility.Visible;

            int cursor = first_name.SelectionStart;

            if (regexR.IsMatch(first_name.Text))
            {
                first_name.Text = regexR.Replace(first_name.Text, "");
                first_name.SelectionStart = cursor - 1;
            }

            if (first_name.Text.Length != 0)
            {
                first_name.Text = char.ToUpper(first_name.Text[0]) + first_name.Text.Substring(1, first_name.Text.Length - 1).ToLower();
                first_name.SelectionStart = cursor;
            }
        }
        /// <summary>
        /// Обработка ввода в поле "Отчество"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void middle_name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (middle_name.Text.Length != 0)
                middle_name_label.Visibility = Visibility.Hidden;
            else
                middle_name_label.Visibility = Visibility.Visible;

            int cursor = middle_name.SelectionStart;

            if (regexR.IsMatch(middle_name.Text))
            {
                middle_name.Text = regexR.Replace(middle_name.Text, "");
                middle_name.SelectionStart = cursor - 1;
            }

            if (middle_name.Text.Length != 0)
            {
                middle_name.Text = char.ToUpper(middle_name.Text[0]) + middle_name.Text.Substring(1, middle_name.Text.Length - 1).ToLower();
                middle_name.SelectionStart = cursor;
            }
        }
        /// <summary>
        /// Метка "Администратор"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void check(object sender, RoutedEventArgs e)
        {
            if (check_admin.IsChecked == true)
            {
                check_is.Visibility = Visibility.Hidden;
                text_is.Visibility = Visibility.Hidden;
            }
            else
            {
                check_is.Visibility = Visibility.Visible;
                text_is.Visibility = Visibility.Visible;
            }
        }
        /// <summary>
        /// Метка "Права на исследования"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void check1(object sender, RoutedEventArgs e)
        {
            if (check_is.IsChecked == true)
            {
                check_admin.Visibility = Visibility.Hidden;
                text_admin.Visibility = Visibility.Hidden;
            }
            else
            {
                check_admin.Visibility = Visibility.Visible;
                text_admin.Visibility = Visibility.Visible;
            }
        }
        /// <summary>
        /// Изменение изображения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void select_image_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();

            dialog.DefaultExt = ".png";
            dialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg";

            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {
                string filename_image = dialog.FileName;
                string filename = dialog.SafeFileName;

                if (!File.Exists(save_path + "\\" + filename))
                {
                    System.IO.File.Copy(filename_image, save_path + "\\" + filename);
                    image.Source = BitmapFrame.Create(new Uri(save_path + "\\" + filename));
                    image_name = filename;
                }
                else if (MessageBox.Show("Файл с таким названием уже есть в системе.\nИспользовать существующее изображение?", "Информация", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    image.Source = BitmapFrame.Create(new Uri(save_path + "\\" + filename));
                    image_name = filename;
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
            _page.ShowDialog();
        }

        private void password_generation_Click(object sender, RoutedEventArgs e)
        {
            password.Password = GeneratePassword(8);
            password_show.Text = password.Password;
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
                newIcon.Source = new BitmapImage(new Uri(path + "\\" + "password_show.png"));
                password_visible.Content = newIcon;
            }
            else
            {
                password.Password = password_show.Text;
                password_show.Visibility = Visibility.Hidden;
                password.Visibility = Visibility.Visible;

                password_visible.Content = null;

                Image newIcon = new Image();
                newIcon.Source = new BitmapImage(new Uri(path + "\\" + "password_hide.png"));
                password_visible.Content = newIcon;
            }
        }
    }
}