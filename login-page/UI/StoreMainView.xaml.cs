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

namespace login_page.UI
{
    /// <summary>
    /// Логика взаимодействия для StoreMainView.xaml
    /// </summary>
    public partial class StoreMainView : UserControl
    {
        MainWindow Mainwindow;

        public StoreMainView()
        {
            InitializeComponent();
        }

        public void GetMainWindow(MainWindow mainWindow)
        {
            Mainwindow = mainWindow;
        }

        private void create_MouseUp(object sender, MouseButtonEventArgs e)
        {
            AllCloseControls(1);
        }

        public void AllCloseControls(int i)
        {

            productcategory_view.Visibility = Visibility.Hidden;
            productSubCategory_view.Visibility = Visibility.Hidden;
            products_view.Visibility = Visibility.Hidden;

            if(i == -1)
            {
                Mainwindow.AllCloseControls(3);
            }
            if (i == 1)
            {
                productcategory_view.Visibility = Visibility.Visible;
                productcategory_view.GetMainView(this);
                productcategory_view.WindowLoad();
            }
            if (i == 2)
            {
                productSubCategory_view.Visibility = Visibility.Visible;
                productSubCategory_view.GetMainView(this);
                productSubCategory_view.WindowLoad();
            }
            if (i == 3)
            {
                products_view.Visibility = Visibility.Visible;
                products_view.GetMainView(this);
            }
        }

        private void main_MouseUp(object sender, MouseButtonEventArgs e)
        {
            AllCloseControls(-1);
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            if (MainGrid.ColumnDefinitions[0].Width.Value == 200)
            {
                MainGrid.ColumnDefinitions[0].Width = new GridLength(60, GridUnitType.Pixel);
                MainGrid.ColumnDefinitions[1].Width = new GridLength(1260, GridUnitType.Pixel);
            }
            else
            {
                MainGrid.ColumnDefinitions[0].Width = new GridLength(200, GridUnitType.Pixel);
                MainGrid.ColumnDefinitions[1].Width = new GridLength(1120, GridUnitType.Pixel);
            }
        }
    }
}
