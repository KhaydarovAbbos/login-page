using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace login_page.UI
{
    /// <summary>
    /// Логика взаимодействия для EditProductSubCategoryWindow.xaml
    /// </summary>
    public partial class EditProductSubCategoryWindow : Window
    {
        ProductSubCategoryView ProductsubCategoryView;
        int subCategoryid = 0;

        public EditProductSubCategoryWindow(ProductSubCategoryView productSubCategoryView, int id)
        {
            InitializeComponent();

            ProductsubCategoryView = productSubCategoryView;
            subCategoryid = id;
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

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
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

                MySqlCommand command = new MySqlCommand($"update product_sub_category set name = '{txtName.Text}' where id = '{subCategoryid}'", dB.GetConnection());
                command.ExecuteNonQuery();

                dB.CloseConnection();

                ProductsubCategoryView.WindowLoad();
                this.Close();


            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DB dB = new DB();
            DataTable dtShops = new DataTable();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();

            dB.OpenConnection();

            MySqlCommand command = new MySqlCommand($"select name from product_sub_category where id = '{subCategoryid}'", dB.GetConnection());
            mySqlDataAdapter.SelectCommand = command;
            mySqlDataAdapter.Fill(dtShops);

            dB.CloseConnection();

            if (dtShops.Rows.Count > 0)
            {
                txtName.Text = dtShops.Rows[0]["name"].ToString();
            }
        }
    }
}
