﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HybridRocksCases.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int IsStatus { get; set; }

        public Product Product { get; set; }
        public User User { get; set; }
    }
}
