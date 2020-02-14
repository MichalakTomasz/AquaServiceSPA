using AquaServiceSPA.Models;
using AquaServiceSPA.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AquaServiceSPA.Controllers
{
    [Route("api")]
    public class APIController : ControllerBase
    {
        private readonly IEmailService emailService;
        private readonly IEmailSettingsService emailSettingsService;
        private readonly IAquaCalcService aquaCalcService;
        private readonly IEmailMessageLayoutService emailMessageLayoutService;
        private readonly IVisitService visitService;
        private readonly AquaMacroDefaultSettings aquaMacroDefaultSettings;
        private readonly ILoggerService loggerService;

        public APIController(
            IEmailService emailService,
            IEmailSettingsService emailSettingsService,
            IAquaCalcService aquaCalcService,
            IEmailMessageLayoutService emailMessageLayoutService,
            IVisitService visitService,
            AquaMacroDefaultSettings aquaMacroDefaultSettings,
            ILoggerService loggerService)
        {
            this.emailService = emailService;
            this.emailSettingsService = emailSettingsService;
            this.aquaCalcService = aquaCalcService;
            this.emailMessageLayoutService = emailMessageLayoutService;
            this.visitService = visitService;
            this.aquaMacroDefaultSettings = aquaMacroDefaultSettings;
            this.loggerService = loggerService;
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
                var viewModel = new K2so4Result
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

            var result = new K2so4Result
            {
                PotassiumContent = potassiumContent
            };
            return Ok(result);
        }

        [HttpPost("kh2po4")]
        public IActionResult Kh2po4([FromBody] Kh2po4 kh2po4)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var kh2po4Solubility = aquaCalcService
                .Solubility(kh2po4.Kh2po4g, SaltsStaticData.KH2PO4SolubilityGramsPer100Ml);
            var minSaltSolubilityInAmountWater = aquaCalcService
                .SolubilityInWater(kh2po4.ContainerCapacity, SaltsStaticData.KH2PO4SolubilityGramsPer100Ml);

            if (kh2po4Solubility > kh2po4.ContainerCapacity)
            {
                var viewModel = new K2so4Result
                {
                    Solubility = kh2po4Solubility,
                    SolubilityInAmountWater = minSaltSolubilityInAmountWater
                };
                return Ok(viewModel);
            }

            var phosphorusContent = aquaCalcService.ConcentrationIn1Ml(
                kh2po4.AquaLiters,
                kh2po4.Kh2po4g,
                kh2po4.ContainerCapacity,
                aquaCalcService.Percent(aquaCalcService.KH2PO4ContentP));

            var potassiumContent = aquaCalcService.ConcentrationIn1Ml(
                kh2po4.AquaLiters,
                kh2po4.Kh2po4g,
                kh2po4.ContainerCapacity,
                aquaCalcService.Percent(aquaCalcService.KH2PO4ContentK));

            var result = new Kh2po4Result
            {
                PhosphorusContent = phosphorusContent,
                PotassiumContent = potassiumContent
            };
            return Ok(result);
        }

        [HttpPost("mgso4")]
        public IActionResult MgSO4([FromBody] Mgso4 mgso4)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var solubility = aquaCalcService
                .Solubility(mgso4.Mgso4g, SaltsStaticData.MgSO4SolubilityGramsPer100Ml);
            var minSaltSolubilityInAmountWater = aquaCalcService
                .SolubilityInWater(mgso4.ContainerCapacity, SaltsStaticData.MgSO4SolubilityGramsPer100Ml);

            if (solubility > mgso4.ContainerCapacity)
            {
                var viewModel = new Mgso4Result
                {
                    Solubility = solubility,
                    SolubilityInAmountWater = minSaltSolubilityInAmountWater
                };
                return Ok(viewModel);
            }

            var magnesiumContent = aquaCalcService.ConcentrationIn1Ml(
                mgso4.AquaLiters,
                mgso4.Mgso4g,
                mgso4.ContainerCapacity,
                aquaCalcService.Percent(aquaCalcService.MgSO47H2OContentMg));

            var result = new Mgso4Result
            {
                MagnesiumContent = magnesiumContent

            };
            return Ok(result);
        }

        [HttpPost("sendcontactemail")]
        public async Task<IActionResult> SendEmail([FromBody]Email email)
        {
            if (!ModelState.IsValid)
            {
                loggerService.Log("SendContactEmailAction - ModelState invalid");
                loggerService.Log($"Email address: {email.EmailAddress}, Subject: {email.Subject}" +
                    $", Message: {email.Message}, Description: {email.Description}, Username: {email.Username}");
                loggerService.Log(ModelState.Values.ToString());
                return BadRequest(ModelState);
            }

            var emailSettings = emailSettingsService.GetSettings();
            var contactEmailToMod = new Email
            {
                EmailAddress = emailSettings.EmailAddress,
                Message = emailMessageLayoutService.ContactModMessage(email),
                Subject = $" [{ConstStrings.Servicename}] - temat: {email.Subject}",
                Description = email.Description,
                Username = email.Username
            };

            await emailService.SendEmailAsync(contactEmailToMod);

            var confirmEmailToUser = new Email
            {
                EmailAddress = email.EmailAddress,
                Message = emailMessageLayoutService.ContactUserFeedback(email.Username, ConstStrings.Servicename),
                Subject = $"[{ConstStrings.Servicename}] - temat: {email.Subject}"
            };
            await emailService.SendEmailAsync(confirmEmailToUser);
            return Ok(true);
        }

        [HttpPost("setemailsettings")]
        public IActionResult SetEmailSettings([FromBody]EmailSettings emailSettings)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            emailSettingsService.SetSettings(emailSettings);
            return Ok(true);
        }

        [HttpGet("getemailsettings")]
        public IActionResult GetEmailSettings()
        {
            var emailSettings = emailSettingsService.GetSettings();
            if (emailSettings != null)
                return Ok(emailSettings);
            else
                return BadRequest(false);
        }

        [HttpGet("savevisit")]
        public async Task<IActionResult> SaveVisit()
        {
            var ip = await visitService.AddAsync();
            return Ok(ip);
        }
    }
}