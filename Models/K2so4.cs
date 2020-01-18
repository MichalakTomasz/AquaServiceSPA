using System.ComponentModel.DataAnnotations;

namespace AquaServiceSPA.Models
{
    public class K2so4
    {
        [Required]
        public double AquaLiters { get; set; }
        [Required]
        public double ContainerCapacity { get; set; }
        [Required]
        public double K2so4g { get; set; }
    }
}
