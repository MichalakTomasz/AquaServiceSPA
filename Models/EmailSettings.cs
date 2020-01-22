using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AquaServiceSPA.Models
{
    [Serializable]
    public class EmailSettings
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        public string Host { get; set; }
        [Required]
        [Range(minimum: 0, maximum: 9999)]
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public bool IsHtmlMessage { get; set; }
    }
}
