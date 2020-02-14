using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaServiceSPA.Services
{
    public class CryptographicKeyService : ICryptographicKeyService
    {
        private readonly IEncryptedDataStoreService keyService;
        private readonly IGenericCryptographicService genericCryptographicService;
        private readonly ILoggerService loggerService;

        public CryptographicKeyService(
            IEnumerable<IEncryptedDataStoreService> keyServices,
            IGenericCryptographicService genericCryptographicService,
            ILoggerService loggerService)
        {
            keyService = keyServices
                .FirstOrDefault(f => f.GetType() == typeof(EncryptedKeyStoreService));
            this.genericCryptographicService = genericCryptographicService;
            this.loggerService = loggerService;
        }

        private string GenerateKey()
        {
            var random = new Random();
            return random.Next(int.MaxValue).ToString();
        }
        public string GetKey()
        {
            var encryptedKey = keyService.GetEncrypted();
            if (encryptedKey != null)
            {
                var decryptedKeyArray = genericCryptographicService.Decrypt(encryptedKey);

                if (decryptedKeyArray == null)
                    loggerService.Log("CryptographicKeyService-GetKey Decrypt returned null");
                else loggerService.Log("CryptographicKeyService-GetKey Decrypt returned not null value");

                return Encoding.Unicode.GetString(decryptedKeyArray);
            }
            else
            {
                var newKey = GenerateKey();

                if (newKey == null)
                    loggerService.Log("CryptographicKeyService-GetKey GenerateKey returned null");
                else
                    loggerService.Log("CryptographicKeyService-GetKey Decrypt returned not null value");

                var keyArray = Encoding.Unicode.GetBytes(newKey);
                var encryptedKeyArray = genericCryptographicService.Encrypt(keyArray);
                keyService.SetEncrypted(encryptedKeyArray);
                return newKey;
            }
        }
    }
}
