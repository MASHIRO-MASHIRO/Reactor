using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using MySql.Data.MySqlClient;

namespace Thermal_and_hydraulic_calculation_of_the_reactor
{
    /// <summary>
    /// Форма изменения пользователя
    /// </summary>
    public partial class ChangeUser : Window
    {
        System.Windows.Threading.DispatcherTimer _timer;
        TimeSpan _time;

        static public Regex regexPassword = new Regex(@"[^a-zA-Z0-9]");
        public Regex regexD = new Regex(@"[^a-zA-Z0-9]!#\$%&\*()_");
        public Regex regexR = new Regex(@"[^а-яА-Я-]");

        public string path = AppDomain.CurrentDomain.BaseDirectory + "\\image";
        public string save_path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) + "\\save_image";

        public string image_name = "user_image.png";

        Administrator_page _page;
        string login_name;
        void ComponentDispatcher_ThreadIdle(object sender, EventArgs e)
        {
            if (_time.TotalSeconds == 0)
            {
                this.Close();
            }

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
        /// Функция для генерация соли пароля
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
        /// Функция для шифрования пароля
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
        /// Функция проверки валидности пароля
        /// </summary>
        /// <param name="password"></param>
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
                if(char.IsLower(c))
                    char_lower_valide = true;
                if(char.IsUpper(c))
                    char_upper_valise = true;
                if(char.IsDigit(c))
                    digit_valide = true;
            }

            if (!regexPassword.IsMatch(password))
                regex_valide = true;

