using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace XPlatform.Core.ViewModels
{
    public abstract class ViewModel : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Dispose()
        {
            OnDisposed();
        }

        public event EventHandler Disposed;
        public void OnDisposed()
        {
            var handler = Disposed;
            if (handler != null) Disposed(this, new EventArgs());
        }

		public ViewModel()
		{
		}

        public virtual void InitializeViewModel()
        {
			this.Title = "";
        }

		private string _title;
		public string Title {
			get{
				return _title;
			}
			set{
				if (value != _title) {
					_title = value;
					OnPropertyChanged ();
				}
			}
		}
    }
}
