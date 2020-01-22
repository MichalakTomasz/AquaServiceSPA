using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AquaServiceSPA.Models
{
    public class Contact
    {
        public string EmailAddress { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
    }
}
