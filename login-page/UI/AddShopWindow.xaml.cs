﻿using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace login_page.UI
{
    /// <summary>
    /// Логика взаимодействия для AddShop.xaml
    /// </summary>
    public partial class AddShopWindow : Window
    {
        ShopView Shopview { get; set; }

        public AddShopWindow(ShopView shopView)
        {
            InitializeComponent();
            Shopview = shopView;
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
