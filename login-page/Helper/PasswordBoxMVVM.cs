using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace login_page.Helper
{
    class PasswordBoxMVVM
    {
        public class PasswordViewModel : ViewModelBase
        {
            private bool isPasswordFieldEmpty;
            public bool IsPasswordFieldEmpty
            {
                get { return isPasswordFieldEmpty; }
                set
                {
                    isPasswordFieldEmpty = value;
                    RaisePropertyChanged();
                }
            }

            private SecureString password;
            public SecureString Password
            {
                get { return password; }
                set
                {
                    password = value;
                    RaisePropertyChanged();
                }
            }

            private bool isPassWordFieldDisabled;
            public bool IsPasswordFieldDisabled
            {
                get { return isPassWordFieldDisabled; }
                set
                {
                    isPassWordFieldDisabled = value;
                    RaisePropertyChanged();
                }
            }

            public ICommand ClickCommand { get { return new RelayCommand(doAction, canDoAction); } }
            public ICommand PasswordChangedCommand { get { return new RelayCommand(updatePassword, canUpdatePassword); } }

            public PasswordViewModel()
            {
                // Init conditions, need them to not get null reference at the start.
                isPassWordFieldDisabled = true;
                IsPasswordFieldEmpty = true;
            }

            private void doAction()
            {
                IsPasswordFieldDisabled = !IsPasswordFieldDisabled;
            }

            private bool canDoAction()
            {
                // Replace this with any condition that you need.
                return true;
            }

            private void updatePassword()
            {
                if (Password != null)
                {
                    if (Password.Length > 0)
                    {
                        isPasswordFieldEmpty = false;
                    }
                    else
                    {
                        isPasswordFieldEmpty = true;
                    }
                }
                else
                {
                    isPasswordFieldEmpty = true;
                }
            }

            private bool canUpdatePassword()
            {
                // Replace this with any condition that you need. 
                return true;
            }
        }

        class PasswordLengthToColorConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                int length = (int)value;
                Color output = Colors.Red;

                if (length >= 0 && length < 5)
                    output = Colors.Red;

                else if (length >= 5 && length < 6)
                    output = Colors.Orange;

                else if (length >= 6 && length < 7)
                    output = Colors.Yellow;

                else if (length >= 7 && length < 8)
                    output = Colors.LightGreen;

                else if (length >= 8)
                    output = Colors.Green;

                return output;
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotSupportedException();
            }
        }
    }
}
