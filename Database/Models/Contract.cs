using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinic.Database.Models
{
    [Table("Contracts")]
    public class Contract : BaseModel
    {
        public DateTime StartDate { get; set; }
        public decimal Salary { get; set; }
        public DateTime EndDate { get; set; }
    }
}
