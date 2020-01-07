using AquaServiceSPA.Models;
using System;

namespace AquaServiceSPA.Services
{
    public class AquaCalcService : IAquaCalcService
    {
        public double KNO3ContentN { get; } = 61;
        public double KNO3ContentK { get; } = 38.7;
        public double KH2PO4ContentP { get; } = 69.8;
        public double KH2PO4ContentK { get; } = 28.7;
        public double K2SO4ContentK { get; } = 44.83;
        public double MgSO47H2OContentMg { get; } = 9.9;
        public double KNO3SolubilityGramsPer100Ml { get; } = 31;
        public double KH2PO4SolubilityGramsPer100Ml { get; } = 25;
        public double K2SO4SolubilityGramsPer100Ml { get; } = 11;
        public double MgSO4SolubilityGramsPer100Ml { get; } = 105;
        
        public double Co2Concentration(double kh, double ph)
            => Math.Round(3 * kh * Math.Pow(10, 7 - ph), 2);

        public double Percent(double value) => value / 100;

        public double Ppm(double percentInSalt, double liters)
            => (percentInSalt / liters) * 1000;

        public double GramsSalt(
            double ppmsUp,
            double liters,
            double percentageInSalt)
            => (ppmsUp * liters) / (percentageInSalt * 1000);

        public double Solubility(double gramsSalt, double solubilityPer100Grams)
            => gramsSalt * 100 / solubilityPer100Grams;

        public double SolubilityInWater(double litersWater, double solubilityPer100Grams)
            => solubilityPer100Grams * litersWater / 100;

        public double SeingleDose(double weeklyDodeInMl, double soulubilityPer100Ml)
            => weeklyDodeInMl / soulubilityPer100Ml;

        public double ConcentrationIn1Ml(
            double liters,
            double gramsInSolution,
            double mlSolution,
            double precentInSalt)
            => ((gramsInSolution * precentInSalt / liters) / mlSolution) * 1000;

        public double OptimalConcentrationIn1Ml(
            double liters,
            double gramsInsolution,
            double mlSolution,
            double precententInSalt,
            double saltSolubility)
        {
            var maxGramsPerWaterLiters = SolubilityInWater(mlSolution, saltSolubility);
            var maxConcentrationPerMl = ConcentrationIn1Ml(
                liters,
                maxGramsPerWaterLiters,
                mlSolution,
                precententInSalt);
            if (maxConcentrationPerMl <= .5) return maxConcentrationPerMl;
            else return -1;
        }

        public MacroResult MacroCompute(Macro macro)
        {
            var weeklyKNO3Dose = GramsSalt(macro.Nitrogen,
                   macro.AquaLiters,
                   Percent(KNO3ContentN));
            var weeklyKH2PO4Dose = GramsSalt(macro.Phosphorus,
                macro.AquaLiters,
                Percent(KH2PO4ContentP));
            var NO3Kppm = Ppm(Percent(KNO3ContentK),
                macro.AquaLiters);
            var KH2SPO4Kppm = Ppm(Percent(KH2PO4ContentK),
                macro.AquaLiters);
            var K2SO4Other = macro.Potassium - (NO3Kppm + KH2SPO4Kppm) > 0 ?
                macro.Potassium - (NO3Kppm + KH2SPO4Kppm) : 0;
            var weeklyK2SO4Dose = GramsSalt(K2SO4Other,
                macro.AquaLiters,
                Percent(K2SO4ContentK));
            var weeklyMgSO4Dose = GramsSalt(macro.Magnesium,
                macro.AquaLiters,
                Percent(MgSO47H2OContentMg));
            double fertililizerWeeklyDose = 0;
            fertililizerWeeklyDose += Solubility(weeklyKNO3Dose,
                KNO3SolubilityGramsPer100Ml);
            fertililizerWeeklyDose += Solubility(weeklyKH2PO4Dose,
                KH2PO4SolubilityGramsPer100Ml);
            fertililizerWeeklyDose += Solubility(weeklyK2SO4Dose,
                K2SO4SolubilityGramsPer100Ml);
            fertililizerWeeklyDose += Solubility(weeklyMgSO4Dose,
                MgSO4SolubilityGramsPer100Ml);
            fertililizerWeeklyDose = Math.Round(fertililizerWeeklyDose, MidpointRounding.ToEven);
            while (fertililizerWeeklyDose % macro.TimesAWeek != 0)
                fertililizerWeeklyDose++;
            var saltMultiplier = macro.ContainerCapacity / fertililizerWeeklyDose;
            var macroResult = new MacroResult
            {
                SingleDose = fertililizerWeeklyDose / macro.TimesAWeek,
                Kno3 = Math.Round((weeklyKNO3Dose * saltMultiplier), 2),
                Kh2po4 = Math.Round((weeklyKH2PO4Dose * saltMultiplier), 2),
                K2so4 = Math.Round((weeklyK2SO4Dose * saltMultiplier), 2),
                Mgso4 = Math.Round((weeklyMgSO4Dose * saltMultiplier), 2)
            };
            return macroResult;
        }

