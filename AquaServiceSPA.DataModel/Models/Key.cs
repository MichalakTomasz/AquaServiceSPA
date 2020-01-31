using System;

namespace AquaServiceSPA.DataModel
{
    public class Key
    {
        public Guid ID { get; set; }
        public byte[] EncryptedKey { get; set; }
    }
}
