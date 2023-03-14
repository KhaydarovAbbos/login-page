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
    /// Логика взаимодействия для StoreView.xaml
    /// </summary>
    public partial class StoreView : UserControl
    {
        MainView MainView { get; set; }
        public static string StoreName = "";

        public StoreView()
        {
            InitializeComponent();

        }

        public void GetMainView(MainView mainView)
        {
            MainView = mainView;

            txtName.Text = StoreName;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            MainView.AllCloseControls(1);
        }
    }
}
