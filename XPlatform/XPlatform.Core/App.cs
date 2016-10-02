using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace XPlatform.Core
{

	public class App : Xamarin.Forms.Application
	{
		public static ViewDictionary Views { get; private set;}

		static App(){
			Views = new ViewDictionary ();
		}

		public App()
		{
			MainPage = DeviceServices.Current.RootView;
		}
	}

	public class ViewDictionary : Dictionary<string, IView>
	{
		public IView RootView { get{ return MainView; } }
		private IView BookView { get; set; }
		private IView AuthorView { get; set; }
		private IView MainView { get; set; }

		public ViewDictionary(){
			BookView = new XPlatform.Core.Views.BookView ();
			AuthorView = new XPlatform.Core.Views.AuthorView ();
			MainView = new XPlatform.Core.Views.MainView ();

			this.Add ("BookView", BookView);
			this.Add ("AuthorView", AuthorView);
			this.Add ("MainView", MainView);
		}
	}

	public static class Colors
	{
		public static Color AlmostOrange = Color.FromHex ("#ffE95927");
	}

	public class DateTimeConverter : IValueConverter
	{

		#region IValueConverter implementation

		public object Convert (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
            return (value == null) ? value : (((DateTime)value).ToString("dd/MM/yyyy"));
		}

		public object ConvertBack (object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}

