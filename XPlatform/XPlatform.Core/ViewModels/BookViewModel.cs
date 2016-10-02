using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using XPlatform.Core.Model;
using System.Windows.Input;

namespace XPlatform.Core.ViewModels
{
	
	public class BookViewModel : ViewModel {

		private string _authorName;
		public string AuthorName
		{
			get { return _authorName; }
			set
			{
				if (value == _authorName)
				{
					return;
				}

				_authorName = value;
				OnPropertyChanged();
			}
		}

        private string _bookDescription;
        public string BookDescription
        {
            get
            {
                return this._bookDescription;
            }
            set
            {
                if (value == this._bookDescription) return;
                this._bookDescription = value;
                this.OnPropertyChanged();
            }
        }

		private string _bookTitle;
		public string BookTitle
		{
			get
			{
				return this._bookTitle;
			}
			set
			{
				if (value == this._bookTitle) return;
				this._bookTitle = value;
				this.OnPropertyChanged();
			}
		}

        private string _bookTime;
        public string BookTime
        {
            get
            {
                return this._bookTime;
            }
            set
            {
                if (value == this._bookTime) return;
                this._bookTime = value;
                this.OnPropertyChanged();
            }
        }

		public override void InitializeViewModel ()
		{
			base.InitializeViewModel ();

			this.Title = "Dettaglio libro";
		}
	}
	
}
