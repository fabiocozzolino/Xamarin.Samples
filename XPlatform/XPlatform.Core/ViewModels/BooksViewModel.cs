using System;
using System.Collections.ObjectModel;
using XPlatform.Core.Model;
using XPlatform.Core.Managers;
using System.Windows.Input;

namespace XPlatform.Core.ViewModels
{
	public class BooksViewModel: ViewModel
	{
		private ObservableCollection<Book> _books;
		public ObservableCollection<Book> Books
		{
			get { return _books; }
			set
			{
				if (value == _books)
				{
					return;
				}

				_books = value;
				OnPropertyChanged();
			}
		}

		private string _searchText;
		public string SearchText
		{
			get { return _searchText; }
			set
			{
				if (value == _searchText)
				{
					return;
				}

				_searchText = value;
				OnPropertyChanged();
			}
		}

		public override void InitializeViewModel ()
		{
			base.InitializeViewModel ();

			this.Title = "Libri";
			this.Books = new ObservableCollection<Book> ();

			this.Load ();
		}

		void DoSearch()
		{
			this.Books.Clear();

			if (string.IsNullOrEmpty(this.SearchText))
			{
				this.Load();
			}
			else
			{
				var searchResult = Database.Search(this.SearchText);
				searchResult.ForEach(this.Books.Add);
			}
		}

		void ResetSearch()
		{
			this.SearchText = "";
			this.Load();
		}

		public ICommand SearchCommand
		{
			get
			{
				return new DelegateCommand(DoSearch);
			}
		}

		public ICommand ResetSearchCommand
		{
			get
			{
				return new DelegateCommand(ResetSearch);
			}
		}

		public void BookSelected(int index)
		{
			BookSelected(Books[index]);
		}

		public void BookSelected(Book book)
		{
			if (book == null)
				return;

			DeviceServices.Current.Navigate(new BookViewModel() { 
				AuthorName = book.Author, 
				BookTitle = book.Title,
				BookDescription = book.Description,
                BookTime = string.Format("Letto dal {0} al {1}", book.StartTime.ToString("dd/MM/yyyy"), book.EndTime.ToString("dd/MM/yyyy"))
			});
		}

		public void Load()
		{
			this.Books.Clear();

			Database.Books.ForEach(this.Books.Add);
		}
	}

}