        public ExpressResult ExpressCalc(Express express)
        {
            var result = new ExpressResult
            {
                AquaLiters = express.AquaLiters,
                ContainerCapacity = express.ContainerCapacity
            };
            //nitrogen
            result.MaxKNO3g = SolubilityInWater(
                result.ContainerCapacity.Value,
                KNO3SolubilityGramsPer100Ml);
            result.MaxConcentrationNinKNO3MgPerLiter = ConcentrationIn1Ml(
                result.AquaLiters.Value,
                result.MaxKNO3g.Value,
                result.ContainerCapacity.Value,
                Percent(KNO3ContentN));
            result.MaxConcentrationKinKNO3MgPerLiter = ConcentrationIn1Ml(
                result.AquaLiters.Value,
                result.MaxKNO3g.Value,
                result.ContainerCapacity.Value,
                Percent(KNO3ContentK));
            if (result.MaxConcentrationNinKNO3MgPerLiter <= 0.5)
            {
                result.OptimalConcentrationNinKNO3MgPerLiter = 
                    result.MaxConcentrationNinKNO3MgPerLiter.Value;
                result.OptimalConcentrationKinKNO3MgPerLiter = 
                    result.MaxConcentrationKinKNO3MgPerLiter.Value;
                result.OptimalKNO3g = result.MaxKNO3g.Value;
            }
            else if (result.MaxConcentrationNinKNO3MgPerLiter > 0.5 &&
                result.MaxConcentrationNinKNO3MgPerLiter < 1)
            {
                result.OptimalKNO3g = (result.MaxKNO3g.Value * 0.5) /
                    result.MaxConcentrationNinKNO3MgPerLiter.Value;
                result.OptimalConcentrationNinKNO3MgPerLiter = ConcentrationIn1Ml(
                    result.AquaLiters.Value,
                    result.OptimalKNO3g.Value,
                    result.ContainerCapacity.Value,
                    Percent(KNO3ContentN));
                result.OptimalConcentrationKinKNO3MgPerLiter = ConcentrationIn1Ml(
                    result.AquaLiters.Value,
                    result.OptimalKNO3g.Value,
                    result.ContainerCapacity.Value,
                    Percent(KNO3ContentK));
            }
            else
            {
                result.OptimalKNO3g = (result.MaxKNO3g.Value * 1) /
                    result.MaxConcentrationNinKNO3MgPerLiter.Value;
                result.OptimalConcentrationNinKNO3MgPerLiter = Math.Round(ConcentrationIn1Ml(
                    result.AquaLiters.Value,
                    result.OptimalKNO3g.Value,
                    result.ContainerCapacity.Value,
                    Percent(KNO3ContentN)), 2);
                result.OptimalConcentrationKinKNO3MgPerLiter = ConcentrationIn1Ml(
                    result.AquaLiters.Value,
                    result.OptimalKNO3g.Value,
                    result.ContainerCapacity.Value,
                    Percent(KNO3ContentK));
            }
            //phosphorus
            result.MaxKH2PO4g = SolubilityInWater(
                result.ContainerCapacity.Value,
                KH2PO4SolubilityGramsPer100Ml);
            result.MaxConcentrationPinKH2PO4MgPerLiter = ConcentrationIn1Ml(
                result.AquaLiters.Value,
                result.MaxKH2PO4g.Value,
                result.ContainerCapacity.Value,
                Percent(KH2PO4ContentP));
            result.MaxConcentrationKinKH2PO4MgPerLiter = ConcentrationIn1Ml(
                result.AquaLiters.Value,
                result.MaxKH2PO4g.Value,
                result.ContainerCapacity.Value,
                Percent(KH2PO4ContentK));
            if (result.MaxConcentrationPinKH2PO4MgPerLiter <= 0.5)
            {
                result.OptimalConcentrationPinKH2PO4MgPerLiter = 
                    result.MaxConcentrationPinKH2PO4MgPerLiter;
                result.OptimalConcentrationKinKH2PO4MgPerLiter = 
                    result.MaxConcentrationKinKH2PO4MgPerLiter;
                result.OptimalKH2PO4g = result.MaxKH2PO4g;
            }
            else if (result.MaxConcentrationPinKH2PO4MgPerLiter > 0.5 &&
                result.MaxConcentrationPinKH2PO4MgPerLiter < 1)
            {
                result.OptimalKH2PO4g = (result.MaxKH2PO4g.Value * 0.1) /
                    result.MaxConcentrationPinKH2PO4MgPerLiter.Value;
                result.OptimalConcentrationPinKH2PO4MgPerLiter = ConcentrationIn1Ml(
                    result.AquaLiters.Value,
                    result.OptimalKH2PO4g.Value,
                    result.ContainerCapacity.Value,
                    Percent(KH2PO4ContentP));
                result.OptimalConcentrationKinKH2PO4MgPerLiter = ConcentrationIn1Ml(
                    result.AquaLiters.Value,
                    result.OptimalKH2PO4g.Value,
                    result.ContainerCapacity.Value,
                Percent(KH2PO4ContentK));
            }
            else
            {
                result.OptimalKH2PO4g = (result.MaxKH2PO4g.Value * .1) /
                    result.MaxConcentrationPinKH2PO4MgPerLiter.Value;
                result.OptimalConcentrationPinKH2PO4MgPerLiter = ConcentrationIn1Ml(
                    result.AquaLiters.Value,
                    result.OptimalKH2PO4g.Value,
                    result.ContainerCapacity.Value,
                    Percent(KH2PO4ContentP));
                result.OptimalConcentrationKinKH2PO4MgPerLiter = ConcentrationIn1Ml(
                    result.AquaLiters.Value,
                    result.OptimalKH2PO4g.Value,
                    result.ContainerCapacity.Value,
                    Percent(KH2PO4ContentK));
            }
            //potasium
            result.MaxK2SO4g = SolubilityInWater(
                result.ContainerCapacity.Value,
                K2SO4SolubilityGramsPer100Ml);
            result.MaxConcentrationKinK2SO4MgPerLiter = ConcentrationIn1Ml(
                result.AquaLiters.Value,
                result.MaxK2SO4g.Value,
                result.ContainerCapacity.Value,
                Percent(K2SO4ContentK));
            if (result.MaxConcentrationKinK2SO4MgPerLiter <= 0.5)
            {
                result.OptimalConcentrationKinK2SO4MgPerLiter = 
                    result.MaxConcentrationKinK2SO4MgPerLiter;
                result.OptimalK2SO4g = result.MaxK2SO4g.Value;
            }
            else if (result.MaxConcentrationKinK2SO4MgPerLiter > 0.5 &&
                result.MaxConcentrationKinK2SO4MgPerLiter < 1)
            {
                result.OptimalK2SO4g = (result.MaxK2SO4g.Value * 0.5) /
                    result.MaxConcentrationKinK2SO4MgPerLiter.Value;
                result.OptimalConcentrationKinK2SO4MgPerLiter = ConcentrationIn1Ml(
                    result.AquaLiters.Value,
                result.OptimalK2SO4g.Value,
                    result.ContainerCapacity.Value,
                    Percent(K2SO4ContentK));
            }
            else
            {
                result.OptimalK2SO4g = (result.MaxK2SO4g.Value * 1) /
                    result.MaxConcentrationKinK2SO4MgPerLiter.Value;
                result.OptimalConcentrationKinK2SO4MgPerLiter = ConcentrationIn1Ml(
                    result.AquaLiters.Value,
                    result.OptimalK2SO4g.Value,
                    result.ContainerCapacity.Value,
                    Percent(K2SO4ContentK));
            }
            //magnesium
            result.MaxMgSO4g = SolubilityInWater(
                result.ContainerCapacity.Value, MgSO4SolubilityGramsPer100Ml);
            result.MaxConcentrationMginMgSO4MgPerLiter = ConcentrationIn1Ml(
                result.AquaLiters.Value,
                result.MaxMgSO4g.Value,
                result.ContainerCapacity.Value,
                Percent(MgSO47H2OContentMg));
            if (result.MaxConcentrationMginMgSO4MgPerLiter <= 0.5)
            {
                result.OptimalConcentrationMginMgSO4MgPerLiter = 
                    result.MaxConcentrationMginMgSO4MgPerLiter;
                result.OptimalMgSO4g = result.MaxMgSO4g;
            }
            else if (result.MaxConcentrationMginMgSO4MgPerLiter > 0.5 &&
                result.MaxConcentrationMginMgSO4MgPerLiter < 1)
            {
                result.OptimalMgSO4g = (result.MaxMgSO4g.Value * 0.5) /
                    result.MaxConcentrationMginMgSO4MgPerLiter.Value;
                result.OptimalConcentrationMginMgSO4MgPerLiter = ConcentrationIn1Ml(
                    result.AquaLiters.Value,
                    result.OptimalMgSO4g.Value,
                    result.ContainerCapacity.Value,
                Percent(MgSO47H2OContentMg));
            }
            else
            {
                result.OptimalMgSO4g = (result.MaxMgSO4g.Value * 1) /
                    result.MaxConcentrationMginMgSO4MgPerLiter.Value;
                result.OptimalConcentrationMginMgSO4MgPerLiter = ConcentrationIn1Ml(
                    result.AquaLiters.Value,
                    result.OptimalMgSO4g.Value,
                    result.ContainerCapacity.Value,
                Percent(MgSO47H2OContentMg));
            }
            return result;
        }
    }
}
