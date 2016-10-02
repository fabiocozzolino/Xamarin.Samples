using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using XPlatform.Core.Model;
using System.Windows.Input;
using XPlatform.Core.Managers;

namespace XPlatform.Core.ViewModels
{
    public class MainViewModel : ViewModel
    {
		private string _bookSectionTitle;
		public string BookSectionTitle {
			get{
				return _bookSectionTitle;
			}
			set{
				if (value != _bookSectionTitle) {
					_bookSectionTitle = value;
					OnPropertyChanged ();
				}
			}
		}

		private string _authorSectionTitle;
		public string AuthorSectionTitle {
			get{
				return _authorSectionTitle;
			}
			set{
				if (value != _authorSectionTitle) {
					_authorSectionTitle = value;
					OnPropertyChanged ();
				}
			}
		}

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

		private bool firstLoad = true;

		public MainViewModel()
		{
			this.Books = new ObservableCollection<Book>();
			this.Books.Add(new Book() { Description = "prova", Author = "autore", Title = "mybook" });
		}

		public override void InitializeViewModel ()
		{
			base.InitializeViewModel ();

			this.Title = "LIBRERIA";
			this.BookSectionTitle = "Libri";
			this.AuthorSectionTitle = "Autori";

			if (!firstLoad)
				return;

			this.SearchCommand = new DelegateCommand (() => {
				this.Books.Clear ();

				if (string.IsNullOrEmpty (this.SearchText)) {
					this.Load ();
				} else {
					var searchResult = Database.Search (this.SearchText);
					searchResult.ForEach (this.Books.Add);
				}
			});
			this.BookSelectedCommand = new DelegateCommand (() => {
				DeviceServices.Current.Navigate (new BookViewModel () { 
					AuthorName = SelectedBook.Author, 
					BookTitle = SelectedBook.Title,
					BookDescription = SelectedBook.Description,
					BookTime = string.Format ("Letto dal {0} al {1}", SelectedBook.StartTime.ToString ("dd/MM/yyyy"), SelectedBook.EndTime.ToString ("dd/MM/yyyy"))
				});
			});
			this.AuthorSelectedCommand = new DelegateCommand (() => {
				DeviceServices.Current.Navigate (new AuthorViewModel () { 
					Author = SelectedAuthor
				});
			});

			this.Books = new ObservableCollection<Book> ();
			this.Authors = new ObservableCollection<Author> ();
			this.Load ();

			firstLoad = false;
		}

		private ICommand _searchCommand;
		public ICommand SearchCommand
		{
			get
			{
				return _searchCommand;
			}
			set{
				if (_searchCommand != value) {
					_searchCommand = value;
					OnPropertyChanged ();
				}
			}
		}

		private ICommand _bookSelectedCommand;
		public ICommand BookSelectedCommand
		{
			get
			{
				return _bookSelectedCommand;
			}
			set{
				if (_bookSelectedCommand != value) {
					_bookSelectedCommand = value;
					OnPropertyChanged ();
				}
			}
		}

		private Book _selectedBook;
		public Book SelectedBook{
			get{
				return _selectedBook;
			}
			set{
				if (value != _selectedBook) {
					_selectedBook = value;
					OnPropertyChanged ();
				}
			}
		}

		private ICommand _authorSelectedCommand;
		public ICommand AuthorSelectedCommand
		{
			get
			{
				return _authorSelectedCommand;
			}
			set{
				if (_authorSelectedCommand != value) {
					_authorSelectedCommand = value;
					OnPropertyChanged ();
				}
			}
		}

		private Author _selectedAuthor;
		public Author SelectedAuthor{
			get{
				return _selectedAuthor;
			}
			set{
				if (value != _selectedAuthor) {
					_selectedAuthor = value;
					OnPropertyChanged ();
				}
			}
		}

		public void Load()
		{
			this.Books.Clear();
			this.Authors.Clear();

			Database.Books.ForEach(this.Books.Add);
			Database.Authors.ForEach(this.Authors.Add);
		}
    }
}
