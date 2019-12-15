using Clinic.Database.Models;
using Clinic.Database.Models.ExternalTypes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Clinic.WebApp.Models.DoctorViewModels
{
    public class DoctorViewModel
    {
        [Required]
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
        public Specialties Speciality { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public string Salary { get; set; }
        [Required]
        public Guid ContractId { get; set; }


        public User ConvertToDataModel()
        {
            var user = new User
            {
                Id = this.Id,
                Login = this.Login,
                Password = this.Password,
                FirstName = this.FirstName,
                LastName = this.LastName
            };

            var doctor = new Doctor
            {
                Id = this.Id,
                DateOfBirth = this.DateOfBirth,
                Speciality = Speciality
            };
            user.Doctor = doctor;

            var contract = new Contract
            {
                Id = this.ContractId,
                Salary = decimal.Parse(this.Salary),
                StartDate = this.StartDate,
                EndDate = this.EndDate
            };
            user.Doctor.Contract = contract;

            return user;
        }
    }
}
