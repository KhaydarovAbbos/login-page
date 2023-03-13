using System;
using System.IO;
using System.Windows;

namespace login_page
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string FilePath = Path.Combine(Environment.CurrentDirectory, @"Data\", "RememberData.txt");

    }
}
