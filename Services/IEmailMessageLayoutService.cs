using AquaServiceSPA.Models;

namespace AquaServiceSPA.Services
{
    public interface IEmailMessageLayoutService
    {
        string ConfirmEmailMessage(string serviceName, string confirmLink);
        string ContactModMessage(Email email);
        string ContactUserFeedback(string userName, string serviceName);
        string ResetPasswordMessageOne(string serviceName, string confirmLink);
        string ResetPasswordMessageTwo(string serviceName, string userName, string password, string callbackUrl);
    }
}