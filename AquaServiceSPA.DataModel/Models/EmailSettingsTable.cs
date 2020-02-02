using System;

namespace AquaServiceSPA.DataModel
{
    public class EmailSettingsTable
    {
        public Guid ID { get; set; }
        public byte[] EncryptedBuffer { get; set; }
    }
}
