// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;

namespace PartialApp.iOS
{
	[Register ("PartialApp_iOSViewController")]
	partial class PartialApp_iOSViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel PlatformLabel { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (PlatformLabel != null) {
				PlatformLabel.Dispose ();
				PlatformLabel = null;
			}
		}
	}
}
