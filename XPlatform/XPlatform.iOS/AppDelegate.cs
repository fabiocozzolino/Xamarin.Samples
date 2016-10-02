using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using XPlatform.Core;
using Xamarin.Forms;
using System.IO;
using XPlatform.Core.Controls;
using XPlatform.Core.Model;
using XPlatform.Core.Managers;
using XPlatform.Droid;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer (typeof (XLabel), typeof (XLabelRenderer))]

namespace XPlatform.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : FormsApplicationDelegate
	{
		// class-level declarations
		
		public override UIWindow Window {
			get;
			set;
		}

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			#if DEBUG
			Xamarin.Calabash.Start();
			#endif

			Xamarin.Forms.Forms.Init ();
			Initialize ();

			LoadApplication (new App ());

			return base.FinishedLaunching (application, launchOptions);
		}

		public void Initialize()
		{
			DeviceServices.Current.RegisterService(
				new iOSLocalStorageService(),
				new iOSDataConnectionService(),
				new MessagingService(),
				new NavigationService());

			Database.Initialize ();
		}
		
		// This method is invoked when the application is about to move from active to inactive state.
		// OpenGL applications should use this method to pause.
		public override void OnResignActivation (UIApplication application)
		{
		}
		
		// This method should be used to release shared resources and it should store the application state.
		// If your application supports background exection this method is called instead of WillTerminate
		// when the user quits.
		public override void DidEnterBackground (UIApplication application)
		{
		}
		
		// This method is called as part of the transiton from background to active state.
		public override void WillEnterForeground (UIApplication application)
		{
		}
		
		// This method is called when the application is about to terminate. Save data, if needed.
		public override void WillTerminate (UIApplication application)
		{
		}
	}
}

