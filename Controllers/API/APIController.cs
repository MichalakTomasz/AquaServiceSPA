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
                return BadRequest("Model is invalid");

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
                return BadRequest("Model is invalid");

            var result = aquaCalcService.ExpressCalc(express);
            return Ok(result);
        }

        [HttpPost("kno3")]
        public IActionResult kno3([FromBody] Kno3 kno3)
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
    }
}