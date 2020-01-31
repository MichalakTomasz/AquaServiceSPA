using Microsoft.EntityFrameworkCore;

namespace AquaServiceSPA.DataModel
{
    public class AquaServiceSPADBContext : DbContext
    {
        public AquaServiceSPADBContext(DbContextOptions<AquaServiceSPADBContext> options) 
            : base(options) { }
        public DbSet<EmailSettingsTable> EmailSettings { get; set; }
        public DbSet<Key> KeyTable { get; set; }
    }
}
                                                                                                 