using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XPlatform.Core.ViewModels;
using XPlatform.Core.Model;

namespace XPlatform.Core.Views
{
	public partial class AuthorsView : ContentPage, IView<AuthorsViewModel>
	{
		#region IView implementation

		public AuthorsViewModel ViewModel {
			get {
				return this.BindingContext as AuthorsViewModel;
			}
			set {
				this.BindingContext = value;
			}
		}

		#endregion

		public AuthorsView ()
		{
			InitializeComponent ();
		}

		void AuthorItemTapped(object sender, ItemTappedEventArgs e)
		{
			this.ViewModel.AuthorSelected (e.Item as Author);
		}
	}
}

