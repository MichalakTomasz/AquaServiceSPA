using AquaServiceSPA.DataModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace AquaServiceSPA.Services
{
    public class VisitService : IVisitService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly AquaServiceSPADBContext aquaDbContext;

        public VisitService(
            IHttpContextAccessor httpContextAccessor,
            AquaServiceSPADBContext aquaDbContext)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.aquaDbContext = aquaDbContext;
        }

        public async Task<string> AddAsync()
        {
            var ip = httpContextAccessor.HttpContext
                .Connection.RemoteIpAddress.ToString();
            await Task.Run(() =>
            {
                var visit = new Visit
                {
                    IP = ip,
                    Date = DateTime.Now
                };
                aquaDbContext.Visit.Add(visit);
                aquaDbContext.SaveChanges();
            });
            return ip;
        }
    }
}
