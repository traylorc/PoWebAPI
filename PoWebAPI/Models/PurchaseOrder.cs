using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PoWebAPI.Models
{
    public class PurchaseOrder
    {
        public PurchaseOrder()
        { }
        public static string StatusNew = "new";
        public static string StatusEdit = "edit";
        public static string StatusReview = "review";
        public static string StatusApproved = "approved";
        public static string StatusRejected = "rejected";

        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Description { get; set; }
        [Required, StringLength(30)]
        public string Status { get; set; } = PurchaseOrder.StatusNew;
        [Required, Column(TypeName = "decimal (9,2)")]
        public decimal Total { get; set; } = 0;
        [Required]
        public bool Active { get; set; } = true;
        [Required]
        public int EmployeeId { get; set; }        
        public virtual Employee Employee { get; set; }








    }
}
