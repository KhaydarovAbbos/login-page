using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using System.Xml.Linq;
using static login_page.UI.ShopView;

namespace login_page.UI
{
    /// <summary>
    /// Логика взаимодействия для ProductCategory.xaml
    /// </summary>
    public partial class ProductCategoryView : UserControl
    {
        StoreMainView StoremainView { get; set; }

        public ProductCategoryView()
        {
            InitializeComponent();
        }

        public void GetMainView(StoreMainView  storeMainView)
        {
            StoremainView = storeMainView;

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

            MySqlCommand command = new MySqlCommand("select * from product_category order by id desc", dB.GetConnection());
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
                txt.MouseUp += new MouseButtonEventHandler(txt_Mouseup);

                Grid grid = new Grid
                {
                    ColumnDefinitions = { c1, c2 },
                    Children = { txt }
                };

                MyButton btnDelete = new MyButton
                {
                    Width = 40,
                    Height = 35,
                    Background = Brushes.White,
                    BorderBrush = Brushes.White,
                    Margin = new Thickness(0, 10, 0, 0),
                    VerticalAlignment = VerticalAlignment.Center,
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
                    VerticalAlignment = VerticalAlignment.Center,
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

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            StoremainView.txtcategoryName.Text = "";
            StoremainView.nameCategory.Visibility = Visibility.Hidden;
            StoremainView.AllCloseControls(2);
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

                    MySqlCommand command = new MySqlCommand($"delete from product_category where id = '{id}'", dB.GetConnection());
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

                EditProductCategoryWindow editProductCategoryWindow = new EditProductCategoryWindow(this, id);
                editProductCategoryWindow.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txt_Mouseup(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;

            string name = textBlock.Text;
            StoremainView.txtcategoryName.Text = name;
            StoremainView.nameCategory.Visibility = Visibility.Visible;

            StoremainView.AllCloseControls(2);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddProductcategoryWindow addProductcategory = new AddProductcategoryWindow(this);
            addProductcategory.ShowDialog();
        }
    }
}
