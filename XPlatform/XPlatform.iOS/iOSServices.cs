using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using System.Reflection;
using XPlatform.Core;

namespace XPlatform.iOS
{
	public class iOSLocalStorageService : ILocalStorageService
	{
		public string DatabasePath
		{
			get { return Environment.GetFolderPath (Environment.SpecialFolder.Personal); }
		}
	}

	public class iOSDataConnectionService : IDataConnectionService
	{
		public SQLite.Net.SQLiteConnection GetConnection(string databaseName)
		{
			var localStorage = DeviceServices.Current.GetService<ILocalStorageService>();
			var databasePath = System.IO.Path.Combine(localStorage.DatabasePath, databaseName);
			return new SQLite.Net.SQLiteConnection(new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS(), databasePath);
		}
	}
}
