using login_page.Entities.DbInfo;
using login_page.Entities.User;
using login_page.Helper;
using MaterialDesignThemes.Wpf;
using SQLite;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
            }
            else
            {
                userSignIns = ReadFileHelper.GetUsers();

                if (userSignIns != null)
                {
                    var user = userSignIns.Last();

                    if (user != null)
                    {
                        txtLogin.Text = user.Login;
                        txtPassword.Password = user.Password;
                        ckRememberMe.IsChecked = true;
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

                if (userSignIns.FirstOrDefault(x => x.Login == user.Login && x.Password == user.Password) == null)
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
                txtError.Text = "Invalid password.";
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
            if (isExistLogin && !isExistPassword)
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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password == "" )
            {
                txtPasswordCheck.Text = "Required";
                TextFieldAssist.SetUnderlineBrush(txtPassword, Brushes.Red);
                return;
            }
            if (txtPassword.Password.Length < 8 )
            {
                txtPasswordCheck.Text = "Minimum 8 characters are required";
                TextFieldAssist.SetUnderlineBrush(txtPassword, Brushes.Red);
                return;
            }
            if (txtPassword.Password != "" )
            {
                var response = CheckPassword.CheckStrength(txtPassword.Password);
                TextFieldAssist.SetUnderlineBrush(txtPassword, Brushes.Red);

                if (response == Enums.PasswordScore.NoChar)
                {
                    txtPasswordCheck.Text = "Must contain at least 1 letter";
                    return;
                }
                if (response == Enums.PasswordScore.NoNumber)
                {
                    txtPasswordCheck.Text = "Must contain at least 1 digit";
                    return;
                }
                if (response == Enums.PasswordScore.NoNumberAndChar)
                {
                    txtPasswordCheck.Text = "Must contain at least 1 digit and 1 letter";
                    return;
                }
                if (response == Enums.PasswordScore.Strong)
                {
                    txtPasswordCheck.Text = "";
                    TextFieldAssist.SetUnderlineBrush(txtPassword, Brushes.Green);
                }
            }
        }
    }
}
