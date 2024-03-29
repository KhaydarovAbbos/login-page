﻿using login_page.Entities.Products;
using MySql.Data.MySqlClient;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace login_page.UI
{
    /// <summary>
    /// Логика взаимодействия для AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        ProductCategory Productcategory { get; set; }
        ProductSubCategory ProductSubcategory { get; set; }
        ProductsView Productsview;

        public AddProductWindow(ProductCategory category, ProductSubCategory subCategory)
        {
            InitializeComponent();

            Productcategory = category;
            ProductSubcategory = subCategory;

            txtCategory.Text = category.Name;
            txtSubCategory.Text = subCategory.Name;
        }

        public void GetProductsView(ProductsView productsView)
        {
            Productsview = productsView;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
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
            if (autoBarcodeGrid.Visibility == Visibility.Visible)
            {
                byte[] encoded = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(txtName.Text));
                var value = BitConverter.ToUInt32(encoded, 0) % 100000;
                //txtBarcode.Text = txtName.Text[0].ToString() + value.ToString();
                txtBarcode.Text = value.ToString();
            }
            if (barcodeGrid.Visibility == Visibility.Visible)
            {
                txtErrorBarocde.Text = "Необходимый";
                txtBarcode.Focus();
                return;
            }

            DB dB = new DB();
            dB.OpenConnection();

            MySqlCommand command = new MySqlCommand($"insert into products(name, arrival_price, selling_price, quantity, sub_category_id, store_id, barcode)  values('{txtName.Text}', {double.Parse(txtArrivalPrice.Text)}, {double.Parse(txtSellingPrice.Text)}, {double.Parse(txtQuantity.Text)},  {ProductSubcategory.Id}, {Productsview.StoremainView.store_id.Content}, '{txtBarcode.Text}')", dB.GetConnection());
            command.ExecuteNonQuery();

            dB.CloseConnection();

            Productsview.WindowLoad();

            this.Close();
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtName.Text.Length == 0 || txtName.Text == "")
            {
                txtErrorName.Text = "Необходимый";
                txtName.Focus();
            }
            else
            {
                txtErrorName.Text = "";
            }
        }

        private void txtArrivalPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtArrivalPrice.Text.Length == 0 || txtArrivalPrice.Text == "")
                {
                    txtErrorArrivalPrice.Text = "Необходимый";
                    txtArrivalPrice.Focus();
                }
                else
                {
                    txtErrorArrivalPrice.Text = "";
                }
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
                {
                    txtErrorSellingPrice.Text = "Необходимый";
                    txtSellingPrice.Focus();
                }
                else
                {
                    txtErrorSellingPrice.Text = "";
                }
            }
            catch (Exception)
            {

            }
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

        private void txtQuantity_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtQuantity.Text.Length == 0 || txtQuantity.Text == "")
                {
                    txtErrorQuantity.Text = "Необходимый";
                    txtQuantity.Focus();
                }
                else
                {
                    txtErrorQuantity.Text = "";
                }
            }
            catch (Exception)
            {
            }
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

        private void ckBarcode_Unchecked(object sender, RoutedEventArgs e)
        {
            txtBarcode.Clear();
            txtBarcode.Focus();

            autoBarcodeGrid.Visibility = Visibility.Visible;
            barcodeGrid.Visibility = Visibility.Hidden;

            ckAutoBarcode.IsChecked = false;
            txtErrorBarocde.Text = "";
        }

        private void txtBarcode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ckAutoBarcode_Checked(object sender, RoutedEventArgs e)
        {
            autoBarcodeGrid.Visibility = Visibility.Hidden;
            barcodeGrid.Visibility = Visibility.Visible;
            ckBarcode.IsChecked = true;
        }
    }
}
