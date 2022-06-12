using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppliancesMVC.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public String Name{ get; set; }
        public String Password{ get; set; }

        public User(int id, string name, string password)
        {
            this.Id=id;
            this.Name=name;
            this.Password=password;
        }

        public User()
        {
        }
    }
}
