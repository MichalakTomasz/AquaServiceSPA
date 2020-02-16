using AquaServiceSPA.Models;

namespace AquaServiceSPA.Services
{
    public interface IAESKeyConverterService
    {
        AESKey ToAESKey(byte[] buffer);
        byte[] ToByteBuffer(AESKey emailAddressSettings);
    }
}