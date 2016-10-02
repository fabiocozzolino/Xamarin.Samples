using SQLite;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XPlatform.Core.Model
{
    public class Book
    {
        [PrimaryKey]
        public string Code { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
        public string Author { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

	public class Author {
		public string Name { get; set; }
	}
}
