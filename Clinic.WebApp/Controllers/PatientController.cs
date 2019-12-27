using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Clinic.Services.Implementations;
using Clinic.Utils;
using Clinic.WebApp.Controllers;
using Clinic.WebApp.Models.PatientViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicMVC.Controllers
{
    [Authorize]
    public class PatientController : BaseController
    {
        private readonly UserService _userService;
        public PatientController(UserService userService)
        {
            this._userService = userService;
        }

        [Authorize(Roles = nameof(UserRoles.User))]
        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [Authorize(Roles = nameof(UserRoles.User))]
        public async Task<IActionResult> Create()
        {
            try
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
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(PatientViewModel vm)
        {
            try
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
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [Authorize(Roles = nameof(UserRoles.Patient))]
        public async Task<IActionResult> Account()
        {
            try
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
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Account(PatientViewModel vm)
        {
            try
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
            catch (Exception ex)
            {
                return Error(ex);
            }
        }
    }
}