using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;

namespace login_page.UI
{
    /// <summary>
    /// Логика взаимодействия для EditShop.xaml
    /// </summary>
    public partial class EditShopWindow : Window
    {
        ShopView shopView { get; set; }
        int ShopId;
        public EditShopWindow()
        {
            InitializeComponent();
        }

        public void WindowLoad(int id, ShopView shopView)
        {
            ShopId = id;
            DB dB = new DB();
            DataTable dtShops = new DataTable();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();

            dB.OpenConnection();

            MySqlCommand command = new MySqlCommand($"select name from shops where id = '{id}'", dB.GetConnection());
            mySqlDataAdapter.SelectCommand = command;
            mySqlDataAdapter.Fill(dtShops);

            dB.CloseConnection();

            if (dtShops.Rows.Count > 0 )
            {
                txtName.Text = dtShops.Rows[0]["name"].ToString();
            }

            this.shopView = shopView;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtName.Text == "")
                    return;

                DB dB = new DB();
                DataTable dtShops = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();

                dB.OpenConnection();

                MySqlCommand command = new MySqlCommand($"update shops set name = '{txtName.Text}' where id = '{ShopId}'", dB.GetConnection());
                command.ExecuteNonQuery();

                dB.CloseConnection();

                shopView.WindowLoad();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
