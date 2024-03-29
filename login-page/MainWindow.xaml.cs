﻿using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Effects;

namespace login_page
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BlurEffect myEffect = new BlurEffect();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnExit_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        public void SetEffect()
        {
            myEffect.Radius = 5;
            Effect = myEffect;
        }

        public void RemoveEffect()
        {
            myEffect.Radius = 0;
            Effect = myEffect;
        }

        public void AllCloseControls(int i)
        {

            sign_in_view.Visibility = Visibility.Hidden;
            sign_up_view.Visibility = Visibility.Hidden;
            main_view.Visibility = Visibility.Hidden;
            store_main_view.Visibility = Visibility.Hidden;

            if (i == 1)
            {
                sign_in_view.Visibility = Visibility.Visible;
                sign_in_view.LoadWindow();
            }
            if (i == 2)
            {
                sign_up_view.Visibility = Visibility.Visible;
                sign_up_view.GetSignInPage(sign_in_view);
            }
            if (i == 3)
            {
                main_view.Visibility = Visibility.Visible;
                main_view.GetMainWindow(this);
            }
            if (i == 4)
            {
                store_main_view.Visibility = Visibility.Visible;
                store_main_view.GetMainWindow(this);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AllCloseControls(1);
        }
    }
}
