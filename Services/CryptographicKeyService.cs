using AquaServiceSPA.Models;
using System.Collections.Generic;
using System.Linq;

namespace AquaServiceSPA.Services
{
    public class CryptographicKeyService : ICryptographicKeyService
    {
        private readonly IEncryptedDataStoreService keyService;
        private readonly IGenericCryptographicService genericCryptographicService;
        private readonly IAESCryptographicService aesCryptographicService;
        private readonly IAESKeyConverterService aesConverterService;

        public CryptographicKeyService(
            IEnumerable<IEncryptedDataStoreService> keyServices,
            IGenericCryptographicService genericCryptographicService,
            IAESCryptographicService aesCryptographicService,
            IAESKeyConverterService aesConverterService)
        {
            keyService = keyServices
                .FirstOrDefault(f => f.GetType() == typeof(EncryptedKeyStoreService));
            this.genericCryptographicService = genericCryptographicService;
            this.aesCryptographicService = aesCryptographicService;
            this.aesConverterService = aesConverterService;
        }

        private AESKey GenerateKey()
            => aesCryptographicService.GenerateKey();

        public AESKey GetKey()
        {
            var encryptedKey = keyService.GetEncrypted();
            if (encryptedKey != null)
            {
                var decryptedKeyArray = genericCryptographicService.Decrypt(encryptedKey);
                return aesConverterService.ToAESKey(decryptedKeyArray);
            }
            else
            {
                var newKey = GenerateKey();
                var keyArray = aesConverterService.ToByteBuffer(newKey);
                var encryptedKeyArray = genericCryptographicService.Encrypt(keyArray);
                keyService.SetEncrypted(encryptedKeyArray);
                return newKey;
            }
        }
    }
}
