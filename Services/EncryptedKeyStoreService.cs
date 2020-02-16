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
            => dbBContext.KeyTable.FirstOrDefault()?.EncryptedKey;

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
