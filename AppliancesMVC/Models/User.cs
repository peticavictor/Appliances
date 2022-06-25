using AppliancesMVC.Common.Enum;
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
        [Required]
        public String Name{ get; set; }
        [Required]
        public String Password{ get; set; }
        public DateTime? BirthDate { get; set; }
        public String Email { get; set; }
        public String Telefon { get; set; }
        public UserRole Role { get; set; }


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
