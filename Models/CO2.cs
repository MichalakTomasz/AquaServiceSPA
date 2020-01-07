using AquaServiceSPA.Services;
using System.ComponentModel.DataAnnotations;

namespace AquaServiceSPA.Models
{
    public class CO2
    {
        [Required]
        [Range(0, 12)]
        [RegularExpression(ConstValues.DIGITS_DOUBLE_PRECISION_PATTERN)]
        public double Ph { get; set; }
        [Required]
        [Range(0, 12)]
        [RegularExpression(ConstValues.DIGITS_DOUBLE_PRECISION_PATTERN)]
        public double Kh { get; set; }
    }
}
