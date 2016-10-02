using System;
using Xamarin.Forms.Platform.iOS;
using XPlatform.Core;
using Xamarin.Forms;
using XPlatform.Core.Controls;
using UIKit;

namespace XPlatform.Droid
{
	public class XLabelRenderer:LabelRenderer
	{
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
        
			XLabel label = (XLabel)e.NewElement;
			var control = Control as UILabel;

			if (label.FontStyle == "Bold") {
				control.Font = UIFont.BoldSystemFontOfSize (control.Font.FontDescriptor.Size.GetValueOrDefault(17));
			} else if (label.FontStyle == "Italic") {
				control.Font = UIFont.ItalicSystemFontOfSize (control.Font.FontDescriptor.Size.GetValueOrDefault(17));
			}
		}
	}
}

