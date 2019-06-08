using Database.Models.Utilities;
using System;
using System.Collections.Generic;

namespace Database.Models
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public Contract Contract { get; set; }
        public Specialties Speciality { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
