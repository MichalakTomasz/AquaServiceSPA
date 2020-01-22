using AquaServiceSPA.Models;
using System.Threading.Tasks;

namespace AquaServiceSPA.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(Email email);
    }
}