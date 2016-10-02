using System;
using System.Collections.ObjectModel;
using XPlatform.Core.Model;
using XPlatform.Core.Managers;

namespace XPlatform.Core.ViewModels
{
	public class AuthorsViewModel: ViewModel
	{
		private ObservableCollection<Author> _authors;
		public ObservableCollection<Author> Authors
		{
			get { return _authors; }
			set
			{
				if (value == _authors)
				{
					return;
				}

				_authors = value;
				OnPropertyChanged();
			}
		}

		public override void InitializeViewModel ()
		{
			base.InitializeViewModel ();

			this.Title = "Autori";
			this.Authors = new ObservableCollection<Author> ();

			this.Load ();
		}

		public void AuthorSelected(int index)
		{
			AuthorSelected (Authors [index]);
		}

		public void AuthorSelected(Author author)
		{
			if (author == null)
				return;

			DeviceServices.Current.Navigate(new AuthorViewModel()
				{
					Author = author
				});
		}

		public void Load()
		{
			this.Authors.Clear();

			Database.Authors.ForEach(this.Authors.Add);
		}
	}

}

