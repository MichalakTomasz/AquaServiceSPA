using AquaServiceSPA.DataModel;
using System.Linq;

namespace AquaServiceSPA.Services
{
    public class EncryptedEmailSettingsStoreService : IEncryptedDataStoreService
    {
        private readonly AquaServiceSPADBContext dbContext;

        public EncryptedEmailSettingsStoreService(AquaServiceSPADBContext dbContext)
            => this.dbContext = dbContext;

        public byte[] GetEncrypted()
            => dbContext.EmailSettings.FirstOrDefault()?.Buffer;

        public void SetEncrypted(byte[] buffer)
        {
            var encryptedTempEmailSettings = dbContext.EmailSettings.FirstOrDefault();
            if (encryptedTempEmailSettings == null)
                dbContext.EmailSettings.Add(new EmailSettingsTable { Buffer = buffer });
            else
                encryptedTempEmailSettings.Buffer = buffer;

            dbContext.SaveChanges();
        }
    }
}
