using login_page.Entities.DbInfo;
using login_page.Entities.User;
using login_page.Helper;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.IO;
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
using System.Data.SqlClient;
using XAct.Users;
using System.Windows.Media.Effects;

namespace login_page.UI
{
    /// <summary>
    /// Interaction logic for SignInPage.xaml
    /// </summary>
    public partial class SignInPage : UserControl
    {
        MainWindow targetWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
        public UserSignIn userSign { get; set; }
        IList<UserSignIn> userSignIns = new List<UserSignIn>();

        public SignInPage()
        {
            InitializeComponent();
        }

        public void LoadWindow()
        {
            if (userSign != null)
            {
                txtLogin.Text = userSign.Login;
                txtPassword.Password = userSign.Password;
            };

            userSignIns = ReadFileHelper.GetUsers();

            if (userSignIns != null)
            {
                if (txtLogin.Items.Count > 0)
                {
                    txtLogin.Items.Clear();
                }

                foreach (var item in userSignIns)
                {
                    if (txtLogin.Items.Count == userSignIns.Count)
                    {
                        return;
                    }
                    else
                    {
                        txtLogin.Items.Add(item.Login);
                    }
                }
            }
        }

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtLogin.Text == "")
            {
                txtLogin.Focus();
                return;
            }
            if (txtPassword.Password == "")
            {
                txtPassword.Focus();
                return;
            }

            targetWindow.SetEffect();
            targetWindow.giff.Visibility = Visibility.Visible;

            if (ckRememberMe.IsChecked == true)
            {
                UserSignIn user = new UserSignIn()
                {
                    Login = txtLogin.Text,
                    Password = txtPassword.Password
                };

                if (!userSignIns.Contains(user))
                {
                    using (StreamWriter writer = new StreamWriter(App.FilePath, true))
                    {
                        writer.WriteLine(txtLogin.Text + " " + txtPassword.Password);
                    }
                }

            }

            var response = await CheckUser();

            if (response.Item1 && response.Item2)
            {
                targetWindow.AllCloseControls(3);
            }
            else if (response.Item1 && !response.Item2)
            {
                txtError.Text = "Invalid  password.";
            }
            else
            {
                txtError.Text = "Invalid username and password.";
            }

            targetWindow.RemoveEffect();
            targetWindow.giff.Visibility = Visibility.Hidden;
        }

        private async Task<(bool, bool)> CheckUser()
        {
            await Task.Delay(1000);
            DBInfo userLogin = new DBInfo();
            DBInfo userPassword = new DBInfo();

            using (SQLiteConnection connect = new SQLiteConnection(App.DatabasePath))
            {
                userLogin = connect.FindWithQuery<DBInfo>("SELECT * FROM DBInfo WHERE Login = @Login", txtLogin.Text);
                userPassword = connect.FindWithQuery<DBInfo>("SELECT * FROM DBInfo WHERE Password = @Password", HashPassword.Create(txtPassword.Password));
            }

            bool isExistLogin = userLogin != null;
            bool isExistPassword = userPassword != null;

            if (isExistLogin && isExistPassword)
            {
                return (true, true);
            }
            if(isExistLogin && !isExistPassword)
            {
                return (true, false);
            }
            else
            {
                return (false, false);
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            targetWindow.AllCloseControls(2);
        }

        private void txtLogin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ReadFileHelper.GetUsers();
        }

        private void txtLogin_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }

        private void txtLogin_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string login = txtLogin.SelectedItem.ToString();

            if (userSignIns.Count > 0 && login != "")
            {
                var user = userSignIns.FirstOrDefault(x => x.Login.Trim() == login.Trim());

                if (user != null)
                {
                    txtPassword.Password = user.Password;
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
