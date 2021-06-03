using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PoWebAPI.Models
{
    public class Item
    {
        public Item() { }

        public int Id { get; set; }
        [Required, StringLength(30)]
        public string Name { get; set; }
        [Required, Column(TypeName = "decimal (7,2)")]
        public decimal Price { get; set; }
        [Required]
        public bool Active { get; set; }
    }
}
