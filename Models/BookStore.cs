using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Models
{
    public class BookStore : AbstractEntity
    {
        public string Name { get; set; }
        [JsonIgnore]
        public IList<BooksInStores> BooksInStores { get; set; }

    }
}
