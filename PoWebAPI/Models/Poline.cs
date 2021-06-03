using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PoWebAPI.Models
{
    public class Poline
    {
        public Poline() { }

        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; } = 1;
        [Required]
        public int PurchaseOrderId { get; set; }
        public  virtual PurchaseOrder PurchaseOrder { get; set; }
        [Required]
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }


    }
}
