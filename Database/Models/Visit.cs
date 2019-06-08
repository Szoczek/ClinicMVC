using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models
{
    public class Visit
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public User Patient { get; set; }
        public User Doctor { get; set; }
        public DateTime EndDate { get; set; }
        public string Notes { get; set; }

    }
}
