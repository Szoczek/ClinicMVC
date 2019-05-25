using System;
using System.Collections.Generic;

namespace Database.Models
{
    public class About
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public IEnumerable<string> OpeningHours { get; set; }
    }
}
