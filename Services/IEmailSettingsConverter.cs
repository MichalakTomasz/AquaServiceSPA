using AquaServiceSPA.Models;

namespace AquaServiceSPA.Services
{
    public interface IEmailSettingsConverter
    {
        byte[] ToByteBuffer(EmailSettings emailAddressSettings);
        EmailSettings ToEmailSettings(byte[] buffer);
    }
}