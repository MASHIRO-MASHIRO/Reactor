using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Thermal_and_hydraulic_calculation_of_the_reactor
{
    /// <summary>
    /// Форма создания исследования - выбор характеристик активной зоны реактора
    /// </summary>
    public partial class Research_caset : Window
    {
        static private PageOfUser _page;
        static private string _login;
        public Research_caset(PageOfUser page, string login)
        {
            InitializeComponent();

            _page = page;

            _login = login;

            count_caset.Text = "152";
        }
        //Обработка контроллов кассет
        private void caset74_Click(object sender, RoutedEventArgs e)
        {
            if(Color.Equals(((Button) sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 4);
                ((Button) sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset9_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset66_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 2);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 2);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset23_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset35_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset1_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset3_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset14_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset13_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset16_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset12_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset15_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset11_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset10_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset8_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset7_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset62_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 2);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 2);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset63_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 2);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 2);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset64_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 2);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 2);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset65_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 2);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 2);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset67_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 2);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 2);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset73_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset68_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 2);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 2);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset69_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 2);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 2);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset70_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 2);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 2);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset71_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 2);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 2);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset72_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset18_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset19_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset22_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset21_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset20_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset25_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset24_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset85_Click(object sender, RoutedEventArgs e)
        {
            if(Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset84_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset86_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset91_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset90_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset89_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset88_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset87_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset82_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset83_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset53_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset54_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset55_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset56_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset6_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset4_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset2_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset5_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset60_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset59_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset58_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset57_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset44_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset43_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset42_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset46_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset45_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset81_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset80_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset79_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset78_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 2);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 2);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset77_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 2);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 2);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset76_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset75_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset61_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 4);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset51_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset49_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset48_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset50_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset52_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset47_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset41_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset40_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset39_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset31_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset30_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset27_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset26_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset28_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset32_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset29_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset33_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset34_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset38_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset36_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset37_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }

        private void caset17_Click(object sender, RoutedEventArgs e)
        {
            if (Color.Equals(((Button)sender).Background.ToString(), "#FF4FD9FF"))
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) - 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(255, 79, 79));
            }
            else
            {
                count_caset.Text = Convert.ToString(Convert.ToInt32(count_caset.Text) + 1);
                ((Button)sender).Background = new SolidColorBrush(Color.FromRgb(79, 217, 255));
            }
        }
        /// <summary>
        /// Переход на форму "Создание исследования - характеристики зоны теплоносителя"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calculate_Click(object sender, RoutedEventArgs e)
        {
            int count_caset_value = Convert.ToInt32(count_caset.Text);
            int tvel_value = Convert.ToInt32(tvel.Content);

            double radius_value = Math.Round(Convert.ToDouble(radius.Content), 2);
            double heigth_value = Math.Round(Convert.ToDouble(heigth.Content), 2);
            double effective_value = Math.Round(Convert.ToDouble(effective.Content), 3);
            double step_value = Math.Round(Convert.ToDouble(step.Content), 3);
            double thickness_value = Math.Round(Convert.ToDouble(thickness.Content), 4);

            Research_energy_characteristics page = new Research_energy_characteristics(this, count_caset_value, tvel_value, radius_value, heigth_value, effective_value, step_value, thickness_value, _login);
            this.Hide();
            page.ShowDialog();
        }
        /// <summary>
        /// Возвращение на форму "Страница пользователя"
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
        /// Обработка закрытия формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
            _page.ShowDialog();
        }
    }
}