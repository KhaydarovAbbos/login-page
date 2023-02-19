using login_page.Enums;
using System.Text.RegularExpressions;

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
                score += 2;
            }

            return (PasswordScore)score;
        }
    }
}
