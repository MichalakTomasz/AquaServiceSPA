namespace AquaServiceSPA.Services
{
    public interface IKeyService
    {
        void SetEncrypted(byte[] key);
        byte[] GetEncrypted();
    }
}
