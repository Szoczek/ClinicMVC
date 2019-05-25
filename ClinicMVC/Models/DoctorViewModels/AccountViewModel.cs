using Database.Models;
using Database.Models.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClinicMVC.Models.DoctorViewModels
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
        public IEnumerable<Specialties> Specialties { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public decimal Salary { get; set; }
        public Guid ContractId { get; set; }


        public User ConvertToDataModel()
        {
            var user = new User();

            user.Id = this.Id;
            user.Login = this.Login;
            user.Password = this.Password;
            user.FirstName = this.FirstName;
            user.LastName = this.LastName;

            user.Doctor.Id = this.Id;
            user.Doctor.DateOfBirth = this.DateOfBirth;
            user.Doctor.Specialties = Specialties;

            user.Doctor.Contract.Id = this.ContractId;
            user.Doctor.Contract.StartDate = this.StartDate;
            user.Doctor.Contract.EndDate = this.EndDate;
            user.Doctor.Contract.Salary = this.Salary;

            return user;
        }
    }
}
