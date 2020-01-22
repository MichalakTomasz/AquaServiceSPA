using System.ComponentModel.DataAnnotations;

namespace AquaServiceSPA.Models
{
    public class Mgso4: MixtureData
    {
        [Required]
        public double Mgso4g { get; set; }
    }
}
