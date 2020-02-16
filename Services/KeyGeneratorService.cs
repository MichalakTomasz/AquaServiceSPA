using AquaServiceSPA.Models;
using System;
using System.Collections.Generic;

namespace AquaServiceSPA.Services
{
    public class KeyGeneratorService : IKeyGeneratorService
    {
        public string Generate(int length, KeyTypes keyType = KeyTypes.Mixed)
        {
            var random = new Random();
            var tempPassword = new List<char>(length);
            var valueVarint = 0;
            for (var index = 0; index < tempPassword.Count; index++)
            {
                switch (keyType)
                {
                    case KeyTypes.Mixed:
                        valueVarint = random.Next(3);
                        break;
                    case KeyTypes.Digits:
                        valueVarint = 0;
                        break;
                    case KeyTypes.Letters:
                        valueVarint = random.Next(2) + 1;
                        break;
                }

                switch (valueVarint)
                {
                    case 0:
                        tempPassword[index] = (char)(random.Next(10) + 48);
                        break;
                    case 1:
                        tempPassword[index] = (char)(random.Next(26) + 65);
                        break;
                    case 2:
                        tempPassword[index] = (char)(random.Next(26) + 97);
                        break;
                }
            }
            return new string(tempPassword.ToArray());
        }
    }
}
