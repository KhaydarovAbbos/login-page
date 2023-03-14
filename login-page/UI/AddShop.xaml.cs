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
    /// Логика взаимодействия для AddShop.xaml
    /// </summary>
    public partial class AddShop : Window
    {
        ShopView Shopview { get; set; }

        public AddShop(ShopView shopView)
        {
            InitializeComponent();
            Shopview = shopView;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtName.Text == "")
                    return;

                DB dB = new DB();
                DataTable dtShops = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();

                dB.OpenConnection();

                MySqlCommand command = new MySqlCommand($"insert into shops(name)  values('{txtName.Text}')", dB.GetConnection());
                command.ExecuteNonQuery();

                dB.CloseConnection();

                Shopview.WindowLoad();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
        }
    }
}
