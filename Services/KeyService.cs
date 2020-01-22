namespace AquaServiceSPA.Services
{
    public class KeyService : IKeyService
    {
        public byte[] GetEncrypted()
        {
            return key;
        }

        public void SetEncrypted(byte[] key)
        {
            this.key = key;
        }

        private byte[] key;
    }
}
