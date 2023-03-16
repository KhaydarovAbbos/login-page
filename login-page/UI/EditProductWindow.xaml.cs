using login_page.Entities.Products;
using MySql.Data.MySqlClient;
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
        ProductsView Productsview;
        int productId;

        public EditProductWindow(Product product, ProductsView productsview)
        {
            InitializeComponent();

            productId = product.Id;
            txtName.Text = product.Name;
            txtQuantity.Text = product.Quantity.ToString();
            txtSellingPrice.Text = product.Price.ToString();
            txtArrivalPrice.Text = product.ArrivalPrice.ToString();
            Productsview = productsview;
        }


        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtName.Text.Length == 0 || txtName.Text == "")
                txtErrorName.Text = "Необходимый";
            else
                txtErrorName.Text = "";
        }

        private void txtArrivalPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtArrivalPrice.Text.Length == 0 || txtArrivalPrice.Text == "")
                txtErrorArrivalPrice.Text = "Необходимый";
            else
                txtErrorArrivalPrice.Text = "";
        }

        private void txtSellingPrice_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (txtSellingPrice.Text.Length == 0 || txtSellingPrice.Text == "")
                txtErrorSellingPrice.Text = "Необходимый";
            else
                txtErrorSellingPrice.Text = "";
        }

        private void txtQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtQuantity.Text.Length == 0 || txtQuantity.Text == "")
                txtErrorQuantity.Text = "Необходимый";
            else
                txtErrorQuantity.Text = "";
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == "")
            {
                txtErrorName.Text = "Необходимый";
                txtName.Focus();
                return;
            }
            if (txtArrivalPrice.Text == "")
            {
                txtErrorArrivalPrice.Text = "Необходимый";
                txtArrivalPrice.Focus();
                return;
            }
            if (txtSellingPrice.Text == "")
            {
                txtErrorSellingPrice.Text = "Необходимый";
                txtSellingPrice.Focus();
                return;
            }
            if (txtQuantity.Text == "")
            {
                txtErrorQuantity.Text = "Необходимый";
                txtQuantity.Focus();
                return;
            }

            DB dB = new DB();
            dB.OpenConnection();

            MySqlCommand command = new MySqlCommand($"update products set name = '{txtName.Text}', arrival_price = {txtArrivalPrice.Text}, selling_price = {txtSellingPrice.Text}, quantity = {txtQuantity.Text}  where id = {productId}", dB.GetConnection());
            command.ExecuteNonQuery();

            dB.CloseConnection();

            Productsview.WindowLoad();

            this.Close();

        }
    }
}
