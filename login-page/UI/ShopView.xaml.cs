using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace login_page.UI
{
    /// <summary>
    /// Логика взаимодействия для ShopView.xaml
    /// </summary>
    public partial class ShopView : UserControl
    {
        MainView MainView;
        public ShopView()
        {
            InitializeComponent();
        }

        public void GetMainView(MainView mainView)
        {
            MainView = mainView;
        }

        public void WindowLoad()
        {

            if (panel.Children.Count > 0)
            {
                panel.Children.Clear();
            }

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

                ///////////////////////////////////////////////
                TotalInfo totalInfo = new TotalInfo();
                totalInfo.store_id = dtShops.Rows[i]["id"].ToString();
                totalInfo.store_name = dtShops.Rows[i]["name"].ToString();
                ///////////////////////////////////////////////

                Border border = new Border
                {
                    Background = Brushes.White,
                    Width = 250,
                    Height = 150,
                    BorderBrush = Brushes.Gray,
                    BorderThickness = new Thickness(1),
                    Margin = new Thickness(10, 10, 0, 0),
                    CornerRadius = new CornerRadius(10)
                };

                ColumnDefinition c1 = new ColumnDefinition
                {
                    Width = new GridLength(200, GridUnitType.Star)
                };

                ColumnDefinition c2 = new ColumnDefinition
                {
                    Width = new GridLength(50, GridUnitType.Star)
                };

                TextBlock txt = new TextBlock
                {
                    Foreground = new SolidColorBrush(Colors.Gray),
                    FontSize = 25,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Text = $"{dtShops.Rows[i]["name"]}",
                    FontWeight = FontWeights.Bold
                };
                txt.MouseUp += new MouseButtonEventHandler(TextBlock_Click);

                Grid grid = new Grid 
                {
                    ColumnDefinitions = { c1, c2 },
                    Children = {txt}
                };

                MyButton btnDelete = new MyButton
                {
                    Width = 40,
                    Height = 35,
                    Background = Brushes.White,
                    BorderBrush = Brushes.White,
                    Margin = new Thickness(0, 10, 0, 0),
                    Padding = new Thickness(0),
                    Content = new Image
                    {
                        Source = new BitmapImage(new Uri("../Images/delete.png", UriKind.Relative)),
                        VerticalAlignment = VerticalAlignment.Center,
                        Width = 20,
                        Height = 20
                    }
                };
                btnDelete.Totalinfo = totalInfo;
                btnDelete.Click += new RoutedEventHandler(btnDelete_Click);

                MyButton btnEdit = new MyButton
                {
                    Width = 40,
                    Height = 35,
                    Background = Brushes.White,
                    BorderBrush = Brushes.White,
                    Margin = new Thickness(0, 40, 0, 0),
                    Padding = new Thickness(0),
                    Content = new Image
                    {
                        Source = new BitmapImage(new Uri("../Images/pencil.png", UriKind.Relative)),
                        VerticalAlignment = VerticalAlignment.Center,
                        Width = 20,
                        Height = 20

                    }

                };
                btnEdit.Totalinfo = totalInfo;
                btnEdit.Click += new RoutedEventHandler(btnEdit_Click);

                StackPanel stackPanel = new StackPanel
                {
                    Children = { btnEdit, btnDelete }
                };

                Grid.SetColumn(stackPanel, 1);
                grid.Children.Add(stackPanel);
                border.Child = grid;

                panel.Children.Add(border);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddShopWindow addShop = new AddShopWindow(this);
            addShop.ShowDialog();
        }

        private void TextBlock_Click(object sender, MouseEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;

            string name = textBlock.Text;

            StoreMainView.Storename = name;

            MainView.AllCloseControls(2);
        }

        public class TotalInfo
        {
            public string store_id { set; get; }
            public string store_name { set; get; }
        }

        public class MyButton : Button
        {
            public TotalInfo Totalinfo { set; get; }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MyButton btnDelete = sender as MyButton;

                int id = int.Parse(btnDelete.Totalinfo.store_id);

                if (id != 0) 
                {
                    DB dB = new DB();
                    dB.OpenConnection();

                    MySqlCommand command = new MySqlCommand($"delete from shops where id = '{id}'", dB.GetConnection());
                    command.ExecuteNonQuery();

                    dB.CloseConnection();

                    WindowLoad();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MyButton btnDelete = sender as MyButton;

                int id = int.Parse(btnDelete.Totalinfo.store_id);

                EditShopWindow editShop = new EditShopWindow();
                editShop.WindowLoad(id, this);
                editShop.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
