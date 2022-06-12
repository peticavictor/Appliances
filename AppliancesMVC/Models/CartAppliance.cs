using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AppliancesMVC.Models
{
    public class CartAppliance
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int TotalPrice { get; set; }

        [ForeignKey("Cart")]
        public int CartId { get; set; }
        public Cart Cart { get; set; }

        [ForeignKey("Appliance")]
        public int ApplianceId { get; set; }
        public Appliance Appliance { get; set; }

    }
}
