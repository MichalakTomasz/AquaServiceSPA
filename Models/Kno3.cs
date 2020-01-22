using System.ComponentModel.DataAnnotations;

namespace AquaServiceSPA.Models
{
    public class Kno3: MixtureData
    {
        [Required]
        public double Kno3g { get; set; }
    }
}
