using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Clinic.Services.Abstract;
using Clinic.Utils;
using Clinic.WebApp.Controllers;
using Clinic.WebApp.Models.DoctorViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicMVC.Controllers
{
    [Authorize]
    public class DoctorController : BaseController
    {
        private readonly IUserService _userService;
        public DoctorController(IUserService userService)
        {
            this._userService = userService;
        }

        [Authorize(Roles = nameof(UserRoles.Doctor))]
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

        [Authorize(Roles = nameof(UserRoles.Doctor))]
        public async Task<IActionResult> Account()
        {
            try
            {
                var user = await _userService
                    .GetById(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

                return View(new DoctorViewModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Login = user.Login,
                    Password = user.Password,
                    DateOfBirth = user.Doctor.DateOfBirth,
                    ContractId = user.Doctor.Contract.Id,
                    EndDate = user.Doctor.Contract.EndDate,
                    Salary = Math.Round(user.Doctor.Contract.Salary, 2).ToString(),
                    Speciality = user.Doctor.Speciality,
                    StartDate = user.Doctor.Contract.StartDate
                });
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Account(DoctorViewModel vm)
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