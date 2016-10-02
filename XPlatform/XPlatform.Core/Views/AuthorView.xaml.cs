using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XPlatform.Core.Model;
using XPlatform.Core.ViewModels;

namespace XPlatform.Core.Views
{	
	public partial class AuthorView : ContentPage, IView<AuthorViewModel>
	{
		public AuthorViewModel ViewModel {
			get{
				return this.BindingContext as AuthorViewModel;
			}
			set{
				this.BindingContext = value;
			}
		}

		public AuthorView ()
		{
			InitializeComponent ();
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			this.ViewModel.InitializeViewModel ();
		}
	}
}

