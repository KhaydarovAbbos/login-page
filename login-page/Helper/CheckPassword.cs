using login_page.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace login_page.Helper
{
    public static class CheckPassword
    {
        public static PasswordScore CheckStrength(string password)
        {
            int score = 0;

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasMinimum8Chars = new Regex(@".{8,}");


            if (hasMinimum8Chars.IsMatch(password))
            {
                score++;
            }
            if (hasNumber.IsMatch(password))
            {
                score++;
            }
            if (hasUpperChar.IsMatch(password) || hasLowerChar.IsMatch(password))
            {
                score+=2;
            }
            


            //var isValidated = hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasMinimum8Chars.IsMatch(password);

            return (PasswordScore)score;
        }
    }
}
