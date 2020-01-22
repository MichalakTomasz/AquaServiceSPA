using AquaServiceSPA.Models;
using Microsoft.AspNetCore.DataProtection;

namespace AquaServiceSPA.Services
{
    public class EmailSettingsService : IEmailSettingsService
    {
        private readonly IEmailSettingsConverter emailSettingsConverter;
        private readonly IDataProtector dataProtector;
        private readonly IKeyService keysService;

        public EmailSettingsService(
            IEmailSettingsConverter emailSettingsConverter,
            IDataProtectionProvider dataProtector,
            IKeyService keysService,
            ICryptographicKeyService cryptographicKeyService)
        {
            var key = cryptographicKeyService.GetKey();
            this.emailSettingsConverter = emailSettingsConverter;
            this.dataProtector = dataProtector.CreateProtector(key);
            this.keysService = keysService;
        }

        public EmailSettings GetSettings()
        {
            var encryptedSettings = keysService.GetEncrypted();
            var decryptedArrayBuffer = dataProtector.Unprotect(encryptedSettings);
            return emailSettingsConverter.ToEmailEettings(decryptedArrayBuffer);
        }

        public void SetSettings(EmailSettings settings)
        {
            var emailSettingsBufferArray = emailSettingsConverter.ToByteBuffer(settings);
            var encryptedSettings = dataProtector.Protect(emailSettingsBufferArray);
            keysService.SetEncrypted(encryptedSettings);
        }
    }
}
