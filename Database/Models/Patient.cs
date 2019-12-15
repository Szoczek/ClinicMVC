using Clinic.Database.Models.ExternalTypes;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinic.Database.Models
{
    [Table("Patients")]
    public class Patient : BaseModel
    {
        public DateTime DateOfBirth { get; set; }
        public BloodTypes BloodType { get; set; }
    }
}
