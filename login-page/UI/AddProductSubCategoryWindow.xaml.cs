﻿using login_page.Entities.Products;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для AddProductSubCategory.xaml
    /// </summary>
    public partial class AddProductSubCategoryWindow : Window
    {
        ProductSubCategoryView ProductubCategoryView;
        public AddProductSubCategoryWindow(ProductSubCategoryView productSubCategoryView)
        {
            InitializeComponent();
            ProductubCategoryView = productSubCategoryView;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtName.Text.Trim().Length == 0)
                {
                    txtError.Text = "Необходимый";
                    return;
                }

                DB dB = new DB();
                DataTable dtShops = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();

                dB.OpenConnection();

                MySqlCommand command = new MySqlCommand($"insert into product_sub_category(name)  values('{txtName.Text}')", dB.GetConnection());
                command.ExecuteNonQuery();

                dB.CloseConnection();

                ProductubCategoryView.WindowLoad();

                this.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                txtError.Text = "Необходимый";
                return;
            }

            txtError.Text = "";
        }
    }
}
