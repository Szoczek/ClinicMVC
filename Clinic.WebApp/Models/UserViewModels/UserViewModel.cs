using Clinic.Database.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Clinic.WebApp.Models.UserViewModels
{
    public class UserViewModel
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

        public User ConvertToDataModel()
        {
            User user = new User
            {
                Id = Id,
                Login = Login,
                Password = Password,
                FirstName = FirstName,
                LastName = LastName
            };

            return user;
        }
    }
}
