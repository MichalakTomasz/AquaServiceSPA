using AquaServiceSPA.Models;

namespace AquaServiceSPA.Services
{
    public interface IAquaCalcService
    {
        double K2SO4ContentK { get; }
        double K2SO4SolubilityGramsPer100Ml { get; }
        double KH2PO4ContentK { get; }
        double KH2PO4SolubilityGramsPer100Ml { get; }
        double KH2PO4ContentP { get; }
        double KNO3ContentK { get; }
        double KNO3ContentN { get; }
        double MgSO47H2OContentMg { get; }
        double MgSO4SolubilityGramsPer100Ml { get; }
        double KNO3SolubilityGramsPer100Ml { get; }

        double Co2Concentration(double kh, double ph);
        double ConcentrationIn1Ml(
            double liters,
            double gramsInSolution,
            double mlSolution,
            double precentInSalt);
        double GramsSalt(double ppmsUp, double liters, double percentageInSalt);
        double OptimalConcentrationIn1Ml(double liters,
            double gramsInsolution, double mlSolution,
            double precententInSalt,
            double saltSolubility);
        double Percent(double value);
        double Ppm(double percentInSalt, double liters);
        double SeingleDose(double weeklyDodeInMl, double soulubilityPer100Ml);
        double Solubility(double gramsSalt, double solubilityPer100Grams);
        double SolubilityInWater(double litersWater, double solubilityPer100Grams);
        MacroResult MacroCompute(
            double liters,
            double containerCapacity,
            double timesAWeek,
            double nitrogen,
            double phosphorus,
            double potassium,
            double magnesium);
        MacroResult MacroCompute(Macro Macro);
        Express ExpressCalc(double aquaLiters, double containerCapacity);
    }
}