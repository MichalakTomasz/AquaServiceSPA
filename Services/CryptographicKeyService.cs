using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AquaServiceSPA.Services
{
    public class CryptographicKeyService : ICryptographicKeyService
    {
        private readonly IEncryptedDataStoreService keyService;
        private readonly IGenericCryptographicService genericCryptographicService;
        private readonly ILoggerService loggerService;
        private readonly IConfiguration configuration;

        public CryptographicKeyService(
            IEnumerable<IEncryptedDataStoreService> keyServices,
            IGenericCryptographicService genericCryptographicService,
            ILoggerService loggerService,
            IConfiguration configuration)
        {
            keyService = keyServices
                .FirstOrDefault(f => f.GetType() == typeof(EncryptedKeyStoreService));
            this.genericCryptographicService = genericCryptographicService;
            this.loggerService = loggerService;
            this.configuration = configuration;
        }

        private string GenerateKey()
        {
            var random = new Random();
            return random.Next(int.MaxValue).ToString(CultureInfo.InvariantCulture);
        }
        public string GetKey()
        {
            return configuration["Key"]; 
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
