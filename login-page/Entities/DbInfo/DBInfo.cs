﻿using SQLite;

namespace login_page.Entities.DbInfo
{
    public class DBInfo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public DBInfo()
        {

        }

        public DBInfo(string C1, string C2)
        {
            Login = C1;
            Password = C2;
        }
    }
}
