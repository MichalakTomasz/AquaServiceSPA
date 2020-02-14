using AquaServiceSPA.DataModel;
using System.Linq;

namespace AquaServiceSPA.Services
{
    public class EncryptedKeyStoreService : IEncryptedDataStoreService
    {
        private readonly AquaServiceSPADBContext dbBContext;
        private readonly ILoggerService loggerService;

        public EncryptedKeyStoreService(
            AquaServiceSPADBContext dbContext,
            ILoggerService loggerService)
        {
            dbBContext = dbContext;
            this.loggerService = loggerService;
        }

        public byte[] GetEncrypted()
        {
            var result = dbBContext.KeyTable.FirstOrDefault()?.EncryptedKey;
            if (result != null)
                loggerService.Log("Encrypted key loaded form database correctly.");
            else loggerService.Log("While loading encrypted key from database was returned null.");
            return result;
        }

        public void SetEncrypted(byte[] key)
        {
            var encryptedTempKey = dbBContext.KeyTable.FirstOrDefault();
            if (encryptedTempKey == null)
                dbBContext.KeyTable.Add(new Key { EncryptedKey = key });
            else
                encryptedTempKey.EncryptedKey = key;

            dbBContext.SaveChanges();
        }
    }
}