            return char_lower_valide && char_upper_valise && digit_valide && regex_valide;
        }
        public ChangeUser(Administrator_page page, string login)
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

            Image newIcon = new Image();
            newIcon.Source = new BitmapImage(new Uri(path + "\\" + "password_hide.png"));
            password_visible.Content = newIcon;

            login_name = login;
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

            MySqlCommand users = new MySqlCommand($"SELECT login_user FROM user WHERE login_user != '{login}'");
            users.Connection = connection;

            users.ExecuteNonQuery();

            MySqlDataAdapter adapter = new MySqlDataAdapter(users);

            adapter.Fill(data_table);

            for (int i = 0; i < data_table.Tables[0].Rows.Count; i++)
            {
                if (login_name != data_table.Tables[0].Rows[i]["login_user"].ToString())
                    user_list.Items.Add(data_table.Tables[0].Rows[i]["login_user"].ToString());
            }

            user_list.SelectedItem = user_list.Items[user_list.Items.Count - 1];

            data_table.Clear();

            MySqlCommand user = new MySqlCommand($"SELECT role_id_user, last_name_user, first_name_user, middle_name_user, date_birth_user, mail_user, research_permit_user, image_user FROM user WHERE login_user = '{user_list.SelectedItem}'");
            user.Connection = connection;

            user.ExecuteNonQuery();

            MySqlDataAdapter adapter_user = new MySqlDataAdapter(user);

            adapter_user.Fill(data_table);

            last_name.Text = data_table.Tables[0].Rows[0]["last_name_user"].ToString();
            first_name.Text = data_table.Tables[0].Rows[0]["first_name_user"].ToString();
            middle_name.Text = data_table.Tables[0].Rows[0]["middle_name_user"].ToString();

            date.Text = data_table.Tables[0].Rows[0]["date_birth_user"].ToString();

            if (Convert.ToInt32(data_table.Tables[0].Rows[0]["role_id_user"]) == 2)
            {
                check_admin.IsChecked = true;
                check_is.Visibility = Visibility.Hidden;
                text_is.Visibility = Visibility.Hidden;
            }

            if (Convert.ToInt32(data_table.Tables[0].Rows[0]["research_permit_user"]) == 1)
            {
                check_is.IsChecked = true;
                check_admin.Visibility = Visibility.Hidden;
                text_admin.Visibility = Visibility.Hidden;
            }

            mail.Text = data_table.Tables[0].Rows[0]["mail_user"].ToString();

            image_name = data_table.Tables[0].Rows[0]["image_user"].ToString();
        }
        /// <summary>
        /// Изменение пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void change_user_Click(object sender, RoutedEventArgs e)
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

                MySqlCommand mails = new MySqlCommand($"SELECT COUNT(*) FROM user WHERE mail_user = '{mail.Text}' AND login_user != '{user_list.SelectedItem.ToString()}'");
                mails.Connection = connection;

                int result = Convert.ToInt32(mails.ExecuteScalar());

                TimeSpan time = (DateTime.Parse(date.Text) - DateTime.Now).Duration();

                if(password_show.Visibility == Visibility.Visible)
                    password.Password = password_show.Text;

                if (user_list.SelectedItem.ToString() == "" || password.Password == "" || mail.Text == "" || last_name.Text == "" || first_name.Text == "" || date.Text == "")
                    MessageBox.Show($"Данные не заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    if(user_list.SelectedItem.ToString() == login_name)
                        MessageBox.Show($"Нельзя изменить текущего пользователя!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    else if(password.Password.Length < 8 || !PasswordValide(password.Password))
                        MessageBox.Show($"Недопустимый пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    else if (!((mail.Text.Contains("@mail.ru") && mail.Text.Length >= 11) || (mail.Text.Contains("@gmail.com") && mail.Text.Length >= 13)))
                        MessageBox.Show($"Почта не соответствует шаблону или идентификатор слишком короткий!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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

                        if (MessageBox.Show("Вы уверены, что хотите изменить пользователя?", "Сообщение", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                        {
                            string query_insert = "UPDATE user SET role_id_user = @value1, password_user = @value3, last_name_user = @value4, first_name_user = @value5, middle_name_user = @value6, date_birth_user = @value7, mail_user = @value11, research_permit_user = @value9, image_user = @value10 WHERE login_user = @value2";

                            using (MySqlCommand command = new MySqlCommand(query_insert, connection))
                            {
                                if (check_admin.IsChecked == true)
                                    command.Parameters.AddWithValue("@value1", "2");
                                else
                                    command.Parameters.AddWithValue("@value1", "1");
                                command.Parameters.AddWithValue("@value2", user_list.SelectedItem.ToString());

                                byte[] salt = GenerateSalt();
                                byte[] sha256Hash = GenerateSha256Hash(password.Password, salt);
                                string sha256HashString = Convert.ToBase64String(sha256Hash);

                                command.Parameters.AddWithValue("@value3", sha256HashString);
                                command.Parameters.AddWithValue("@value4", last_name.Text);
                                command.Parameters.AddWithValue("@value5", first_name.Text);
                                command.Parameters.AddWithValue("@value6", middle_name.Text);
                                command.Parameters.AddWithValue("@value7", DateTime.Parse(date.Text));
                                command.Parameters.AddWithValue("@value10", image_name);
                                command.Parameters.AddWithValue("@value11", mail.Text);

                                if (check_is.IsChecked == true)
                                    command.Parameters.AddWithValue("@value9", "1");
                                else
                                    command.Parameters.AddWithValue("@value9", "0");

                                command.ExecuteNonQuery();

                                MessageBox.Show($"Пользователь {user_list.SelectedItem.ToString()} успешно изменён!", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);

                                user_list.SelectedItem = user_list.Items[user_list.Items.Count - 1];
                                password.Password = "";
                                password_show.Text = "";
                                mail.Text = "";
                                last_name.Text = "";
                                first_name.Text = "";
                                middle_name.Text = "";
                                date.Text = "";

                                if (check_admin.IsChecked == true)
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
            catch (FormatException)
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
        /// Смена пользователя
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void user_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            check_admin.IsChecked = false;
            check_is.IsChecked = false;

            Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            string host = currentConfig.AppSettings.Settings["host"].Value;
            string uid = currentConfig.AppSettings.Settings["uid"].Value;
            string pwd = currentConfig.AppSettings.Settings["pwd"].Value;
            string database = currentConfig.AppSettings.Settings["database"].Value;
            string connectionString = $"host={host};uid={uid};pwd={pwd};database={database}";

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand user = new MySqlCommand($"SELECT role_id_user, last_name_user, first_name_user, middle_name_user, date_birth_user, mail_user, research_permit_user, image_user FROM user WHERE login_user = '{user_list.SelectedItem}'");
            user.Connection = connection;

            user.ExecuteNonQuery();

            MySqlDataAdapter adapter_user = new MySqlDataAdapter(user);

            DataSet data_table = new DataSet();

            adapter_user.Fill(data_table);

            last_name.Text = data_table.Tables[0].Rows[0]["last_name_user"].ToString();
            first_name.Text = data_table.Tables[0].Rows[0]["first_name_user"].ToString();
            middle_name.Text = data_table.Tables[0].Rows[0]["middle_name_user"].ToString();

            date.Text = data_table.Tables[0].Rows[0]["date_birth_user"].ToString();

            if (Convert.ToInt32(data_table.Tables[0].Rows[0]["role_id_user"]) == 2)
                check_admin.IsChecked = true;

            if (Convert.ToInt32(data_table.Tables[0].Rows[0]["research_permit_user"]) == 1)
                check_is.IsChecked = true;

            mail.Text = data_table.Tables[0].Rows[0]["mail_user"].ToString();

            image_name = data_table.Tables[0].Rows[0]["image_user"].ToString();

            image.Source = BitmapFrame.Create(new Uri(save_path + "\\" + data_table.Tables[0].Rows[0]["image_user"].ToString()));
        }
        /// <summary>
        /// Обработка закрытия формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
            _page.ShowDialog();
        }

        private void password_generation_Click(object sender, RoutedEventArgs e)
        {
            password.Password = GeneratePassword(8);
            password_show.Text = password.Password;
        }

        private void password_visible_Click(object sender, RoutedEventArgs e)
        {
            if(password_show.Visibility == Visibility.Hidden)
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
