using System.Windows;
using System.Windows.Controls;

namespace login_page.UI
{
    /// <summary>
    /// Логика взаимодействия для ProductsView.xaml
    /// </summary>
    public partial class ProductsView : UserControl
    {
        StoreMainView StoremainView;

        public ProductsView()
        {
            InitializeComponent();
        }

        public void GetMainView(StoreMainView storeMainView)
        {
            StoremainView = storeMainView;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            StoremainView.AllCloseControls(2);

            StoremainView.txtSubCategoryName.Text = "";

            StoremainView.nameSubCategory.Visibility = Visibility.Hidden;
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
