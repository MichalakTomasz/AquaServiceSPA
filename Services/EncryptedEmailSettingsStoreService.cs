using AquaServiceSPA.DataModel;
using System.Linq;

namespace AquaServiceSPA.Services
{
    public class EncryptedEmailSettingsStoreService : IEncryptedDataStoreService
    {
        private readonly AquaServiceSPADBContext dbContext;
        private readonly ILoggerService loggerService;

        public EncryptedEmailSettingsStoreService(
            AquaServiceSPADBContext dbContext,
            ILoggerService loggerService)
        {
            this.dbContext = dbContext;
            this.loggerService = loggerService;
        }

        public byte[] GetEncrypted()
        {
            var result = dbContext.EmailSettings.FirstOrDefault()?.EncryptedBuffer;
            if (result != null)
                loggerService.Log("Encrypted email settings loaded form database correctly.");
            else loggerService.Log("While loading encrypted email settings from database was returned null.");
            return result;
        }

        public void SetEncrypted(byte[] buffer)
        {
            var encryptedTempEmailSettings = dbContext.EmailSettings.FirstOrDefault();
            if (encryptedTempEmailSettings == null)
                dbContext.EmailSettings.Add(new EmailSettingsTable { EncryptedBuffer = buffer });
            else
                encryptedTempEmailSettings.EncryptedBuffer = buffer;

            dbContext.SaveChanges();
        }
    }
}
