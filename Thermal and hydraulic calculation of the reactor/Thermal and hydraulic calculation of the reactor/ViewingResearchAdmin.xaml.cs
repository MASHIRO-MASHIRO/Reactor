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
using System.Windows.Forms;
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
    /// Форма просмотра исследований администратором
    /// </summary>
    public partial class ViewingResearchAdmin : Window
    {
        System.Windows.Threading.DispatcherTimer _timer;
        TimeSpan _time;

        static private Administrator_page _page;
        static private int id;
        static private string name_research;

        static private DataTable data = new DataTable();

        public DataView dv = null;
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
        public ViewingResearchAdmin(Administrator_page page)
        {
            InitializeComponent();

            _time = TimeSpan.FromSeconds(30);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                if (_time == TimeSpan.Zero) _timer.Stop();
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, System.Windows.Application.Current.Dispatcher);

            _timer.Start();

            ComponentDispatcher.ThreadIdle += new System.EventHandler(ComponentDispatcher_ThreadIdle);

            _page = page;

            list.ItemsSource = new List<string> { "Статус", "Логин", "ФИО" };
            list_sort.ItemsSource = new List<string> { "Статус", "Логин", "ФИО" };
            sort_value.ItemsSource = new List<string> { "Возрастание", "Убывание" };
        }
        /// <summary>
        /// Строка фильтра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filter.Text = "";
            if (list.SelectedItem != null)
                filter.IsEnabled = true;
        }
        /// <summary>
        /// Загрузка исследований
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void control_values_Loaded(object sender, RoutedEventArgs e)
        {
            Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            string host = currentConfig.AppSettings.Settings["host"].Value;
            string uid = currentConfig.AppSettings.Settings["uid"].Value;
            string pwd = currentConfig.AppSettings.Settings["pwd"].Value;
            string database = currentConfig.AppSettings.Settings["database"].Value;
            string connectionString = $"host={host};uid={uid};pwd={pwd};database={database}";

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = $"SELECT id_research, success_research, title_research, date_research, login_user, '************' as FIO, '******', '******' FROM research INNER JOIN user  ON id_user = user_id_research;";

            MySqlCommand select = new MySqlCommand(query, connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(select);

            adapter.Fill(data);

            connection.Close();

            control_values.ItemsSource = data.DefaultView;
            dv = data.DefaultView;

            control_values.Columns[0].Visibility = Visibility.Collapsed;

            control_values.Columns[0].Header = "Идентификатор";
            control_values.Columns[1].Header = "Статус";
            control_values.Columns[2].Header = "Название";
            control_values.Columns[3].Header = "Дата";
            control_values.Columns[4].Header = "Логин сотрудника";
            control_values.Columns[5].Header = "ФИО сотрудника";
            control_values.Columns[6].Header = "Дата рождения";
            control_values.Columns[7].Header = "Почта";

            control_values.ScrollIntoView(control_values.Items[control_values.Items.Count - 1]);
        }
        /// <summary>
        /// Фильтрация исследований
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filter_TextChanged(object sender, TextChangedEventArgs e)
        {
            control_values.ScrollIntoView(control_values.Items[control_values.Items.Count - 1]);

            if (show_row.Text == "")
            {
                if (Convert.ToString(list.SelectedItem) == "Статус")
                {
                    foreach (var item in control_values.Items)
                    {
                        var row = control_values.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                        if (row != null && control_values.Columns[1].GetCellContent(row) != null)
                        {
                            TextBlock tbl = control_values.Columns[1].GetCellContent(row) as TextBlock;
                            if (!tbl.Text.Contains(filter.Text))
                                row.Visibility = Visibility.Collapsed;
                            else
                                row.Visibility = Visibility.Visible;
                        }
                    }
                }
                else if (Convert.ToString(list.SelectedItem) == "ФИО")
                {
                    foreach (var item in control_values.Items)
                    {
                        var row = control_values.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                        if (row != null && control_values.Columns[5].GetCellContent(row) != null)
                        {
                            TextBlock tbl = control_values.Columns[5].GetCellContent(row) as TextBlock;
                            if (!tbl.Text.Contains(filter.Text))
                                row.Visibility = Visibility.Collapsed;
                            else
                                row.Visibility = Visibility.Visible;
                        }
                    }
                }
                else if (Convert.ToString(list.SelectedItem) == "Логин")
                {
                    foreach (var item in control_values.Items)
                    {
                        var row = control_values.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                        if (row != null && control_values.Columns[4].GetCellContent(row) != null)
                        {
                            TextBlock tbl = control_values.Columns[4].GetCellContent(row) as TextBlock;
                            if (!tbl.Text.Contains(filter.Text))
                                row.Visibility = Visibility.Collapsed;
                            else
                                row.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
            else
            {
                if (Convert.ToString(list.SelectedItem) == "Статус")
                {
                    foreach (var item in control_values.Items)
                    {
                        var row = control_values.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                        if (row != null && control_values.Columns[2].GetCellContent(row) != null && control_values.Columns[1].GetCellContent(row) != null)
                        {
                            TextBlock tbl = control_values.Columns[2].GetCellContent(row) as TextBlock;
                            TextBlock tbl_status = control_values.Columns[1].GetCellContent(row) as TextBlock;
                            if (!tbl.Text.Contains(show_row.Text))
                                continue;
                            if (!tbl_status.Text.Contains(filter.Text))
                                row.Visibility = Visibility.Collapsed;
                            else
                                row.Visibility = Visibility.Visible;
                        }
                    }
                }
                else if (Convert.ToString(list.SelectedItem) == "ФИО")
                {
                    foreach (var item in control_values.Items)
                    {
                        var row = control_values.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                        if (row != null && control_values.Columns[2].GetCellContent(row) != null && control_values.Columns[5].GetCellContent(row) != null)
                        {
                            TextBlock tbl = control_values.Columns[2].GetCellContent(row) as TextBlock;
                            TextBlock tbl_worker = control_values.Columns[5].GetCellContent(row) as TextBlock;
                            if (!tbl.Text.Contains(show_row.Text))
                                continue;
                            if (!tbl_worker.Text.Contains(filter.Text))
                                row.Visibility = Visibility.Collapsed;
                            else
                                row.Visibility = Visibility.Visible;
                        }
                    }
                }
                else if (Convert.ToString(list.SelectedItem) == "Логин")
                {
                    foreach (var item in control_values.Items)
                    {
                        var row = control_values.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                        if (row != null && control_values.Columns[2].GetCellContent(row) != null && control_values.Columns[4].GetCellContent(row) != null)
                        {
                            TextBlock tbl = control_values.Columns[2].GetCellContent(row) as TextBlock;
                            TextBlock tbl_worker = control_values.Columns[4].GetCellContent(row) as TextBlock;
                            if (!tbl.Text.Contains(show_row.Text))
                                continue;
                            if (!tbl_worker.Text.Contains(filter.Text))
                                row.Visibility = Visibility.Collapsed;
                            else
                                row.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Поиск исследований
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void show_row_TextChanged(object sender, TextChangedEventArgs e)
        {
            control_values.ScrollIntoView(control_values.Items[control_values.Items.Count - 1]);

            if (list.SelectedItem == null || filter.Text == "")
            {
                foreach (var item in control_values.Items)
                {
                    var row = control_values.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                    if (row != null && control_values.Columns[2].GetCellContent(row) != null)
                    {
                        TextBlock tbl = control_values.Columns[2].GetCellContent(row) as TextBlock;
                        if (!tbl.Text.Contains(show_row.Text))
                            row.Visibility = Visibility.Collapsed;
                        else
                            row.Visibility = Visibility.Visible;
                    }
                }
            }
            else
            {
                if (Convert.ToString(list.SelectedItem) == "Статус")
                {
                    foreach (var item in control_values.Items)
                    {
                        var row = control_values.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                        if (row != null && control_values.Columns[2].GetCellContent(row) != null && control_values.Columns[1].GetCellContent(row) != null)
                        {
                            TextBlock tbl = control_values.Columns[2].GetCellContent(row) as TextBlock;
                            TextBlock tbl_status = control_values.Columns[1].GetCellContent(row) as TextBlock;
                            if (!tbl_status.Text.Contains(filter.Text))
                                continue;
                            if (!tbl.Text.Contains(show_row.Text))
                                row.Visibility = Visibility.Collapsed;
                            else
                                row.Visibility = Visibility.Visible;
                        }
                    }
                }
                else if (Convert.ToString(list.SelectedItem) == "ФИО")
                {
                    foreach (var item in control_values.Items)
                    {
                        var row = control_values.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                        if (row != null && control_values.Columns[2].GetCellContent(row) != null && control_values.Columns[5].GetCellContent(row) != null)
                        {
                            TextBlock tbl = control_values.Columns[2].GetCellContent(row) as TextBlock;
                            TextBlock tbl_worker = control_values.Columns[5].GetCellContent(row) as TextBlock;
                            if (!tbl_worker.Text.Contains(filter.Text))
                                continue;
                            if (!tbl.Text.Contains(show_row.Text))
                                row.Visibility = Visibility.Collapsed;
                            else
                                row.Visibility = Visibility.Visible;
                        }
                    }
                }
                else if (Convert.ToString(list.SelectedItem) == "Логин")
                {
                    foreach (var item in control_values.Items)
                    {
                        var row = control_values.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                        if (row != null && control_values.Columns[2].GetCellContent(row) != null && control_values.Columns[4].GetCellContent(row) != null)
                        {
                            TextBlock tbl = control_values.Columns[2].GetCellContent(row) as TextBlock;
                            TextBlock tbl_worker = control_values.Columns[4].GetCellContent(row) as TextBlock;
                            if (!tbl_worker.Text.Contains(filter.Text))
                                continue;
                            if (!tbl.Text.Contains(show_row.Text))
                                row.Visibility = Visibility.Collapsed;
                            else
                                row.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Переход на форму "Детальный просмотр исследований администратором"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void show_admin_Click(object sender, RoutedEventArgs e)
        {
            ShowResearchAdmin page = new ShowResearchAdmin(id);
            page.ShowDialog();
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
                data.Clear();
                this.Close();
                _page.ShowDialog();
            }
            catch (System.InvalidOperationException)
            {
            }
        }
        /// <summary>
        /// Выбор исследования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void control_values_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(control_values.SelectedItem != null)
            {
                var item = (DataRowView)control_values.SelectedItem;
                id = Convert.ToInt32(item.Row[0]);
                name_research = Convert.ToString(item.Row[2]);
                show_admin.IsEnabled = true;
                delete.IsEnabled = true;
            }
        }
        /// <summary>
        /// Смена фокуса с выбранного исследования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void list_GotFocus(object sender, RoutedEventArgs e)
        {
            show_admin.IsEnabled = false;
            delete.IsEnabled = false;
        }
        /// <summary>
        /// Смена фокуса с выбранного исследования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filter_GotFocus(object sender, RoutedEventArgs e)
        {
            show_admin.IsEnabled = false;
            delete.IsEnabled = false;
        }
        /// <summary>
        /// Смена фокуса с выбранного исследования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void show_row_GotFocus(object sender, RoutedEventArgs e)
        {
            show_admin.IsEnabled = false;
            delete.IsEnabled = false;
        }
        /// <summary>
        /// Удаление выбранного исследования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.MessageBox.Show($"После подтверждения данные будут безвозвратно удалены.\nВы уверены, что хотите удалить выбранное исследование?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                string host = currentConfig.AppSettings.Settings["host"].Value;
                string uid = currentConfig.AppSettings.Settings["uid"].Value;
                string pwd = currentConfig.AppSettings.Settings["pwd"].Value;
                string database = currentConfig.AppSettings.Settings["database"].Value;
                string connectionString = $"host={host};uid={uid};pwd={pwd};database={database}";

                MySqlConnection connection = new MySqlConnection(connectionString);
                connection.Open();

                string query_r = $"DELETE FROM research WHERE id_research = {id};";

                MySqlCommand delete_r = new MySqlCommand(query_r, connection);

                MySqlDataAdapter adapter_r = new MySqlDataAdapter(delete_r);

                delete_r.ExecuteNonQuery();

                connection.Close();

                System.Windows.MessageBox.Show($"Исследование '{name_research}' было удалено", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);

                //control_values.Items.Remove(control_values.SelectedItem);

                foreach (DataRow row in data.Rows)
                    if (Convert.ToInt32(row.ItemArray[0].ToString()) == id)
                    {
                        data.Rows.Remove(row);
                        break;
                    }

                show_admin.IsEnabled = false;
                delete.IsEnabled = false;
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
        /// <summary>
        /// Смена фокуса с выбранного исследования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void list_sort_GotFocus(object sender, RoutedEventArgs e)
        {
            show_admin.IsEnabled = false;
            delete.IsEnabled = false;
        }
        /// <summary>
        /// Сортировка данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sort_value_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (list_sort.SelectedItem.ToString() == "Статус" && sort_value.SelectedItem != null)
            {
                if (sort_value.SelectedItem.ToString() == "Возрастание")
                    dv.Sort = "success_research asc";
                else
                    dv.Sort = "success_research desc";
            }
            else if (list_sort.SelectedItem.ToString() == "Логин" && sort_value.SelectedItem != null)
            {
                if (sort_value.SelectedItem.ToString() == "Возрастание")
                    dv.Sort = "login_user asc";
                else
                    dv.Sort = "login_user desc";
            }
            else if (list_sort.SelectedItem.ToString() == "ФИО" && sort_value.SelectedItem != null)
            {
                if (sort_value.SelectedItem.ToString() == "Возрастание")
                    dv.Sort = "FIO asc";
                else
                    dv.Sort = "FIO desc";
            }

            control_values.UpdateLayout();
            control_values.ScrollIntoView(dv);
        }
    }
}
