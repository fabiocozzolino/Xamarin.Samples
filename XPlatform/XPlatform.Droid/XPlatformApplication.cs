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
using XPlatform.Core.Droid;
using XPlatform.Core;

namespace XPlatform.Droid
{
	[Application(Debuggable = true, 
		Theme = "@style/MyTheme",
		Label = "XPlatform")]
	public class XPlatformApplication : Application
	{
        public XPlatformApplication(IntPtr ptr, JniHandleOwnership ownership)
			: base(ptr, ownership)
		{
		}

		public override void OnCreate()
		{
			base.OnCreate();

//            this.Initialize();
		}
	}
}

