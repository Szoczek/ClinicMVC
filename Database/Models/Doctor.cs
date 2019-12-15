using Clinic.Database.Models.ExternalTypes;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinic.Database.Models
{
    [Table("Doctors")]
    public class Doctor : BaseModel
    {
        public Contract Contract { get; set; }
        public Specialties Speciality { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
