using System.ComponentModel.DataAnnotations;

namespace AquaServiceSPA.Models
{
    public class CO2
    {
        [Required]
        [Range(0, 12)]
        [RegularExpression("^[0,9]+$")]
        public double Ph { get; set; }
        [Required]
        [Range(0, 12)]
        [RegularExpression("^[0,9]+$")]
        public double Kh { get; set; }
    }
}
