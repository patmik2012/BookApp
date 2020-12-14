using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BookApp.Models
{
    public class AbstractEntity
    {   
        [Key]
        public int Id { get; set; }
    }
}
