using System;
using Xamarin.Forms;

namespace XPlatform.Core
{
	public class NavigationService : IDeviceNavigator
	{
		internal NavigationPage Navigator;
		public NavigationService ()
		{
			Navigator = new NavigationPage (App.Views.RootView.GetPage());
			Navigator.BarBackgroundColor = Color.FromRgb(233,30,99);
			Navigator.BarTextColor = Color.White;
		}

		#region IDeviceNavigator implementation

		public Page RootView {
			get {
				return Navigator;
			}
		}

		public void Navigate<T> (T viewModel) where T : XPlatform.Core.ViewModels.ViewModel
		{
			var viewName = viewModel.GetType ().Name.Replace ("ViewModel", "View");
			Navigator.PushAsync (App.Views [viewName]
				.SetViewModel(viewModel)
				.GetPage());
		}

		#endregion
	}

	public class MessagingService : IMessagingService
	{
		public NavigationService Navigation;
		public MessagingService()
		{
			Navigation = DeviceServices.Current.GetService<IDeviceNavigator> () as NavigationService;

		}

		#region IMessagingService implementation
		public MessagingResult Ask (string message)
		{
			var result = this.Navigation.Navigator.CurrentPage.DisplayAlert ("", message, "Ok", "Cancel").Result;
			return result ? MessagingResult.Ok : MessagingResult.Cancel;
		}
		public void Inform (string message)
		{
			this.Navigation.Navigator.CurrentPage.DisplayAlert ("", message, "si", "");

		}
		#endregion
	}
}

