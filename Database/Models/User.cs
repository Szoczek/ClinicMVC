using System;

namespace Database.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public bool IsAdmin { get; set; }

        public bool IsDoctor() => this.Doctor != null;
        public bool IsPatient() => this.Patient != null;

        public string GetFullName() => this.FirstName + " " + this.LastName;
    }
}
