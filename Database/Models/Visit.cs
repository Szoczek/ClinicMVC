using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinic.Database.Models
{
    [Table("Visits")]
    public class Visit : BaseModel
    {
        public DateTime StartDate { get; set; }
        public User Patient { get; set; }
        public User Doctor { get; set; }
        public DateTime EndDate { get; set; }
        public string Notes { get; set; }
    }
}
