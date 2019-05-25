using System;

namespace Database.Models
{
    public class Contract
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; }
        public decimal Salary { get; set; }
        public DateTime EndDate { get; set; }
    }
}
