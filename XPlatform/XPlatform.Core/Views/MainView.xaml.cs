using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XPlatform.Core.ViewModels;

namespace XPlatform.Core.Views
{	
	public partial class MainView : TabbedPage, IView<MainViewModel>
	{
		#region IView implementation


		public MainViewModel ViewModel {
			get {
				return this.BindingContext as MainViewModel;
			}
			set {
				this.BindingContext = value;
			}
		}


		#endregion

	
		public MainView ()
		{
			InitializeComponent ();


		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			this.ViewModel.InitializeViewModel ();

//			if (this.ViewModel == null) {
//				this.ViewModel = new MainViewModel ();
//
//				this.Children.Add (new BooksView (){ViewModel = new BooksViewModel()});
//				this.Children.Add (new AuthorsView (){ViewModel = new AuthorsViewModel()});
//			}
		}
	}
}

