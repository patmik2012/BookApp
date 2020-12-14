using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Models
{
    public class Author : AbstractEntity
    {
        public string Name { get; set; }
        public int BirthYear { get; set; }
        public string Nation { get; set; }
        public bool Deleted { get; set; }
        public IList<Book> Books { get; set; }
    }
}
