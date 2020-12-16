using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Models
{
    public class Book : AbstractEntity
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }
        [JsonIgnore]
        public Author Author { get; set; }
        public int PublishedYear { get; set; }
        public int PageNumber { get; set; }
        [MaxLength(13)]
        public string ISBN { get; set; }
        public bool Deleted { get; set; }
        public int AgeLimit { get; set; }
        [JsonIgnore]
        public IList<BooksInStores> BooksInStores { get; set; }

    }
}
