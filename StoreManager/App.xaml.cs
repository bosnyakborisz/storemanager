using System.Configuration;
using System.Data;
using System.Windows;

namespace StoreManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static string database = "storeManager.db";
        static string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string databasePath = System.IO.Path.Combine(path, database);
    }

}
