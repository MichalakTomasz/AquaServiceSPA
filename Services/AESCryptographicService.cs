using AquaServiceSPA.Models;
using System.IO;
using System.Security.Cryptography;

namespace AquaServiceSPA.Services
{
    public class AESCryptographicService : IAESCryptographicService
    {
        private readonly ILoggerService loggerService;

        public AESKey Key { get; set; }
        public AESCryptographicService(ILoggerService loggerService)
            => this.loggerService = loggerService;

        public byte[] Encrypt(byte[] buffer)
        {
            try
            {
                using (var aesAlgoritm = new AesCryptoServiceProvider())
                {
                    aesAlgoritm.Key = Key.Key;
                    aesAlgoritm.IV = Key.IV;
                    ICryptoTransform encryptor =
                        aesAlgoritm.CreateEncryptor(aesAlgoritm.Key, aesAlgoritm.IV);
                    using (var encryptedMemoryStream = new MemoryStream())
                    using (var sourceStream = new MemoryStream(buffer))
                    {
                        using (var cryptoStream = new CryptoStream(
                            encryptedMemoryStream, encryptor, CryptoStreamMode.Write))
                            sourceStream.CopyTo(cryptoStream);
                        return encryptedMemoryStream.ToArray();
                    }
                }
            }
            catch (System.Exception e)
            {
                loggerService.Log($"AES encrypt error: {e.Message}");
                return default;
            }
        }

        public byte[] Decrypt(byte[] encryptedBuffer)
        {
            try
            {
                using (var decryptedStream = new MemoryStream())
                {
                    using (var aesAlg = new AesCryptoServiceProvider())
                    {
                        aesAlg.Key = Key.Key;
                        aesAlg.IV = Key.IV;
                        ICryptoTransform decryptor =
                            aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                        using (var msDecrypt = new MemoryStream(encryptedBuffer))
                        using (var csDecrypt = new CryptoStream(
                            msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            csDecrypt.CopyTo(decryptedStream);
                            return decryptedStream.ToArray();
                        }
                    }
                }
            }
            catch (System.Exception e)
            {
                loggerService.Log($"AES decrypt errer: {e.Message}");
                return default;
            }
        }

        public AESKey GenerateKey()
        {
            try
            {
                var aes = Aes.Create();
                return new AESKey { Key = aes.Key, IV = aes.IV };
            }
            catch (System.Exception e)
            {
                loggerService.Log($"AES generate key errer: {e.Message}");
                return default;
            }
        }
    }
}