﻿using AquaServiceSPA.Models;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace AquaServiceSPA.Services
{
    public class EmailSettingsConverter : IEmailSettingsConverter
    {
        private readonly ILoggerService loggerService;
        public EmailSettingsConverter(ILoggerService loggerService)
            => this.loggerService = loggerService;

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
                return default;
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
                loggerService.Log($"EmailSettingsConverter error: {e.Message}");
                return default;
            }
        }
    }
}
