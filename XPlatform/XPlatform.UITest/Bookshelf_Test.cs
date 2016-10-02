using System;
using NUnit.Framework;
using Xamarin.UITest;

namespace XPlatform.UITest
{
	[TestFixture (Platform.Android)]
	public class Bookshelf_Test
	{
		IApp app;
		Platform platform;

		public Bookshelf_Test (Platform platform)
		{
			this.platform = platform;
		}

		[SetUp]
		public void BeforeEachTest ()
		{
			app = AppInitializer.StartApp (platform);
		}

		[Test]
		public void NewTest ()
		{
			app.Tap(x => x.Class("TextView").Text("George Orwell"));
			app.Screenshot("Tapped on view TextView with Text: 'George Orwell'");
			app.Tap(x => x.Class("ImageButton"));
			app.Screenshot("Tapped on view ImageButton");
			app.Tap(x => x.ClassFull("android.widget.SearchView$SearchAutoComplete").Id("search_src_text"));
			app.Screenshot("Tapped on view SearchView$SearchAutoComplete with ID: 'search_src_text'");
			app.EnterText(x => x.ClassFull("android.widget.SearchView$SearchAutoComplete").Id("search_src_text"), "451");
			app.Screenshot("Entered '451' into view SearchView$SearchAutoComplete with ID: 'search_src_text'");
			app.Tap(x => x.Class("TextView").Text("Ray Bradbury"));
			app.Screenshot("Tapped on view TextView with Text: 'Ray Bradbury'");
			app.Tap(x => x.Class("ImageButton"));
			app.Screenshot("Tapped on view ImageButton");
			app.Tap(x => x.ClassFull("android.support.design.widget.TabLayout$TabView").Index(1));
			app.Screenshot("Tapped on view TabLayout$TabView");
			app.Tap(x => x.Class("TextView").Text("Ray Bradbury"));
			app.Screenshot("Tapped on view TextView with Text: 'Ray Bradbury'");
			app.Tap(x => x.Class("ImageButton"));
			app.Screenshot("Tapped on view ImageButton");
			app.Tap(x => x.Class("ImageButton"));
			app.Screenshot("Tapped on view ImageButton");
			app.Tap(x => x.ClassFull("android.support.design.widget.TabLayout$TabView").Index(0));
			app.Screenshot("Tapped on view ImageView");
			app.ClearText(x => x.ClassFull("android.widget.SearchView$SearchAutoComplete").Id("search_src_text").Text("451"));
		}
	}
}

