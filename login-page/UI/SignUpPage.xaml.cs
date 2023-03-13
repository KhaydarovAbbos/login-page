using login_page.Entities.User;
using login_page.Helper;
using MaterialDesignThemes.Wpf;
using MySql.Data.MySqlClient;
using SQLite;
using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
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
                    DB dB = new DB();
                    
                    dB.OpenConnection();

                    MySqlCommand command = new MySqlCommand($"insert into Users(Login, Password) values('{user.Login}', '{HashPassword.Create(user.Password)}')", dB.GetConnection());
                    command.ExecuteNonQuery();

                    dB.CloseConnection();

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
                txtLogin.Text = null;
                txtPassword.Password = null;
                txtConiformPassword.Password = null;
                txtError.Text = "";
                isClear= false;
            }
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (txtPassword.Password == "" && isClear == false)
            {
                txtPasswordCheck.Text = "Required";
                TextFieldAssist.SetUnderlineBrush(txtPassword, Brushes.Red);
                return;
            }
            if (txtPassword.Password.Length < 8 && isClear == false)
            {
                txtPasswordCheck.Text = "Minimum 8 characters are required";
                TextFieldAssist.SetUnderlineBrush(txtPassword, Brushes.Red);
                return;
            }
            if (txtPassword.Password != "" && isClear == false)
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

                    if (txtPassword.Password != txtConiformPassword.Password && txtConiformPassword.Password != "")
                    {
                        txtConiformPasswordCheck.Text = "Invalid coniform password";
                        TextFieldAssist.SetUnderlineBrush(txtConiformPassword, Brushes.Red);
                        txtConiformPasswordSucces.Visibility = Visibility.Hidden;
                    }
                    if(txtPassword.Password == txtConiformPassword.Password)
                    {
                        txtConiformPasswordCheck.Text = "";
                        TextFieldAssist.SetUnderlineBrush(txtConiformPassword, Brushes.Green);
                        txtConiformPasswordSucces.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private async Task<bool> CheckUser()
        {
            await Task.Delay(1000);
            User userLogin = new User();
            DB dB = new DB();
            DataTable dtUser = new DataTable();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
            

            dB.OpenConnection();

            MySqlCommand command = new MySqlCommand($"SELECT Id FROM Users WHERE Login = '{txtLogin.Text}'", dB.GetConnection());

            mySqlDataAdapter.SelectCommand = command;
            mySqlDataAdapter.Fill(dtUser);

            dB.CloseConnection();

            if (dtUser != null && dtUser.Rows.Count > 0)
                userLogin.Id = int.Parse(dtUser.Rows[0]["Id"].ToString());

            bool isExistLogin = userLogin != null && userLogin.Id != 0;

            if (isExistLogin)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void txtConiformPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            txtConiformPasswordSucces.Visibility = Visibility.Hidden;
            if (txtConiformPassword.Password == "" && isClear == false )
            {
                txtConiformPasswordCheck.Text = "Required";
                TextFieldAssist.SetUnderlineBrush(txtConiformPassword, Brushes.Red);
                return;
            }
            if (txtConiformPassword.Password.Length < 8 && isClear == false )
            {
                txtConiformPasswordCheck.Text = "Minimum 8 characters are required";
                TextFieldAssist.SetUnderlineBrush(txtConiformPassword, Brushes.Red);
                return;
            }
            if (txtConiformPassword.Password != "" && isClear == false)
            {
                var response = CheckPassword.CheckStrength(txtConiformPassword.Password);
                TextFieldAssist.SetUnderlineBrush(txtConiformPassword, Brushes.Red);

                if (response == Enums.PasswordScore.NoChar)
                {
                    txtConiformPasswordCheck.Text = "Must contain at least 1 letter";
                    return;
                }
                if (response == Enums.PasswordScore.NoNumber)
                {
                    txtConiformPasswordCheck.Text = "Must contain at least 1 digit";
                    return;
                }
                if (response == Enums.PasswordScore.NoNumberAndChar)
                {
                    txtConiformPasswordCheck.Text = "Must contain at least 1 digit and 1 letter";
                    return;
                }
                if (response == Enums.PasswordScore.Strong)
                {
                    txtConiformPasswordCheck.Text = "";
                    TextFieldAssist.SetUnderlineBrush(txtConiformPassword, Brushes.Green);
                    txtConiformPasswordSucces.Visibility = Visibility.Visible;
                }
                if(txtConiformPassword.Password != txtPassword.Password)
                {
                    txtConiformPasswordCheck.Text = "Invalid coniform password";
                    TextFieldAssist.SetUnderlineBrush(txtConiformPassword, Brushes.Red);
                    txtConiformPasswordSucces.Visibility = Visibility.Hidden;
                }
            }
        }

        private void ValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"[^a-zA-Z0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
