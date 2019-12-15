using Clinic.Database.Models;
using Clinic.Database.Models.ExternalTypes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Clinic.WebApp.Models.PatientViewModels
{
    public class PatientViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [EmailAddress]
        public string Login { get; set; }
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
            var user = new User
            {
                Id = Id,
                Login = Login,
                Password = Password,
                FirstName = FirstName,
                LastName = LastName
            };
            var patient = new Patient
            {
                Id = Id,
                DateOfBirth = DateOfBirth,
                BloodType = BloodType
            };

            user.Patient = patient;
            return user;
        }
    }
}
