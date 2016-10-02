using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using XPlatform.Core.Model;
using System.Windows.Input;
using XPlatform.Core.ViewModels;
using XPlatform.Core.Managers;

namespace XPlatform.Core.ViewModels
{

	public class AuthorViewModel : ViewModel {
		
		private Author _author;
		public Author Author
		{
			get { return _author; }
			set
			{
				if (value == _author)
				{
					return;
				}

				_author = value;
				OnPropertyChanged();
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

        public override void InitializeViewModel()
        {
            base.InitializeViewModel();

			this.BookSelectedCommand = new DelegateCommand (() => {
				DeviceServices.Current.Navigate (new BookViewModel () { 
					AuthorName = SelectedBook.Author, 
					BookTitle = SelectedBook.Title,
					BookDescription = SelectedBook.Description,
					BookTime = string.Format ("Letto dal {0} al {1}", SelectedBook.StartTime.ToString ("dd/MM/yyyy"), SelectedBook.EndTime.ToString ("dd/MM/yyyy"))
				});
			});

			this.Title = this.Author.Name;
			this.Books = new ObservableCollection<Book>();
			Database.LoadBooksByAuthor(this.Author).ForEach(this.Books.Add);
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
	}
	
}
