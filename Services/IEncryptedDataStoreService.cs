namespace AquaServiceSPA.Services
{
    public interface IEncryptedDataStoreService
    {
        void SetEncrypted(byte[] buffer);
        byte[] GetEncrypted();
    }
}
