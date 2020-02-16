using AquaServiceSPA.Models;

namespace AquaServiceSPA.Services
{
    public interface ICryptographicKeyService
    {
        AESKey GetKey();
    }
}