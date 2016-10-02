using System;
using System.Collections.Generic;
using Xamarin.Forms;
using XPlatform.Core.Model;
using XPlatform.Core.ViewModels;

namespace XPlatform.Core.Views
{	
	public partial class BookView : ContentPage, IView<BookViewModel>
	{
		public BookViewModel ViewModel {
			get{
				return this.BindingContext as BookViewModel;
			}
			set{
				this.BindingContext = value;
			}
		}

		public BookView ()
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

