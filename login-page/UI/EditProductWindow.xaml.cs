using login_page.Entities.Products;
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
using System.Windows.Shapes;

namespace login_page.UI
{
    /// <summary>
    /// Логика взаимодействия для EditProductWindow.xaml
    /// </summary>
    public partial class EditProductWindow : Window
    {
        public EditProductWindow(Product product)
        {
            InitializeComponent();

            txtName.Text = product.Name;
            txtQuantity.Text = product.Quantity.ToString();
            txtSellingPrice.Text = product.Price.ToString();
            txtArrivalPrice.Text = product.ArrivalPrice.ToString();
        }


        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtArrivalPrice_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtSellingPrice_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
