using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XPlatform.Core.ViewModels;
using XPlatform.Core.Model;
using System.ComponentModel;

namespace XPlatform.Core.Views
{	
	public partial class BooksView : ContentPage, IView<BooksViewModel>
	{
		public BooksViewModel ViewModel {
			get{
				return this.BindingContext as BooksViewModel;
			}
			set{
				this.BindingContext = value;
			}
		}
	
		public BooksView ()
		{
			InitializeComponent ();
		}

		void SearchTextChanged (object sender, TextChangedEventArgs e)
		{
			var searchText = e.NewTextValue ?? "";

			if (!string.IsNullOrEmpty (e.NewTextValue)) {
				this.ViewModel.SearchCommand.Execute (searchText);
			} else {
				this.ViewModel.ResetSearchCommand.Execute (searchText);
			}
		}

		void ItemTapped(object sender, ItemTappedEventArgs e)
		{
			this.ViewModel.BookSelected (e.Item as Book);
		}
	}
}

