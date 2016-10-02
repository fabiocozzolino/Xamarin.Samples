using System;
using NUnit.Framework;
using Xamarin.UITest.iOS;
using System.IO;
using System.Reflection;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using System.Linq;

namespace XPlatform.UITest
{
//	[TestFixture (Platform.Android)]
	[TestFixture (Platform.iOS)]
	public class SearchBooksTest
	{
		IApp app;
		Platform platform;

		public SearchBooksTest (Platform platform)
		{
			this.platform = platform;
		}

		[SetUp]
		public void BeforeEachTest ()
		{
			app = AppInitializer.StartApp (platform);
		}

		[Test]
		public void SearchBooks_ValidBook()
		{
			Func<AppQuery, AppQuery> searchBar = c => c.Class ("UISearchBar");
			Func<AppQuery, AppQuery> searchResults = c => c.Class ("UITableViewLabel");

			app.EnterText (searchBar, "1984");
			app.PressEnter ();

			var results = app.Query(searchResults);
			Assert.IsTrue (results.Any (r => r.Text == "1984"));
		}

		[Test]
		public void SearchBooksAndGoToDetail()
		{
			app.EnterText (c => c.Class ("UISearchBar"), "1984");
			app.PressEnter ();

			app.Tap (c => c.Class ("UITableViewLabel").Text ("1984"));

			var result = app.Query (c => c.Text ("Dettaglio libro")).Any ();
			Assert.IsTrue (result);
		}
	}
}

