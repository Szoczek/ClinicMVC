using Database.Models.Utilities;
using System;

namespace Database.Models
{
    public class Patient
    {
        public Guid Id { get; set; }
        public DateTime DateOfBirth { get; set; }
        public BloodTypes BloodType { get; set; }

        public bool IsMinor() => DateTime.Now
                .Subtract(this.DateOfBirth)
                .TotalDays < Constants.ADULTHOOD_IN_DAYS;
    }
}
