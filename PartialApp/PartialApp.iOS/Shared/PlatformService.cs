using System;
using UIKit;

namespace PartialApp.Shared
{
	public partial class PlatformService
	{
		private string GetPlatformName()
		{
			return UIDevice.CurrentDevice.Name;
		}
	}
}

