using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace login_page.UI
{
    /// <summary>
    /// Логика взаимодействия для AddProductSubCategory.xaml
    /// </summary>
    public partial class AddProductSubCategoryWindow : Window
    {
        ProductSubCategoryView ProductubCategoryView;
        int CategoryId = 0;
        public AddProductSubCategoryWindow(ProductSubCategoryView productSubCategoryView, int categoryId)
        {
            InitializeComponent();
            ProductubCategoryView = productSubCategoryView;
            CategoryId = categoryId;
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

                MySqlCommand command = new MySqlCommand($"insert into product_sub_category(name, category_id)  values('{txtName.Text}', '{CategoryId}')", dB.GetConnection());
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
