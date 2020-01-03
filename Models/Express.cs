using System.ComponentModel.DataAnnotations;

namespace AquaServiceSPA.Models
{
    public class Express
    {
        [Required]
        public double? AquaLiters { get; set; }
        [Required]
        public double? ContainerCapacity { get; set; }
        public double? MaxKNO3g { get; set; }
        public double? MaxConcentrationNinKNO3PerLiter { get; set; }
        public double? MaxConcentrationKinKNO3MgPerLiter { get; set; }
        public double? OptimalKNO3g { get; set; }
        public double? OptimalConcentrationNinKNO3MgPerLiter { get; set; }
        public double? OptimalConcentrationKinKNO3MgPerLiter { get; set; }

        public double? MaxKH2PO4g { get; set; }
        public double? MaxConcentrationPinKH2PO4MgPerLiter { get; set; }
        public double? MaxConcentrationKinKH2PO4MgPerLiter { get; set; }
        public double? OptimalKH2PO4g { get; set; }
        public double? OptimalConcentrationPinKH2PO4MgPerLiter { get; set; }
        public double? OptimalConcentrationKinKH2PO4MgPerLiter { get; set; }

        public double? MaxK2SO4g { get; set; }
        public double? MaxConcentrationKinK2SO4MgPerLiter { get; set; }
        public double? OptimalK2SO4g { get; set; }
        public double? OptimalConcentrationKinK2SO4PerLiter { get; set; }

        public double? MaxMgSO4g { get; set; }
        public double? MaxConcentrationMginMgSO4PerLiter { get; set; }
        public double? OptimalMgSO4g { get; set; }
        public double? OptimalConcentrationMginMgSO4PerLiter { get; set; }
    }
}
