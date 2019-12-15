using System.ComponentModel.DataAnnotations.Schema;

namespace Clinic.Database.Models
{
    [Table("Users")]
    public class User : BaseModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public bool IsAdmin { get; set; }
    }
}
