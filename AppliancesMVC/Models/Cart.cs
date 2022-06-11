﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Appliances.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsPayed { get; set; }

        [ForeignKey("User")]
        public int UserId{ get; set; }
        public User User{ get; set; }

    }
}
