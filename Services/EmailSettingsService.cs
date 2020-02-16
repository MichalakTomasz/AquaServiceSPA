using AquaServiceSPA.Models;
using System.Collections.Generic;
using System.Linq;

namespace AquaServiceSPA.Services
{
    public class EmailSettingsService : IEmailSettingsService
    {
        private readonly IEmailSettingsConverter emailSettingsConverter;
        private readonly IEncryptedDataStoreService encryptedDtaStoreService;
        private readonly IAESCryptographicService aesCryptographicService;

        public EmailSettingsService(
            IEmailSettingsConverter emailSettingsConverter,
            IEnumerable<IEncryptedDataStoreService> encryptedDataStoreServices,
            IAESCryptographicService aesCryptographicService,
            ICryptographicKeyService cryptographicKeyService)
        {
            this.emailSettingsConverter = emailSettingsConverter;
            encryptedDtaStoreService = encryptedDataStoreServices
                .FirstOrDefault(f => f.GetType() == typeof(EncryptedEmailSettingsStoreService));
            this.aesCryptographicService = aesCryptographicService;
            aesCryptographicService.Key = cryptographicKeyService.GetKey();
        }

        public EmailSettings GetSettings()
        {
            var encryptedSettings = encryptedDtaStoreService.GetEncrypted();
            var decryptedArrayBuffer = aesCryptographicService.Decrypt(encryptedSettings);
            return emailSettingsConverter.ToEmailSettings(decryptedArrayBuffer);
        }

        public void SetSettings(EmailSettings settings)
        {
            var emailSettingsBufferArray = emailSettingsConverter.ToByteBuffer(settings);
            var encryptedSettings = aesCryptographicService.Encrypt(emailSettingsBufferArray);
            encryptedDtaStoreService.SetEncrypted(encryptedSettings);
        }
    }
}