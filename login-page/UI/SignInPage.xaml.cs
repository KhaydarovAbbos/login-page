using login_page.Entities.User;
using login_page.Helper;
using login_page.Service.Services;
using login_page.ViewModels;
using MaterialDesignThemes.Wpf;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
        UserService userService = new UserService();

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

                if (userSignIns != null && userSignIns.Count > 0)
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
                txtLoginCheck.Text = "Необходимый";

                txtLogin.Focus();
                return;
            }
            if (txtPassword.Password == "")
            {
                txtPasswordCheck.Text = "Необходимый";
                txtPassword.Focus();
                return;
            }

            targetWindow.SetEffect();
            targetWindow.giff.Visibility = Visibility.Visible;



            UserLoginViewModel userLoginViewModel = new UserLoginViewModel()
            {
                Login = txtLogin.Text,
                Password = txtPassword.Password

            };

            var response = await CheckUser();

            if (response.Item1 && response.Item2)
            {
                targetWindow.AllCloseControls(3);

                txtError.Text = "";

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
            userSign = null;
        }

        private async Task<(bool, bool)> CheckUser()
        {
            try
            {
                await Task.Delay(1000);
                User userLogin = new User();
                User userPassword = new User();

                DB dB = new DB();
                DataTable dtLogin = new DataTable();
                DataTable dtPassword = new DataTable();
                MySqlDataAdapter mySqlDataAdapterLogin = new MySqlDataAdapter();
                MySqlDataAdapter mySqlDataAdapterPassword = new MySqlDataAdapter();


                dB.OpenConnection();

                MySqlCommand cmdLogin = new MySqlCommand($"SELECT Id FROM Users WHERE Login = '{txtLogin.Text}'", dB.GetConnection());
                MySqlCommand cmdPassword = new MySqlCommand($"SELECT Id FROM Users WHERE Password = '{HashPassword.Create(txtPassword.Password)}'", dB.GetConnection());


                mySqlDataAdapterLogin.SelectCommand = cmdLogin;
                mySqlDataAdapterLogin.Fill(dtLogin);

                mySqlDataAdapterPassword.SelectCommand = cmdPassword;
                mySqlDataAdapterPassword.Fill(dtPassword);

                if (dtLogin != null)
                    userLogin.Id = int.Parse(dtLogin.Rows[0]["Id"].ToString());

                if (dtPassword != null)
                    userPassword.Id = int.Parse(dtPassword.Rows[0]["Id"].ToString());

                dB.CloseConnection();

                bool isExistLogin = userLogin != null || userLogin.Id != 0;
                bool isExistPassword = userPassword != null || userPassword.Id != 0;

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
            catch (System.Exception ex)
            {
                //MessageBox.Show(ex.Message, "Xatolik", MessageBoxButton.OK, MessageBoxImage.Error);
                return (false, false);
            }

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
            if (txtPassword.Password == "")
            {
                txtPasswordCheck.Text = "Необходимый";
                TextFieldAssist.SetUnderlineBrush(txtPassword, Brushes.Red);
                return;
            }
            if (txtPassword.Password.Length < 8)
            {
                txtPasswordCheck.Text = "Требуется минимум 8 символов";
                TextFieldAssist.SetUnderlineBrush(txtPassword, Brushes.Red);
                return;
            }
            if (txtPassword.Password != "")
            {
                var response = CheckPassword.CheckStrength(txtPassword.Password);
                TextFieldAssist.SetUnderlineBrush(txtPassword, Brushes.Red);

                if (response == Enums.PasswordScore.NoChar)
                {
                    txtPasswordCheck.Text = "Должен содержать хотя бы 1 букву";
                    return;
                }
                if (response == Enums.PasswordScore.NoNumber)
                {
                    txtPasswordCheck.Text = "Должен содержать хотя бы 1 цифру";
                    return;
                }
                if (response == Enums.PasswordScore.NoNumberAndChar)
                {
                    txtPasswordCheck.Text = "Должен содержать как минимум 1 цифру и 1 букву";
                    return;
                }
                if (response == Enums.PasswordScore.Strong)
                {
                    txtPasswordCheck.Text = "";
                    TextFieldAssist.SetUnderlineBrush(txtPassword, Brushes.Green);
                }
            }
        }

        private void txtLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtLogin.Text.Length > 0)
            {
                txtLoginCheck.Text = "";
            }
            if (txtLogin.Text.Length == 0)
            {
                txtLoginCheck.Text = "Необходимый";
            }
        }
    }
}
