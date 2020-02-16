using AquaServiceSPA.Models;

namespace AquaServiceSPA.Services
{
    public interface IAESCryptographicService
    {
        AESKey Key { get; set; }

        byte[] Decrypt(byte[] encryptedBuffer);
        byte[] Encrypt(byte[] buffer);
        AESKey GenerateKey();
    }
}