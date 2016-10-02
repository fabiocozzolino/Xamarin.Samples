using System;
using Xamarin.Forms;

namespace XPlatform.Core
{
	public static class Extensions
	{
		public static IView<T> SetViewModel<T>(this IView view, T viewModel) where T:ViewModels.ViewModel
		{
			var typedView = ((IView<T>)view);
			typedView.ViewModel = viewModel;
			return typedView;
		}

		public static Page GetPage<T>(this T view) where T:IView
		{
			return view as Page;
		}
	}
}

