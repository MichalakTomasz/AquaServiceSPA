using System.ComponentModel.DataAnnotations;

namespace AquaServiceSPA.Models
{
    public class Macro
    {
        [Required]
        public double AquaLiters { get; set; }
        [Required]
        public double ContainerCapacity { get; set; }
        [Required]
        public double TimesAWeek { get; set; }
        [Required]
        public double Nitrogen { get; set; }
        [Required]
        public double Phosphorus { get; set; }
        [Required]
        public double Potassium { get; set; }
        [Required]
        public double Magnesium { get; set; }
    }
}