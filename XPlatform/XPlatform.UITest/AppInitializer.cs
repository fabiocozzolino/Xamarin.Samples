using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace XPlatform.UITest
{
	public class AppInitializer
	{
		public static IApp StartApp (Platform platform)
		{
			// TODO: If the iOS or Android app being tested is included in the solution 
			// then open the Unit Tests window, right click Test Apps, select Add App Project
			// and select the app projects that should be tested.
			if (platform == Platform.Android) {
				return ConfigureApp
					.Android
				// TODO: Update this path to point to your Android app and uncomment the
				// code if the app is not included in the solution.
					.ApkFile ("../../../XPlatform.Droid/bin/Debug/XPlatform.Droid.apk")
					.StartApp ();
			}

			return ConfigureApp
				.iOS
				.PreferIdeSettings()
				.AppBundle ("../../../XPlatform.iOS/bin/iPhoneSimulator/Debug/XPlatformiOS.app")
				.StartApp ();
		}
	}
}

