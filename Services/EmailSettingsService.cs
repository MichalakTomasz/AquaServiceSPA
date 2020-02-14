using AquaServiceSPA.Models;
using Microsoft.AspNetCore.DataProtection;
using System.Collections.Generic;
using System.Linq;

namespace AquaServiceSPA.Services
{
    public class EmailSettingsService : IEmailSettingsService
    {
        private readonly IEmailSettingsConverter emailSettingsConverter;
        private readonly ILoggerService loggerService;
        private readonly IDataProtector dataProtector;
        private readonly IEncryptedDataStoreService encryptedDtaStoreService;

        public EmailSettingsService(
            IEmailSettingsConverter emailSettingsConverter,
            IDataProtectionProvider dataProtector,
            IEnumerable<IEncryptedDataStoreService> encryptedDataStoreServices,
            ICryptographicKeyService cryptographicKeyService,
            ILoggerService loggerService)
        {
            var key = cryptographicKeyService.GetKey();
            this.emailSettingsConverter = emailSettingsConverter;
            this.loggerService = loggerService;
            this.dataProtector = dataProtector.CreateProtector(key);
            encryptedDtaStoreService = encryptedDataStoreServices
                .FirstOrDefault(f => f.GetType() == typeof(EncryptedEmailSettingsStoreService));
        }

        public EmailSettings GetSettings()
        {
            var encryptedSettings = encryptedDtaStoreService.GetEncrypted();

            if (encryptedSettings == null)
                loggerService.Log("EmailSettingsService-GetSettings - GetEncrypted returned null.");
            else loggerService.Log("EmailSettingsService-GetSettings - GetEncrypted are not null.");
            
            var decryptedArrayBuffer = dataProtector.Unprotect(encryptedSettings);
            
            if (decryptedArrayBuffer == null)
                loggerService.Log("EmailSettingsService-GetSettings - Unprotect returned null.");
            else loggerService.Log("EmailSettingsService-GetSettings - Unprotect are not null.");
            
            var emailSettings = emailSettingsConverter.ToEmailSettings(decryptedArrayBuffer);
            
            if (emailSettings == null)
                loggerService.Log("EmailSettingsService-GetSettings - converted email settings from byte buffer returned null.");
            else loggerService.Log("EmailSettingsService-GetSettings - converted email settings from byte buffer are not null.");

            return emailSettings;
        }

        public void SetSettings(EmailSettings settings)
        {
            var emailSettingsBufferArray = emailSettingsConverter.ToByteBuffer(settings);
            var encryptedSettings = dataProtector.Protect(emailSettingsBufferArray);
            encryptedDtaStoreService.SetEncrypted(encryptedSettings);
        }
    }
}