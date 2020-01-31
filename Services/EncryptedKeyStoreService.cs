using AquaServiceSPA.DataModel;
using System.Linq;

namespace AquaServiceSPA.Services
{
    public class EncryptedKeyStoreService : IEncryptedDataStoreService
    {
        private readonly AquaServiceSPADBContext dbBContext;

        public EncryptedKeyStoreService(AquaServiceSPADBContext dbContext)
            => dbBContext = dbContext;

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
