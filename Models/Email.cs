using System.ComponentModel.DataAnnotations;

namespace AquaServiceSPA.Models
{
    public class Email
    {
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
    }
}
