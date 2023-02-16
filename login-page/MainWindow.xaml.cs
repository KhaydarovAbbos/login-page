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

namespace login_page
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnExit_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        public void AllCloseControls(int i)
        {

            sign_in_view.Visibility = Visibility.Hidden;
            sign_up_view.Visibility = Visibility.Hidden;

            if (i == 1)
            {
                sign_in_view.Visibility = Visibility.Visible;
            }
            if(i == 2)
            {
                sign_up_view.Visibility = Visibility.Visible;
            }
        }
    }
}
