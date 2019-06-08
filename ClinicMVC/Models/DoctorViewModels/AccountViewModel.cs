using Database.Models;
using Database.Models.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClinicMVC.Models.DoctorViewModels
{
    public class AccountViewModel
    {
        [Required]
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
        public Specialties Speciality { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public decimal Salary { get; set; }
        [Required]
        public Guid ContractId { get; set; }


        public User ConvertToDataModel()
        {
            var user = new User();
            user.Id = this.Id;
            user.Login = this.Login;
            user.Password = this.Password;
            user.FirstName = this.FirstName;
            user.LastName = this.LastName;

            var doctor = new Doctor();
            doctor.Id = this.Id;
            doctor.DateOfBirth = this.DateOfBirth;
            doctor.Speciality = Speciality;
            user.Doctor = doctor;

            var contract = new Contract();
            contract.Id = this.ContractId;
            contract.StartDate = this.StartDate;
            contract.EndDate = this.EndDate;
            contract.Salary = this.Salary;
            user.Doctor.Contract = contract;

            return user;
        }
    }
}
