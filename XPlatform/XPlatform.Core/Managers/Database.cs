using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XPlatform.Core.Model;

namespace XPlatform.Core.Managers
{
    public class Database
    {
		private static SQLite.Net.SQLiteConnection _connection;
		static Database()
        {
            var dataConnection = DeviceServices.Current.GetService<IDataConnectionService>();
			_connection = dataConnection.GetConnection("bookshelf.db");
        }

		internal static IEnumerable<Book> Books
        {
			get{
				var result = _connection.Table<Book> ().ToArray ();
				return result;
			}
        }

		public static IEnumerable<Book> Search (string searchText)
		{
			var result = _connection.Table<Book>().Where(s => s.Title.Contains(searchText)).ToArray();
			return result;
		}

		internal static IEnumerable<Author> Authors
		{
			get{
				var result = _connection.Query<Author> ("SELECT DISTINCT Author AS Name FROM Book").ToArray ();
				return result;
			}
		}

		internal static IEnumerable<Book> LoadBooksByAuthor(Author author)
        {
            var result = _connection.Table<Book>().Where(s => s.Author == author.Name).ToArray();
            return result;
        }

		public static void Initialize()
		{
			_connection.DropTable<Book> ();
			_connection.CreateTable<Book> ();

			_connection.Insert (new Book () {
				Code = "B001",
				Title = "1984",
				Description = "L'azione si svolge in un futuro prossimo del mondo (l'anno 1984) in cui il potere si concentra in tre immensi superstati: Oceania, Eurasia ed Estasia. Al vertice del potere politico in Oceania c'è il Grande Fratello, onnisciente e infallibile, che nessuno ha visto di persona ma di cui ovunque sono visibili grandi manifesti. Il Ministero della Verità, nel quale lavora il personaggio principale, Smith, ha il compito di censurare libri e giornali non in linea con la politica ufficiale, di alterare la storia e di ridurre le possibilità espressive della lingua. Per quanto sia tenuto sotto controllo da telecamere, Smith comincia a condurre un'esistenza \"sovversiva\". Scritto nel 1949, il libro è considerato una delle più lucide rappresentazioni del totalitarismo.",
				Author = "George Orwell",
				StartTime = new DateTime (2008, 6, 10),
				EndTime = new DateTime (2008, 6, 20)
			});
			_connection.Insert (new Book () {
				Code = "B002",
                Title = "Fahrenheit 451",
                Description = "In un'allucinante società del futuro si cercano, per bruciarli, gli ultimi libri scampati a una distruzione sistematica e conservati illegalmente. Il romanzo più conosciuto del celebre scrittore americano di fantascienza.",
				Author = "Ray Bradbury",
                StartTime = new DateTime(2008, 6, 20),
                EndTime = new DateTime(2008, 6, 30)
            });
			_connection.Insert (new Book () {
				Code = "B003",
                Title = "Mondo nuovo - Ritorno al mondo nuovo",
                Description = "Romanzo del 1932, ambientato in un immaginario stato totalitario del futuro pianificato nel nome del razonalismo produttivistico, dove tutto è sacrificabile a un malinteso mito del progresso. I cittadini di questa società non sono oppressi dalla guerra nè dalle malattie e possono accedere liberamente ad ogni piacere materiale. Affinchè però, si mantenga questo equilibrio gli abitanti, concepiti e prodotti industrialmente in provetta sotto il costante controllo di ingegneri genetici, durante l’infanzia vengono condizionati con la tecnologia e con droghe; da adulti occupano ruoli sociali prestabiliti secondo il livello di nascita. In cambio del mero benessere fisico, i cittadini devono insomma rinunciare ad ogni emozione, sentimento e ad ogni difesa della propria individualità. I pilastri ideologici che fanno da sfondo al fortunato romanzo vengono ripresi nel 1958, nella raccolta di saggi intitolata “Ritorno al mondo nuovo“, in cui Huxley riesamina singolarmente le sue profezie alla luce degli avvenimenti degli ultimi anni, arrivando alla conclusione che molte delle sue più catastrofiche previsioni di quasi trent’anni prima si sono avverate anzitempo e fanno già parte del presente. Un documento inquietante che costringe a riflettere sul prezzo che quotidianamente siamo chiamati a pagare per costruire il futuro.",
				Author = "Aldous Huxley",
                StartTime = new DateTime(2008, 7, 1),
                EndTime = new DateTime(2008, 7, 8)
            });
            _connection.Insert(new Book()
            {
                Code = "B004",
                Title = "Cronache marziane",
                Description = "Scritto tra il 1946 e il 1950, Cronache marziane è il resoconto della colonizzazione di Marte da parte dei terrestri: un racconto ricco di inventiva e di situazioni capaci di scolpirsi nell'immaginazione di generazioni di scrittori e di lettori. Ma è soprattutto il romanzo che segnò una svolta nella letteratura americana di fantascienza. Per la prima volta in queste pagine, infatti, Bradbury riesce genialmente a superare i limiti della narrativa di genere, ritrovando l'universalità simbolica della fiaba. Opera originalissima di uno degli scrittori più innovativi del Novecento, capolavoro della fantascienza \"classica\", Cronache marziane conserva ancora oggi una profondità, un equilibrio e una vitalità straordinarie, che ne fanno uno dei libri più amati della letteratura contemporanea.",
                Author = "Ray Bradbury",
                StartTime = new DateTime(2008, 7, 9),
                EndTime = new DateTime(2008, 7, 15)
            });
            _connection.Insert(new Book()
            {
                Code = "B005",
                Title = "La fattoria degli animali",
                Description = "Gli animali della fattoria Manor decidono di ribellarsi al padrone e di instaurare una loro democrazia. I maiali Napoleon e Snowball capeggiano la rivoluzione che però ben presto degenera. Infatti Napoleon, dopo aver bandito Snowball, introduce una nuova costituzione: \"Tutti gli animali sono uguali, ma alcuni sono più uguali degli altri\". La dittatura e la repressione fanno riappacificare gli animali con gli uomini che ormai non appaiono più agli exrivoluzionari molto diversi da loro.",
                Author = "George Orwell",
                StartTime = new DateTime(2008, 7, 16),
                EndTime = new DateTime(2008, 7, 28)
            });
        }
    }
}
