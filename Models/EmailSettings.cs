using System;
using System.ComponentModel.DataAnnotations;

namespace AquaServiceSPA.Models
{
    [Serializable]
    public class EmailSettings
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        public string Smtp { get; set; }
        [Required]
        [Range(minimum: 0, maximum: 9999)]
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public bool IsHtmlMessage { get; set; }
    }
}
