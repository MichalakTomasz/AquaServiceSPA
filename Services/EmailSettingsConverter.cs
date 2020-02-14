using AquaServiceSPA.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AquaServiceSPA.Services
{
    public class EmailSettingsConverter : IEmailSettingsConverter
    {
        public byte[] ToByteBuffer(EmailSettings emailAddressSettings)
        {
            try
            {
                var binaryFormatter = new BinaryFormatter();
                using (var memoryStream = new MemoryStream())
                {
                    binaryFormatter.Serialize(memoryStream, emailAddressSettings);
                    return memoryStream.ToArray();
                }
            }
            catch (Exception)
            {
                return default(byte[]);
            }
        }

        public EmailSettings ToEmailSettings(byte[] buffer)
        {
            try
            {
                var binaryFormatter = new BinaryFormatter();
                using (var memoryStream = new MemoryStream(buffer))
                {
                    return (EmailSettings)binaryFormatter.Deserialize(memoryStream);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"EmailSettingsConverter error: {e.Message}");
                return default(EmailSettings);
            }
        }
    }
}
