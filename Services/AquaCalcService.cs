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

        public MacroResult MacroCompute(
            double aquaLiters,
            double containerCapacity,
            double timesAWeek,
            double nitrogen,
            double phosphorus,
            double potassium,
            double magnesium)
        {
            var weeklyKNO3Dose = GramsSalt(nitrogen,
                   aquaLiters,
                   Percent(KNO3ContentN));
            var weeklyKH2PO4Dose = GramsSalt(phosphorus,
                aquaLiters,
                Percent(KH2PO4ContentP));
            var NO3Kppm = Ppm(Percent(KNO3ContentK),
                aquaLiters);
            var KH2SPO4Kppm = Ppm(Percent(KH2PO4ContentK),
                aquaLiters);
            var K2SO4Other = potassium - (NO3Kppm + KH2SPO4Kppm) > 0 ?
                potassium - (NO3Kppm + KH2SPO4Kppm) : 0;
            var weeklyK2SO4Dose = GramsSalt(K2SO4Other,
                aquaLiters,
                Percent(K2SO4ContentK));
            var weeklyMgSO4Dose = GramsSalt(magnesium,
                aquaLiters,
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
            while (fertililizerWeeklyDose % timesAWeek != 0)
                fertililizerWeeklyDose++;
            var saltMultiplier = containerCapacity / fertililizerWeeklyDose;
            var macroResult = new MacroResult
            {
                singleDose = fertililizerWeeklyDose / timesAWeek,
                kno3 = Math.Round((weeklyKNO3Dose * saltMultiplier), 2),
                kh2po4 = Math.Round((weeklyKH2PO4Dose * saltMultiplier), 2),
                k2so4 = Math.Round((weeklyK2SO4Dose * saltMultiplier), 2),
                mgso4 = Math.Round((weeklyMgSO4Dose * saltMultiplier), 2)
            };
            return macroResult;
        }

        public MacroResult MacroCompute(Macro macro)
            => MacroCompute(
                macro.AquaLiters,
                macro.ContainerCapacity,
                macro.TimesAWeek,
                macro.Nitrogen,
                macro.Phosphorus,
                macro.Potassium,
                macro.Magnesium);

        public Express ExpressCalc(double aquaLiters, double containerCapacity)
        {
            var express = new Express
            {
                AquaLiters = aquaLiters,
                ContainerCapacity = containerCapacity
            };
            //nitrogen
            express.MaxKNO3g = SolubilityInWater(
                express.ContainerCapacity.Value,
                KNO3SolubilityGramsPer100Ml);
            express.MaxConcentrationNinKNO3PerLiter = ConcentrationIn1Ml(
                express.AquaLiters.Value,
                express.MaxKNO3g.Value,
                express.ContainerCapacity.Value,
                Percent(KNO3ContentN));
            express.MaxConcentrationKinKNO3MgPerLiter = ConcentrationIn1Ml(
                express.AquaLiters.Value,
                express.MaxKNO3g.Value,
                express.ContainerCapacity.Value,
                Percent(KNO3ContentK));
            if (express.MaxConcentrationNinKNO3PerLiter <= 0.5)
            {
                express.OptimalConcentrationNinKNO3MgPerLiter = 
                    express.MaxConcentrationNinKNO3PerLiter.Value;
                express.OptimalConcentrationKinKNO3MgPerLiter = 
                    express.MaxConcentrationKinKNO3MgPerLiter.Value;
                express.OptimalKNO3g = express.MaxKNO3g.Value;
            }
            else if (express.MaxConcentrationNinKNO3PerLiter > 0.5 &&
                express.MaxConcentrationNinKNO3PerLiter < 1)
            {
                express.OptimalKNO3g = (express.MaxKNO3g.Value * 0.5) /
                    express.MaxConcentrationNinKNO3PerLiter.Value;
                express.OptimalConcentrationNinKNO3MgPerLiter = ConcentrationIn1Ml(
                    express.AquaLiters.Value,
                    express.OptimalKNO3g.Value,
                    express.ContainerCapacity.Value,
                    Percent(KNO3ContentN));
                express.OptimalConcentrationKinKNO3MgPerLiter = ConcentrationIn1Ml(
                    express.AquaLiters.Value,
                    express.OptimalKNO3g.Value,
                    express.ContainerCapacity.Value,
                    Percent(KNO3ContentK));
            }
            else
            {
                express.OptimalKNO3g = (express.MaxKNO3g.Value * 1) /
                    express.MaxConcentrationNinKNO3PerLiter.Value;
                express.OptimalConcentrationNinKNO3MgPerLiter = Math.Round(ConcentrationIn1Ml(
                    express.AquaLiters.Value,
                    express.OptimalKNO3g.Value,
                    express.ContainerCapacity.Value,
                    Percent(KNO3ContentN)), 2);
                express.OptimalConcentrationKinKNO3MgPerLiter = ConcentrationIn1Ml(
                    express.AquaLiters.Value,
                    express.OptimalKNO3g.Value,
                    express.ContainerCapacity.Value,
                    Percent(KNO3ContentK));
            }
            //phosphorus
            express.MaxKH2PO4g = SolubilityInWater(
                express.ContainerCapacity.Value,
                KH2PO4SolubilityGramsPer100Ml);
            express.MaxConcentrationPinKH2PO4MgPerLiter = ConcentrationIn1Ml(
                express.AquaLiters.Value,
                express.MaxKH2PO4g.Value,
                express.ContainerCapacity.Value,
                Percent(KH2PO4ContentP));
            express.MaxConcentrationKinKH2PO4MgPerLiter = ConcentrationIn1Ml(
                express.AquaLiters.Value,
                express.MaxKH2PO4g.Value,
                express.ContainerCapacity.Value,
                Percent(KH2PO4ContentK));
            if (express.MaxConcentrationPinKH2PO4MgPerLiter <= 0.5)
            {
                express.OptimalConcentrationPinKH2PO4MgPerLiter = 
                    express.MaxConcentrationPinKH2PO4MgPerLiter;
                express.OptimalConcentrationKinKH2PO4MgPerLiter = 
                    express.MaxConcentrationKinKH2PO4MgPerLiter;
                express.OptimalKH2PO4g = express.MaxKH2PO4g;
            }
            else if (express.MaxConcentrationPinKH2PO4MgPerLiter > 0.5 &&
                express.MaxConcentrationPinKH2PO4MgPerLiter < 1)
            {
                express.OptimalKH2PO4g = (express.MaxKH2PO4g.Value * 0.1) /
                    express.MaxConcentrationPinKH2PO4MgPerLiter.Value;
                express.OptimalConcentrationPinKH2PO4MgPerLiter = ConcentrationIn1Ml(
                    express.AquaLiters.Value,
                    express.OptimalKH2PO4g.Value,
                    express.ContainerCapacity.Value,
                    Percent(KH2PO4ContentP));
                express.OptimalConcentrationKinKH2PO4MgPerLiter = ConcentrationIn1Ml(
                    express.AquaLiters.Value,
                    express.OptimalKH2PO4g.Value,
                    express.ContainerCapacity.Value,
                Percent(KH2PO4ContentK));
            }
            else
            {
                express.OptimalKH2PO4g = (express.MaxKH2PO4g.Value * .1) /
                    express.MaxConcentrationPinKH2PO4MgPerLiter.Value;
                express.OptimalConcentrationPinKH2PO4MgPerLiter = ConcentrationIn1Ml(
                    express.AquaLiters.Value,
                    express.OptimalKH2PO4g.Value,
                    express.ContainerCapacity.Value,
                    Percent(KH2PO4ContentP));
                express.OptimalConcentrationKinKH2PO4MgPerLiter = ConcentrationIn1Ml(
                    express.AquaLiters.Value,
                    express.OptimalKH2PO4g.Value,
                    express.ContainerCapacity.Value,
                    Percent(KH2PO4ContentK));
            }
            //potasium
            express.MaxK2SO4g = SolubilityInWater(
                express.ContainerCapacity.Value,
                K2SO4SolubilityGramsPer100Ml);
            express.MaxConcentrationKinK2SO4MgPerLiter = ConcentrationIn1Ml(
                express.AquaLiters.Value,
                express.MaxK2SO4g.Value,
                express.ContainerCapacity.Value,
                Percent(K2SO4ContentK));
            if (express.MaxConcentrationKinK2SO4MgPerLiter <= 0.5)
            {
                express.OptimalConcentrationKinK2SO4PerLiter = 
                    express.MaxConcentrationKinK2SO4MgPerLiter;
                express.OptimalK2SO4g = express.MaxK2SO4g.Value;
            }
            else if (express.MaxConcentrationKinK2SO4MgPerLiter > 0.5 &&
                express.MaxConcentrationKinK2SO4MgPerLiter < 1)
            {
                express.OptimalK2SO4g = (express.MaxK2SO4g.Value * 0.5) /
                    express.MaxConcentrationKinK2SO4MgPerLiter.Value;
                express.OptimalConcentrationKinK2SO4PerLiter = ConcentrationIn1Ml(
                    express.AquaLiters.Value,
                express.OptimalK2SO4g.Value,
                    express.ContainerCapacity.Value,
                    Percent(K2SO4ContentK));
            }
            else
            {
                express.OptimalK2SO4g = (express.MaxK2SO4g.Value * 1) /
                    express.MaxConcentrationKinK2SO4MgPerLiter.Value;
                express.OptimalConcentrationKinK2SO4PerLiter = ConcentrationIn1Ml(
                    express.AquaLiters.Value,
                    express.OptimalK2SO4g.Value,
                    express.ContainerCapacity.Value,
                    Percent(K2SO4ContentK));
            }
            //magnesium
            express.MaxMgSO4g = SolubilityInWater(
                express.ContainerCapacity.Value, MgSO4SolubilityGramsPer100Ml);
            express.MaxConcentrationMginMgSO4PerLiter = ConcentrationIn1Ml(
                express.AquaLiters.Value,
                express.MaxMgSO4g.Value,
                express.ContainerCapacity.Value,
                Percent(MgSO47H2OContentMg));
            if (express.MaxConcentrationMginMgSO4PerLiter <= 0.5)
            {
                express.OptimalConcentrationMginMgSO4PerLiter = 
                    express.MaxConcentrationMginMgSO4PerLiter;
                express.OptimalMgSO4g = express.MaxMgSO4g;
            }
            else if (express.MaxConcentrationMginMgSO4PerLiter > 0.5 &&
                express.MaxConcentrationMginMgSO4PerLiter < 1)
            {
                express.OptimalMgSO4g = (express.MaxMgSO4g.Value * 0.5) /
                    express.MaxConcentrationMginMgSO4PerLiter.Value;
                express.OptimalConcentrationMginMgSO4PerLiter = ConcentrationIn1Ml(
                    express.AquaLiters.Value,
                    express.OptimalMgSO4g.Value,
                    express.ContainerCapacity.Value,
                Percent(MgSO47H2OContentMg));
            }
            else
            {
                express.OptimalMgSO4g = (express.MaxMgSO4g.Value * 1) /
                    express.MaxConcentrationMginMgSO4PerLiter.Value;
                express.OptimalConcentrationMginMgSO4PerLiter = ConcentrationIn1Ml(
                    express.AquaLiters.Value,
                    express.OptimalMgSO4g.Value,
                    express.ContainerCapacity.Value,
                Percent(MgSO47H2OContentMg));
            }
            return express;
        }
    }
}
