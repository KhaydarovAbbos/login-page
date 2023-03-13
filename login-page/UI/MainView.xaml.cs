﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace login_page.UI
{
    /// <summary>
    /// Логика взаимодействия для MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            if (MainGrid.ColumnDefinitions[0].Width.Value == 200)
            {
                MainGrid.ColumnDefinitions[0].Width = new GridLength(60, GridUnitType.Pixel);
                MainGrid.ColumnDefinitions[1].Width = new GridLength(1260, GridUnitType.Pixel);
            }
            else
            {
                MainGrid.ColumnDefinitions[0].Width = new GridLength(200, GridUnitType.Pixel);
                MainGrid.ColumnDefinitions[1].Width = new GridLength(1120, GridUnitType.Pixel);

                
            }
        }

        private void shops_MouseUp(object sender, MouseButtonEventArgs e)
        {
            AllCloseControls(1);
        }

        public void AllCloseControls(int i)
        {
            shop_view.Visibility = Visibility.Hidden;

            if (i == 1)
            {
                shop_view.Visibility = Visibility.Visible;
                shop_view.WindowLoad();
            }
        }

        private void settings_MouseUp(object sender, MouseButtonEventArgs e)
        {
            AllCloseControls(2);
        }
    }
}