using login_page.Entities.Products;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
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
        Product Product;


        public EditProductWindow(Product product, ProductsView productsview)
        {
            InitializeComponent();

            productId = product.Id;
            txtName.Text = product.Name;
            txtQuantity.Text = product.Quantity.ToString();
            txtSellingPrice.Text = product.Price.ToString();
            txtArrivalPrice.Text = product.ArrivalPrice.ToString();
            txtBarcode.Text = product.Barcode.ToString();
            Productsview = productsview;
            Product = product;
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
            try
            {

                if (txtArrivalPrice.Text.Length == 0 || txtArrivalPrice.Text == "")
                    txtErrorArrivalPrice.Text = "Необходимый";
                else
                    txtErrorArrivalPrice.Text = "";
            }
            catch (Exception)
            {

            }

        }

        private void txtSellingPrice_TextChanged(object sender, TextChangedEventArgs e)
        {

            try
            {

                if (txtSellingPrice.Text.Length == 0 || txtSellingPrice.Text == "")
                    txtErrorSellingPrice.Text = "Необходимый";
                else
                    txtErrorSellingPrice.Text = "";
            }
            catch (Exception)
            {

            }
        }

        private void txtQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                


                if (txtQuantity.Text.Length == 0 || txtQuantity.Text == "")
                    txtErrorQuantity.Text = "Необходимый";
                else
                    txtErrorQuantity.Text = "";
            }
            catch (Exception)
            {
            }
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
            if (txtBarcode.Text == "")
            {
                txtErrorBarocde.Text = "Необходимый";
                txtBarcode.Focus();
                return;
            }

            DB dB = new DB();
            dB.OpenConnection();

            MySqlCommand command = new MySqlCommand($"update products set name = '{txtName.Text}', arrival_price = {double.Parse(txtArrivalPrice.Text)}, selling_price = {double.Parse(txtSellingPrice.Text)}, quantity = {double.Parse(txtQuantity.Text)}, barcode = '{txtBarcode.Text}'  where id = {productId}", dB.GetConnection());
            command.ExecuteNonQuery();

            dB.CloseConnection();

            Productsview.WindowLoad();

            this.Close();

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            try
            {
                TextBox textbox = (TextBox)sender;

                char ch = e.Text[0];

                if ((Char.IsDigit(ch) || ch == '.'))

                {

                    if (ch == '.' && textbox.Text.Contains('.'))

                        e.Handled = true;
                }

                else
                    e.Handled = true;

            }
            catch (Exception)
            {

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtCategory.Text = Productsview.StoremainView.txtcategoryName.Text;
            txtSubCategory.Text = Productsview.StoremainView.txtSubCategoryName.Text;
        }

        private void txtBarcode_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtBarcode.Text.Length == 0 || txtBarcode.Text == "")
                {
                    txtErrorBarocde.Text = "Необходимый";
                    txtBarcode.Focus();
                }
                else
                {
                    txtErrorBarocde.Text = "";
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
