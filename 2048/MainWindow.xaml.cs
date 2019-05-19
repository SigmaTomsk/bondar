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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _2048
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool changeLocation = false, @try = false;
        double Sx = 0, Sy = 0;
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void exit_button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if(changeLocation)
            {
                if (@try)
                {
                    double y = e.GetPosition(null).Y - Sy;
                    double x = e.GetPosition(null).X - Sx;

                    this.Left = x;
                    this.Top = y;
                    @try = false;
                }
                else @try = true;
            }
        }

        private void Grid_MouseRightButtonUp(object sender, MouseEventArgs e)
        {
            Sx = 0;
            Sy = 0;
            changeLocation = false;
        }

        private void Grid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Sx = e.GetPosition(null).X - this.Top;
            Sy = e.GetPosition(null).Y - this.Left;
            changeLocation = true;
        }

        private void Grid_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Sx = 0;
            Sy = 0;
            changeLocation = false;
        }
    }
}
