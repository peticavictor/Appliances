using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppliancesMVC.Models
{
    public class Appliance
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Code { get; set; }
        public double Price { get; set; }
        public DateTime ProducedOn { get; set; }
        public string Image { get; set; }

        public Appliance(int id, string type, string brand, string model, string code, double price, DateTime producedOn, string image)
        {
            Id=id;
            Type=type;
            Brand=brand;
            Model=model;
            Code=code;
            Price=price;
            ProducedOn=producedOn;
            Image=image;
        }
        public Appliance()
        {
        }
    }
}
