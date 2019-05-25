using Database.Models;
using System.ComponentModel.DataAnnotations;

namespace ClinicMVC.Models.UserViewModels
{
    public class RegisterViewModel
    {
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
            User user = new User();

            user.Login = this.Login;
            user.Password = this.Password;
            user.FirstName = this.FirstName;
            user.LastName = this.LastName;

            return user;
        }
    }
}
