using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HybridRocksCases.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName ="NVARCHAR(30)")]
        public string Name { get; set; }

        [Column(TypeName = "NVARCHAR(30)")]
        public string Surname { get; set; }


    }

}
