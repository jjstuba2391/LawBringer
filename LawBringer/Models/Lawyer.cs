using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LawBringer.Models
{
    public class Lawyer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }

        [System.ComponentModel.DataAnnotations.Display(Name = "What Do you need help with?")]
        public string HelpType { get; set; }
        [System.ComponentModel.DataAnnotations.Display(Name = "Select TimeSlot")]
        public DateTime TimeSlot { get; set; }

        public string[] AvailableHelpTypes { get; set; }
        public DateTime[] AvailableDays { get; set; }
        
    }
}