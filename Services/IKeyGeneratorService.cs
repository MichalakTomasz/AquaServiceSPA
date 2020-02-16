using AquaServiceSPA.Models;

namespace AquaServiceSPA.Services
{
    public interface IKeyGeneratorService
    {
        string Generate(int length, KeyTypes keyType = KeyTypes.Mixed);
    }
}