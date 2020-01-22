using System.ComponentModel.DataAnnotations;

namespace AquaServiceSPA.Models
{
    public class MixtureData
    {
        [Required]
        public double AquaLiters { get; set; }
        [Required]
        public double ContainerCapacity { get; set; }
    }
}
