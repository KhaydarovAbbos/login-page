using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace login_page.Enums
{
    public enum PasswordScore
    {
        NoNumberAndChar = 1,
        NoNumber = 3,
        NoChar = 2,
        Strong = 4
    }
}
