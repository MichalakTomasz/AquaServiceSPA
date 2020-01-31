using AquaServiceSPA.Models;
using Microsoft.AspNetCore.DataProtection;
using System.Collections.Generic;
using System.Linq;

namespace AquaServiceSPA.Services
{
    public class EmailSettingsService : IEmailSettingsService
    {
        private readonly IEmailSettingsConverter emailSettingsConverter;
        private readonly IDataProtector dataProtector;
        private readonly IEncryptedDataStoreService encryptedDtaStoreService;

        public EmailSettingsService(
            IEmailSettingsConverter emailSettingsConverter,
            IDataProtectionProvider dataProtector,
            IEnumerable<IEncryptedDataStoreService> encryptedDataStoreServices,
            ICryptographicKeyService cryptographicKeyService)
        {
            var key = cryptographicKeyService.GetKey();
            this.emailSettingsConverter = emailSettingsConverter;
            this.dataProtector = dataProtector.CreateProtector(key);
            encryptedDtaStoreService = encryptedDataStoreServices
                .FirstOrDefault(f => f.GetType() == typeof(EncryptedEmailSettingsStoreService));
        }

        public EmailSettings GetSettings()
        {
            var encryptedSettings = encryptedDtaStoreService.GetEncrypted();
            var decryptedArrayBuffer = dataProtector.Unprotect(encryptedSettings);
            return emailSettingsConverter.ToEmailEettings(decryptedArrayBuffer);
        }

        public void SetSettings(EmailSettings settings)
        {
            var emailSettingsBufferArray = emailSettingsConverter.ToByteBuffer(settings);
            var encryptedSettings = dataProtector.Protect(emailSettingsBufferArray);
            encryptedDtaStoreService.SetEncrypted(encryptedSettings);
        }
    }
}