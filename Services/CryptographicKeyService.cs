using System;
using System.Text;

namespace AquaServiceSPA.Services
{
    public class CryptographicKeyService : ICryptographicKeyService
    {
        private readonly IKeyService keyService;
        private readonly IGenericCryptographicService genericCryptographicService;
        public CryptographicKeyService(
            IKeyService keyService,
            IGenericCryptographicService genericCryptographicService)
        {
            this.keyService = keyService;
            this.genericCryptographicService = genericCryptographicService;
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
                return Encoding.Unicode.GetString(decryptedKeyArray);
            }
            else
            {
                var newKey = GenerateKey();
                var keyArray = Encoding.Unicode.GetBytes(newKey);
                var encryptedKeyArray = genericCryptographicService.Encrypt(keyArray);
                keyService.SetEncrypted(encryptedKeyArray);
                return newKey;
            }
        }
    }
}
