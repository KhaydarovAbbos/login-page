using login_page.Entities.Shops;
using login_page.Entities.User;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace login_page.UI
{
    /// <summary>
    /// Логика взаимодействия для ShopView.xaml
    /// </summary>
    public partial class ShopView : UserControl
    {
        public ShopView()
        {
            InitializeComponent();
        }

        public void WindowLoad()
        {

            DB dB = new DB();
            DataTable dtShops = new DataTable();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();

            dB.OpenConnection();

            MySqlCommand command = new MySqlCommand("select * from shops", dB.GetConnection());
            mySqlDataAdapter.SelectCommand = command;
            mySqlDataAdapter.Fill(dtShops);

            dB.CloseConnection();

            for (int i = 0; i < dtShops.Rows.Count; i++)
            {
                Border border = new Border();
                border.Background = Brushes.White;
                border.Width = 250;
                border.Height = 150;
                border.BorderBrush = Brushes.Gray;
                border.BorderThickness = new Thickness(1);
                border.Margin = new Thickness(10, 10, 0, 0);

                Grid grid = new Grid();
                ColumnDefinition c1 = new ColumnDefinition();
                c1.Width = new GridLength(200, GridUnitType.Star);
                ColumnDefinition c2 = new ColumnDefinition();
                c2.Width = new GridLength(50, GridUnitType.Star);
                grid.ColumnDefinitions.Add(c1);
                grid.ColumnDefinitions.Add(c2);

                TextBlock txt = new TextBlock();
                txt.Foreground = new SolidColorBrush(Colors.Gray);
                txt.FontSize = 25;
                txt.VerticalAlignment = VerticalAlignment.Center;
                txt.HorizontalAlignment = HorizontalAlignment.Center;
                txt.Text = $"{dtShops.Rows[i]["name"].ToString()}";
                txt.FontWeight = FontWeights.Bold;

                Grid.SetColumn(txt, 0);
                grid.Children.Add(txt);

                StackPanel stackPanel = new StackPanel();

                Button btnAdd = new Button();
                btnAdd.Width = 40;
                btnAdd.Height = 35;
                btnAdd.Background = Brushes.White;
                btnAdd.BorderBrush = Brushes.White;
                btnAdd.Margin = new Thickness(0, 10, 0, 0);
                btnAdd.Padding = new Thickness(0);
                btnAdd.Content = new Image
                {
                    Source = new BitmapImage(new Uri("../Images/add.png", UriKind.Relative)),
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = 20,
                    Height = 20

                };
                btnAdd.Click += new RoutedEventHandler(btnAdd_Click);

                Button btnDelete = new Button();
                btnDelete.Width = 40;
                btnDelete.Height = 35;
                btnDelete.Background = Brushes.White;
                btnDelete.BorderBrush = Brushes.White;
                btnDelete.Margin = new Thickness(0, 10, 0, 0);
                btnDelete.Padding = new Thickness(0);
                btnDelete.Content = new Image
                {
                    Source = new BitmapImage(new Uri("../Images/delete.png", UriKind.Relative)),
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = 20,
                    Height = 20

                };
                btnDelete.Click += new RoutedEventHandler(btnDelete_Click);

                Button btnEdit = new Button();
                btnEdit.Width = 40;
                btnEdit.Height = 35;
                btnEdit.Background = Brushes.White;
                btnEdit.BorderBrush = Brushes.White;
                btnEdit.Margin = new Thickness(0, 10, 0, 0);
                btnEdit.Padding = new Thickness(0);
                btnEdit.Content = new Image
                {
                    Source = new BitmapImage(new Uri("../Images/pencil.png", UriKind.Relative)),
                    VerticalAlignment = VerticalAlignment.Center,
                    Width = 20,
                    Height = 20

                };
                btnEdit.Click += new RoutedEventHandler(btnEdit_Click);

                stackPanel.Children.Add(btnAdd);
                stackPanel.Children.Add(btnEdit);
                stackPanel.Children.Add(btnDelete);
                Grid.SetColumn(stackPanel, 1);
                grid.Children.Add(stackPanel);

                border.Child = grid;

                panel.Children.Add(border);
            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
