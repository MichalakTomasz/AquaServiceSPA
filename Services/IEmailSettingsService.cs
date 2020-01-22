using AquaServiceSPA.Models;

namespace AquaServiceSPA.Services
{
    public interface IEmailSettingsService
    {
        EmailSettings GetSettings();
        void SetSettings(EmailSettings settings);
    }
}