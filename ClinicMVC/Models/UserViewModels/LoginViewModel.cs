using System.ComponentModel.DataAnnotations;

namespace ClinicMVC.Models.UserViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Login { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
