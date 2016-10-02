using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using XPlatform.Core;
using XPlatform.Core.Droid;
using System.IO;
using XPlatform.Core.Controls;
using XPlatform.Droid;
using XPlatform.Core.Managers;

[assembly: ExportRenderer (typeof (XLabel), typeof (XLabelRenderer))]

namespace XPlatform.Droid
{
	[Activity (Label = "XPlatform.Droid", Theme = "@style/MyTheme", MainLauncher = true)]
	public class MainActivity : FormsAppCompatActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			Forms.Init (this, bundle);
			this.Initialize ();

			FormsAppCompatActivity.ToolbarResource = Resource.Layout.toolbar;
			FormsAppCompatActivity.TabLayoutResource = Resource.Layout.tabs;
			LoadApplication (new App ());
		}
			
		public void Initialize()
		{
			DeviceServices.Current.RegisterService(
				new AndroidLocalStorageService(),
				new AndroidDataConnectionService(),
				new MessagingService(),
				new NavigationService());

			Database.Initialize ();
		}
	}
}


