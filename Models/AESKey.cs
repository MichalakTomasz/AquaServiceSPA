using System;

namespace AquaServiceSPA.Models
{
    [Serializable]
    public class AESKey
    {
        public byte[] Key { get; set; }
        public byte[] IV { get; set; }
    }
}
