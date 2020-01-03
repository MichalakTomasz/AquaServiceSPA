﻿using AquaServiceSPA.Models;
using AquaServiceSPA.Services;
using Microsoft.AspNetCore.Mvc;

namespace AquaServiceSPA.Controllers
{
    [Route("api")]
    public class APIController : ControllerBase
    {
        private readonly IAquaCalcService aquaCalcService;

        public APIController(IAquaCalcService aquaCalcService)
        {
            this.aquaCalcService = aquaCalcService;
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
    }
}