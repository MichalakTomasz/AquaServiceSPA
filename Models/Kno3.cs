using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace AquaServiceSPA.Models
{
    public class Kno3
    {
        [Required]
        public double AquaLiters { get; set; }
        [Required]
        public double ContainerCapacity { get; set; }
        [Required]
        public double Kno3g { get; set; }
    }
}
