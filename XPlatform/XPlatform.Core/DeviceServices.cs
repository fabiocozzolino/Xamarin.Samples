using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using XPlatform.Core.ViewModels;

namespace XPlatform.Core
{
    public class DeviceServices
    {
        private static DeviceServices _current;
        public static DeviceServices Current
        {
            get
            {
                if (_current == null)
                {
                    _current = new DeviceServices();
                }
                return _current;
            }
        }

        private IDictionary<Type, IDeviceService> _services;

        private IDeviceNavigator Navigator
        {
            get
            {
                return GetService<IDeviceNavigator>();
            }
        }

        private IDeviceDispatcher Dispatcher
        {
            get
            {
                return GetService<IDeviceDispatcher>();
            }
        }

        public ISettingsService Settings
        {
            get
            {
                return GetService<ISettingsService>();
            }
        }

        public ILocalStorageService Storage
        {
            get
            {
                return GetService<ILocalStorageService>();
            }
        }

        public void RegisterNavigator(IDeviceNavigator workflow)
        {
            if (workflow == null) return;
            RegisterService(workflow);
        }

        public void RegisterDispatcher(IDeviceDispatcher dispatcher)
        {
            if (dispatcher == null) return;
            RegisterService(dispatcher);
        }

        public void RegisterService(params IDeviceService[] services)
        {
            if (_services == null) _services = new Dictionary<Type, IDeviceService>();

            foreach (var service in services)
            {
                if (!_services.ContainsKey(service.GetType()))
                {
                    _services.Add(service.GetType(), service);
                }
                else
                {
                    _services[service.GetType()] = service;
                }
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void Navigate<T>(T viewModel) where T : ViewModel
        {
            if (this.Navigator == null)
                return;

            this.Navigator.Navigate(viewModel);
        }

		public Xamarin.Forms.Page RootView{
			get{
				return this.Navigator.RootView;
			}
		}

        public void Dispatch(Action action)
        {
            if (Dispatcher == null)
                return;

            this.Dispatcher.Invoke(action);
        }

        public TService GetService<TService>() where TService : IDeviceService
        {
            if (_services == null) return default(TService);
            var typeInfo = typeof(TService).GetTypeInfo();
            var candidates = _services.Select(s => s.Value).Where(s => typeInfo.IsAssignableFrom(s.GetType().GetTypeInfo()));
            return (TService)candidates.Single();
        }
    }
}
