using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ClinicMVC.Models.DoctorViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Implementations;

namespace ClinicMVC.Controllers
{
    [Authorize]
    public class DoctorController : Controller
    {
        private readonly UserService _userService;
        public DoctorController(UserService userService)
        {
            this._userService = userService;
        }
        [Authorize(Roles = "Doctor")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Account()
        {
            var user = await _userService
                .GetById(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            return View(new AccountViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Login = user.Login,
                Password = user.Password,
                DateOfBirth = user.Doctor.DateOfBirth,
                ContractId = user.Doctor.Contract.Id,
                EndDate = user.Doctor.Contract.EndDate,
                Salary = user.Doctor.Contract.Salary,
                Speciality = user.Doctor.Speciality,
                StartDate = user.Doctor.Contract.StartDate
            });
        }

        [HttpPost]
        public async Task<IActionResult> Account(AccountViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = vm.ConvertToDataModel();
                await _userService.PatchUser(user);

                var principal = await _userService.Authenticate(user);
                await HttpContext
                    .SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }

            return View(vm);
        }
    }
}