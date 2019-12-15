using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Clinic.Services.Implementations;
using Clinic.WebApp.Models.PatientViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicMVC.Controllers
{
    [Authorize]
    public class PatientController : Controller
    {
        private readonly UserService _userService;
        public PatientController(UserService userService)
        {
            this._userService = userService;
        }

        [Authorize(Roles = "Patient")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create()
        {
            var user = await _userService
                .GetById(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            return View(new PatientViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Login = user.Login,
                Password = user.Password,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(PatientViewModel vm)
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

        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Account()
        {
            var user = await _userService
                .GetById(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            return View(new PatientViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Login = user.Login,
                Password = user.Password,
                BloodType = user.Patient.BloodType,
                DateOfBirth = user.Patient.DateOfBirth
            });
        }

        [HttpPost]
        public async Task<IActionResult> Account(PatientViewModel vm)
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