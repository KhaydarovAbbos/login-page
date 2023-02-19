using login_page.Entities.DbInfo;
using login_page.Entities.User;
using login_page.Helper;
using SQLite;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace login_page.UI
{
    /// <summary>
    /// Interaction logic for SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : UserControl
    {
        MainWindow targetWindow = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
        public SignInPage signInPage { get; set; }
        bool isClear = false;
        public SignUpPage()
        {
            InitializeComponent();
        }

        public void GetSignInPage(SignInPage signInPage)
        {
            this.signInPage = signInPage;
        }
        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            targetWindow.AllCloseControls(1);
        }

        private async void BtnRegister_Click(object sender, RoutedEventArgs e)
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
            if (txtConiformPassword.Password == "")
            {
                txtConiformPassword.Focus();
                return;
            }
            if (txtPasswordCheck.Text == "" && txtPassword.Password == "")
            {
                txtPassword.Focus();
                return;
            }
            if (txtPasswordCheck.Text != "")
            {
                txtPassword.Focus();
                return;
            }
            if (txtPassword.Password != txtConiformPassword.Password)
            {
                txtError.Text = "Invalid coniform password";
                txtConiformPassword.Focus();
                return;
            }

            targetWindow.SetEffect();
            targetWindow.giff.Visibility = Visibility.Visible;
            UserSignUp user = new UserSignUp
            {
                Login = txtLogin.Text,
                Password = txtPassword.Password,
                ConiformPassword = txtConiformPassword.Password
            };

            var response = await CheckUser();

            if (response)
            {
                txtError.Text = "Login is already registered";
            }
            else
            {
                try
                {
                    DBInfo dBInfo = new DBInfo(user.Login, HashPassword.Create(user.Password));
                    var db = new SQLiteConnection(App.DatabasePath);
                    db.Insert(dBInfo);
                    db.Close();

                    signInPage.userSign = new UserSignIn
                    {
                        Login = user.Login,
                        Password = user.Password
                    };
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                targetWindow.AllCloseControls(1);
                isClear = true;
            }

            targetWindow.RemoveEffect();
            targetWindow.giff.Visibility = Visibility.Hidden;

            if (isClear)
            {
                txtLogin.Clear();
                txtPassword.Clear();
                txtConiformPassword.Clear();
                txtError.Text = "";
            }
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password == "")
            {
                txtPasswordCheck.Text = "Required";
                return;
            }
            if (txtPassword.Password.Length < 8)
            {
                txtPasswordCheck.Text = "Minimum 8 characters are required";
                return;
            }
            if (txtPassword.Password != "")
            {
                var response = CheckPassword.CheckStrength(txtPassword.Password);

                if (response == Enums.PasswordScore.NoChar)
                {
                    txtPasswordCheck.Text = "Must contain at least 1 letter";
                }
                if (response == Enums.PasswordScore.NoNumber)
                {
                    txtPasswordCheck.Text = "Must contain at least 1 digit";
                }
                if (response == Enums.PasswordScore.NoNumberAndChar)
                {
                    txtPasswordCheck.Text = "Must contain at least 1 digit and 1 letter";
                }
                if (response == Enums.PasswordScore.Strong)
                {
                    txtPasswordCheck.Text = "";
                }
            }
        }

        private async Task<bool> CheckUser()
        {
            await Task.Delay(1000);
            DBInfo userLogin = new DBInfo();

            using (SQLiteConnection connect = new SQLiteConnection(App.DatabasePath))
            {
                userLogin = connect.FindWithQuery<DBInfo>("SELECT * FROM DBInfo WHERE Login = @Login", txtLogin.Text);
            }

            bool isExistLogin = userLogin != null;

            if (isExistLogin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
