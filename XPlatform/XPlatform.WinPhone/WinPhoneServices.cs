using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using XPlatform.Core;

namespace App1.WinPhone
{
    public class WinPhoneLocalStorageService : ILocalStorageService
    {
        public string DatabasePath
        {
            get { return ApplicationData.Current.LocalFolder.Path; }
        }
    }

    public class WinPhoneDataConnectionService : IDataConnectionService
    {
        public SQLite.Net.SQLiteConnection GetConnection(string databaseName)
        {
            var localStorage = DeviceServices.Current.GetService<ILocalStorageService>();
            var databasePath = System.IO.Path.Combine(localStorage.DatabasePath, databaseName);
            return new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.WindowsPhone8.SQLitePlatformWP8(), databasePath);
        }
    }
}
