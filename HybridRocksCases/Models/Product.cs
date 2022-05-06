using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HybridRocksCases.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName ="NVARCHAR(50)")]
        public string ProductName { get; set; }
        public double Price { get; set; }


    }
}
