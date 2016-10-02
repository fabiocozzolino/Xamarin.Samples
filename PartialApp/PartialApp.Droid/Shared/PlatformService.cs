using System;
using Android.App;

namespace PartialApp.Shared
{
	public partial class PlatformService
	{
		private string GetPlatformName()
		{
			return Android.OS.Build.Model;
		}
	}
}

