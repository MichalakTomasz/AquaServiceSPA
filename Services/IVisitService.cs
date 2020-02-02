using System.Threading.Tasks;

namespace AquaServiceSPA.Services
{
    public interface IVisitService
    {
        Task<string> AddAsync();
    }
}