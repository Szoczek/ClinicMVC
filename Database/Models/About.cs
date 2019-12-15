using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinic.Database.Models
{
    [Table("Abouts")]
    public class About : BaseModel
    {
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public List<string> OpeningHours { get; set; }

        public About()
        {
            OpeningHours = new List<string>();
        }
    }
}
