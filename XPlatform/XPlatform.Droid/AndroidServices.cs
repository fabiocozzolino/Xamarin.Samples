using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Reflection;
using Android.Net;
using Android.Preferences;
using System.IO.IsolatedStorage;
using XPlatform.Core.ViewModels;

namespace XPlatform.Core.Droid
{
   
    public class AndroidNetworkService : INetworkService
    {
        public bool IsOnline()
        {
            return true;

            // TODO ripristinare il corretto funzionamento
            var connectivityManager = (ConnectivityManager)Application.Context.GetSystemService(Application.ConnectivityService);
            var activeConnection = connectivityManager.ActiveNetworkInfo;
            return ((activeConnection != null) && activeConnection.IsConnected);
        }
    }

    public class AndroidSettingsService : ISettingsService
    {
        ISharedPreferences SharedPreferences;

        public AndroidSettingsService()
        {
            this.SharedPreferences = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
        }

        public string Get(string name, bool secured = false)
        {
            return SharedPreferences.GetString(name, "");
        }

        public void Set(string name, string value, bool secured = false)
        {
            var editor = SharedPreferences.Edit();
            editor.PutString(name, value);
            editor.Apply();
        }
    }

    public class AndroidLocalStorageService : ILocalStorageService
    {
        public string DatabasePath
        {
            get { return Android.App.Application.Context.FilesDir.AbsolutePath; /* Android.App.Application.Context.GetDatabasePath("SpesaSicura").AbsolutePath;*/ }
        }
    }

    public class AndroidDataConnectionService : IDataConnectionService
    {
        public SQLite.Net.SQLiteConnection GetConnection(string databaseName)
        {
            var localStorage = DeviceServices.Current.GetService<ILocalStorageService>();
            var databasePath = System.IO.Path.Combine(localStorage.DatabasePath, databaseName);
            return new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), databasePath);
        }
    }
}