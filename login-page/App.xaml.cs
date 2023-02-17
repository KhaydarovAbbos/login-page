using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace login_page
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string FilePath = Path.Combine(Environment.CurrentDirectory, @"Data\", "RememberData.txt");

        static string DatabaseName = "LoginPage.db";
        static string FolderPath = Environment.CurrentDirectory;
        public static string DatabasePath = Path.Combine(FolderPath, @"Data\", DatabaseName);

    }
}
