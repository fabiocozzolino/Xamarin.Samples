using System;
using Xamarin.Forms.Platform.Android;
using XPlatform.Core;
using Xamarin.Forms;
using Android.Graphics;
using XPlatform.Core.Controls;

namespace XPlatform.Droid
{
	public class XLabelRenderer:LabelRenderer
	{
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

			XLabel label = (XLabel)e.NewElement;

			var control = (global::Android.Widget.TextView)Control;
			control.Typeface = Typeface.CreateFromAsset (Context.Assets, "Fonts/Roboto-" + label.FontStyle + ".ttf");
		}
	}
}

