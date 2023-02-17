using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace login_page.Entities.User
{
    public class UserSignUp
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Login { get; set; }

        public string Password { get; set; }

        public string ConiformPassword { get; set; }
    }
}
