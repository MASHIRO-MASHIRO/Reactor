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
using System.Data;
using System.Windows.Forms;
using MySqlX.XDevAPI.Relational;
using System.Windows.Markup;
using System.ComponentModel;
using System.Xml.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Word;
using Application = System.Windows.Application;
using static System.Collections.Specialized.BitVector32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.Office.Interop.Excel;
using System.Collections.ObjectModel;

namespace Thermal_and_hydraulic_calculation_of_the_reactor
{
    /// <summary>
    /// Форма просмотра исследований сотрудником
    /// </summary>
    public partial class ViewingResearch : System.Windows.Window
    {
        static private PageOfUser _page;
        static private int id;

        public System.Data.DataTable data = new System.Data.DataTable();

        public DataView dv = null;
        public ViewingResearch(PageOfUser page)
        {
            InitializeComponent();

            _page = page;

            list.ItemsSource = new List<string> { "Статус", "Логин", "ФИО" };
            list_sort.ItemsSource = new List<string> { "Статус", "Логин", "ФИО" };
            sort_value.ItemsSource = new List<string> { "Возрастание", "Убывание" };
        }
        /// <summary>
        /// Загрузка исследований
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Configuration currentConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            string host = currentConfig.AppSettings.Settings["host"].Value;
            string uid = currentConfig.AppSettings.Settings["uid"].Value;
            string pwd = currentConfig.AppSettings.Settings["pwd"].Value;
            string database = currentConfig.AppSettings.Settings["database"].Value;
            string connectionString = $"host={host};uid={uid};pwd={pwd};database={database}";

            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            string query = $"SELECT id_research, success_research, title_research, date_research, login_user, CONCAT_WS(' ', last_name_user, first_name_user, middle_name_user) as FIO FROM research INNER JOIN user  ON id_user = user_id_research;";

            MySqlCommand select = new MySqlCommand(query, connection);

            MySqlDataAdapter adapter = new MySqlDataAdapter(select);

            adapter.Fill(data);

            control_values.ItemsSource = data.DefaultView;
            dv = data.DefaultView;

            control_values.Columns[0].Visibility = Visibility.Collapsed;

            control_values.Columns[1].Header = "Статус";
            control_values.Columns[2].Header = "Название";
            control_values.Columns[3].Header = "Дата";
            control_values.Columns[4].Header = "Логин сотрудника";
            control_values.Columns[5].Header = "ФИО сотрудника";

            control_values.ScrollIntoView(control_values.Items[control_values.Items.Count - 1]);
        }
        /// <summary>
        /// Выбор отдельного исследования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void control_values_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (DataRowView) control_values.SelectedItem;
            id = Convert.ToInt32(item.Row[0]);
            show.IsEnabled = true;
        }
        /// <summary>
        /// Возвращение на форму "Страница сотрудника"
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
        /// Переход на форму "Детальный просмотр исследования"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void show_Click(object sender, RoutedEventArgs e)
        {
            ShowResearch page = new ShowResearch(id);
            page.ShowDialog();
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
        /// Поле для ввода строки фильтра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filter.Text = "";
            if(list.SelectedItem != null)
                filter.IsEnabled = true;
        }
        /// <summary>
        /// Смена фокуса с выбранного исследования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void list_GotFocus(object sender, RoutedEventArgs e)
        {
            show.IsEnabled = false;
        }
        /// <summary>
        /// Смена фокуса с выбранного исследования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filter_GotFocus(object sender, RoutedEventArgs e)
        {
            show.IsEnabled = false;
        }
        /// <summary>
        /// Смена фокуса с выбранного исследования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void show_row_GotFocus(object sender, RoutedEventArgs e)
        {
            show.IsEnabled = false;
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

        private void contin_Click(object sender, RoutedEventArgs e)
        {

        }

        private void back_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
