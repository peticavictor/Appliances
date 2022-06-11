using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Appliances.Models
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

        public Appliance(int id, string type, string brand, string model, string code, double price, DateTime producedOn)
        {
            this.Id=id;
            this.Type=type;
            this.Brand=brand;
            this.Model=model;
            this.Code=code;
            this.Price=price;
            this.ProducedOn=producedOn;
        }
        public Appliance()
        {
        }
    }
}
