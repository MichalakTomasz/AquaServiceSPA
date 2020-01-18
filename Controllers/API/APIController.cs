using AquaServiceSPA.Models;
using AquaServiceSPA.Services;
using Microsoft.AspNetCore.Mvc;

namespace AquaServiceSPA.Controllers
{
    [Route("api")]
    public class APIController : ControllerBase
    {
        private readonly IAquaCalcService aquaCalcService;
        private readonly AquaMacroDefaultSettings aquaMacroDefaultSettings;

        public APIController(
            IAquaCalcService aquaCalcService,
            AquaMacroDefaultSettings aquaMacroDefaultSettings)
        {
            this.aquaCalcService = aquaCalcService;
            this.aquaMacroDefaultSettings = aquaMacroDefaultSettings;
        }

        [HttpPost("co2")]
        public IActionResult CO2([FromBody]CO2 co2)
        {
            if (!ModelState.IsValid)
                return BadRequest("Model is invalid");

            var result = aquaCalcService.Co2Concentration(co2.Kh, co2.Ph);
            return Ok(result);
        }

        [HttpPost("macro")]
        public IActionResult Macro([FromBody]Macro macro)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = aquaCalcService.MacroCompute(macro);
            return Ok(result);
        }

        [HttpGet("macrodefaultdata")]
        public IActionResult GetAquaDefaultMacroSettings()
            => Ok(aquaMacroDefaultSettings);

        [HttpPost("express")]
        public IActionResult Express([FromBody] Express express)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = aquaCalcService.ExpressCalc(express);
            return Ok(result);
        }

        [HttpPost("kno3")]
        public IActionResult Kno3([FromBody] Kno3 kno3)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var kno3Solubility = aquaCalcService
                .Solubility(kno3.Kno3g, SaltsStaticData.KNO3SolubilityGramsPer100Ml);
            var minSaltSolubilityInAmountWater = aquaCalcService
                .SolubilityInWater(kno3.ContainerCapacity, SaltsStaticData.KNO3SolubilityGramsPer100Ml);
            
            if (kno3Solubility > kno3.ContainerCapacity)
            {
                var viewModel = new Kno3Result
                {
                    Solubility = kno3Solubility,
                    SolubilityInAmountWater = minSaltSolubilityInAmountWater
                };
                return Ok(viewModel);
            }

            var nitrogenContent = aquaCalcService.ConcentrationIn1Ml(
                kno3.AquaLiters,
                kno3.Kno3g,
                kno3.ContainerCapacity,
                aquaCalcService.Percent(aquaCalcService.KNO3ContentN));
            var potassiumContent = aquaCalcService.ConcentrationIn1Ml(
                kno3.AquaLiters,
                kno3.Kno3g,
                kno3.ContainerCapacity,
                aquaCalcService.Percent(aquaCalcService.KNO3ContentK));
            var result = new Kno3Result
            {
                NitrogenContent = nitrogenContent,
                PotassiumContent = potassiumContent
            };
            return Ok(result);
        }

        [HttpPost("k2so4")]
        public IActionResult K2So4([FromBody] K2so4 k2So4)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var k2So4Solubility = aquaCalcService
                .Solubility(k2So4.K2so4g, SaltsStaticData.K2SO4SolubilityGramsPer100Ml);
            var minSaltSolubilityInAmountWater = aquaCalcService
                .SolubilityInWater(k2So4.ContainerCapacity, SaltsStaticData.K2SO4SolubilityGramsPer100Ml);

            if (k2So4Solubility > k2So4.ContainerCapacity)
            {
                var viewModel = new Kno3Result
                {
                    Solubility = k2So4Solubility,
                    SolubilityInAmountWater = minSaltSolubilityInAmountWater
                };
                return Ok(viewModel);
            }

            var potassiumContent = aquaCalcService.ConcentrationIn1Ml(
                k2So4.AquaLiters,
                k2So4.K2so4g,
                k2So4.ContainerCapacity,
                aquaCalcService.Percent(aquaCalcService.K2SO4ContentK));
            
            var result = new Kno3Result
            {
                PotassiumContent = potassiumContent
            };
            return Ok(result);
        }
    }
}