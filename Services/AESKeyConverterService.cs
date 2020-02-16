using AquaServiceSPA.Models;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AquaServiceSPA.Services
{
    public class AESKeyConverterService : IAESKeyConverterService
    {
        private readonly ILoggerService loggerService;

        public AESKeyConverterService(ILoggerService loggerService)
            => this.loggerService = loggerService;

        public byte[] ToByteBuffer(AESKey emailAddressSettings)
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
                return default;
            }
        }

        public AESKey ToAESKey(byte[] buffer)
        {
            try
            {
                var binaryFormatter = new BinaryFormatter();
                using (var memoryStream = new MemoryStream(buffer))
                {
                    return (AESKey)binaryFormatter.Deserialize(memoryStream);
                }
            }
            catch (Exception e)
            {
                loggerService.Log($"AESKeyConverterService error: {e.Message}");
                return default;
            }
        }
    }
}

