using Database.Models;
using Database.Models.Utilities;
using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicMVC.Models.PatientViewModels
{
    public class AccountViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [EmailAddress]
        public string Login { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Date of birth")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [Display(Name = "Blood type")]
        public BloodTypes BloodType { get; set; }

        public User ConvertToDataModel()
        {
            var user = new User();

            user.Id = this.Id;
            user.Login = this.Login;
            user.Password = this.Password;
            user.FirstName = this.FirstName;
            user.LastName = this.LastName;

            user.Patient.Id = this.Id;
            user.Patient.DateOfBirth = this.DateOfBirth;
            user.Patient.BloodType = this.BloodType;

            return user;
        }
    }
}
