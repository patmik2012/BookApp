using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Models
{
    public class BooksInStores : AbstractEntity
    {
        public int BookId { get; set; }
        [JsonIgnore]
        public Book Book { get; set; }

        public int BookStoreId { get; set; }
        [JsonIgnore]
        public BookStore bookStore { get; set; }
        
        public int Copies { get; set; }

    }
}
