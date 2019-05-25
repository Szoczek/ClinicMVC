using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ClinicMVC.Models.PatientViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Implementations;

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

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Patient")]
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
                BloodType = user.Patient.BloodType,
                DateOfBirth = user.Patient.DateOfBirth
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