using System;

namespace PartialApp.Shared
{
	public partial class PlatformService
	{
		public string PlatformName
		{
			get {
				return "Hello, I'm a " + GetPlatformName ();
			}
		}
	}
}

