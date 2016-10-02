using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPlatform.Core.ViewModels;

namespace XPlatform.Core
{
	public interface IView
	{
	}

	public interface IView<TViewModel> : IView where TViewModel : ViewModel
    {
        TViewModel ViewModel { get; set; }
    }

    public interface IDeviceService
    {
    }

    public interface IDeviceDispatcher : IDeviceService
    {
        void Invoke(Action action);
    }

    public interface IDeviceNavigator : IDeviceService
    {
		Xamarin.Forms.Page RootView { get; }

        void Navigate<T>(T viewModel) where T : ViewModel;

    }

    public interface IMessagingService : IDeviceService
    {
        MessagingResult Ask(string message);
        void Inform(string message);
    }

    public interface INetworkService : IDeviceService
    {
        bool IsOnline();
    }

    public interface ISettingsService : IDeviceService
    {
        string Get(string name, bool secured = false);
        void Set(string name, string value, bool secured = false);
    }

    public interface IDataConnectionService : IDeviceService
    {
        SQLite.Net.SQLiteConnection GetConnection(string database);
    }

    public interface ILocalStorageService : IDeviceService
    {
        string DatabasePath { get; }
    }

    public enum MessagingResult
    {
        Ok,
        Cancel
    }
}
