using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AquaServiceSPA.DataModel
{
    public class Visit
    {
        public Guid ID { get; set; }
        [MaxLength(15)]
        public string IP { get; set; }
        public DateTime Date { get; set; }
    }
}
