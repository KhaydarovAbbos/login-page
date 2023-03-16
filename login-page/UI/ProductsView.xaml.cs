using MySql.Data.MySqlClient;
using System.Data;
using System;
using System.Windows;
using System.Windows.Controls;
using static login_page.UI.ShopView;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using login_page.Entities.Products;

namespace login_page.UI
{
    /// <summary>
    /// Логика взаимодействия для ProductsView.xaml
    /// </summary>
    public partial class ProductsView : UserControl
    {

        StoreMainView StoremainView;
        Product product;

        public ProductsView()
        {
            InitializeComponent();
        }

        public void WindowLoad()
        {
            if (panel.Children.Count > 0)
            {
                panel.Children.Clear();
            }

            DB dB = new DB();
            DataTable dtProducts = new DataTable();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();

            dB.OpenConnection();

            MySqlCommand command = new MySqlCommand("select * from products order by id desc", dB.GetConnection());
            mySqlDataAdapter.SelectCommand = command;
            mySqlDataAdapter.Fill(dtProducts);

            dB.CloseConnection();

            


            #region Button add
            Border borderAdd = new Border
            {
                Background = Brushes.White,
                Width = 250,
                Height = 150,
                BorderBrush = Brushes.Gray,
                BorderThickness = new Thickness(1),
                Margin = new Thickness(10, 10, 0, 0),
                CornerRadius = new CornerRadius(10),
            };

            MyButton buttonAdd = new MyButton
            {
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                Content = "+ Добавить",
                FontWeight = FontWeights.Bold,
                FontSize = 25
            };
            buttonAdd.Click += new RoutedEventHandler(AddBtn_Click);

            borderAdd.Child = buttonAdd;

            panel.Children.Add(borderAdd);

            #endregion

            for (int i = 0; i < dtProducts.Rows.Count; i++)
            {

                product = new Product()
                {
                    Id = int.Parse(dtProducts.Rows[i]["id"].ToString()),
                    Name = dtProducts.Rows[i]["name"].ToString(),
                    ArrivalPrice = double.Parse(dtProducts.Rows[i]["arrival_price"].ToString()),
                    Price = double.Parse(dtProducts.Rows[i]["selling_price"].ToString()),
                    Quantity = double.Parse(dtProducts.Rows[i]["quantity"].ToString()),
                    Category = dtProducts.Rows[i]["category_id"].ToString(),
                    SubCategory = dtProducts.Rows[i]["sub_category_id"].ToString()

                };


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

                #region ColumnDefinitions

                ColumnDefinition c1 = new ColumnDefinition
                {
                    Width = new GridLength(200, GridUnitType.Star)
                };

                ColumnDefinition c2 = new ColumnDefinition
                {
                    Width = new GridLength(50, GridUnitType.Star)
                };
                #endregion

                #region Product Info
                TextBlock txtProductName = new TextBlock
                {
                    Background = Brushes.Transparent,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(10, 10, 0, 0),
                    Text = product.Name,
                    TextWrapping = TextWrapping.Wrap,
                    FontWeight = FontWeights.Bold,
                    FontSize = 25,
                    Width = 200,
                    Height = 70
                };

                TextBlock txtArrivalPrice = new TextBlock
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontSize = 16,
                    Text = $"Себестоимость : {string.Format("{0:#,##}", product.ArrivalPrice)}",
                    Margin = new Thickness(10, 0, 0, 0)
                };

                TextBlock txtSellingPrice = new TextBlock
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontSize = 16,
                    Text = $"Цена : {string.Format("{0:#,##}", product.Price)}",
                    Margin = new Thickness(10, 0, 0, 0)
                };

                TextBlock txtQuantity = new TextBlock
                {
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontSize = 16,
                    
                    Text = $"Количество : {string.Format("{0:#,##}", product.Quantity)}",
                    Margin = new Thickness(10, 0, 0, 0)
                };

                StackPanel stackPanelRow1 = new StackPanel
                {
                    Children = { txtProductName, txtArrivalPrice , txtSellingPrice, txtQuantity}
                };
                #endregion

                Grid grid = new Grid
                {
                    ColumnDefinitions = { c1, c2 },
                    Children = { stackPanelRow1 }
                };

                #region  Buttons edit and delete

                ProductButton btnDelete = new ProductButton
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
                btnDelete.Product = product;
                btnDelete.Click += new RoutedEventHandler(btnDelete_Click);

                ProductButton btnEdit = new ProductButton
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
                btnEdit.Product = product;
                btnEdit.Click += new RoutedEventHandler(btnEdit_Click);

                StackPanel stackPanel = new StackPanel
                {
                    Children = { btnEdit, btnDelete }
                };

                #endregion

                Grid.SetColumn(stackPanel, 1);
               
                grid.Children.Add(stackPanel);
                border.Child = grid;

                panel.Children.Add(border);
            }
        }

        public void GetMainView(StoreMainView storeMainView)
        {
            StoremainView = storeMainView;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            StoremainView.AllCloseControls(2);

            StoremainView.txtSubCategoryName.Text = "";

            StoremainView.nameSubCategory.Visibility = Visibility.Hidden;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить", "Осторожность", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                
                if (result == MessageBoxResult.Yes) 
                {
                    ProductButton btnDelete = sender as ProductButton;

                    int id = btnDelete.Product.Id;

                    if (id != 0)
                    {
                        DB dB = new DB();
                        dB.OpenConnection();

                        MySqlCommand command = new MySqlCommand($"delete from products where id = '{id}'", dB.GetConnection());
                        command.ExecuteNonQuery();

                        dB.CloseConnection();

                        WindowLoad();
                    }
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
                ProductButton btnDelete = sender as ProductButton;

                Product product = btnDelete.Product;

                EditProductWindow editProductWindow = new EditProductWindow(product, this);
                editProductWindow.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                DB dB = new DB();
                dB.OpenConnection();
                DataTable dtCategoryId = new DataTable();
                DataTable dtSubCategoryId = new DataTable();
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();


                MySqlCommand cmdCategory = new MySqlCommand($"select * from product_category where name = '{StoremainView.txtcategoryName.Text}'", dB.GetConnection());
                MySqlCommand cmdSubCategory = new MySqlCommand($"select * from product_sub_category where name = '{StoremainView.txtSubCategoryName.Text}'", dB.GetConnection());

                mySqlDataAdapter.SelectCommand = cmdCategory;
                mySqlDataAdapter.Fill(dtCategoryId);

                mySqlDataAdapter.SelectCommand = cmdSubCategory;
                mySqlDataAdapter.Fill(dtSubCategoryId);

                dB.CloseConnection();

                ProductCategory category = new ProductCategory()
                {
                    Id = int.Parse(dtCategoryId.Rows[0]["id"].ToString()),
                    Name = dtCategoryId.Rows[0]["name"].ToString()
                };

                ProductSubCategory subCategory = new ProductSubCategory()
                {
                    Id = int.Parse(dtSubCategoryId.Rows[0]["id"].ToString()),
                    Name = dtSubCategoryId.Rows[0]["name"].ToString()
                };

                AddProductWindow addProductWindow = new AddProductWindow(category, subCategory);
                addProductWindow.GetProductsView(this);
                addProductWindow.ShowDialog();

            }
            catch (Exception)
            {

                throw;
            }




            
        }
    }

    public class ProductButton : Button
    {
        public Product Product { get; set; }
    }
}
