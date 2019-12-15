using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Clinic.Services.Implementations;
using ClinicMVC.Models.UserViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicMVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "User")]
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
                Password = user.Password
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

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userService.Login(vm.Login, vm.Password);
                    var principal = await _userService.Authenticate(user);
                    await HttpContext
                        .SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Password", ex.Message);
                    vm.Password = string.Empty;
                    return View(vm);
                }

                if (string.IsNullOrWhiteSpace(returnUrl))
                    return RedirectToAction("Index", "Home");

                return Redirect(returnUrl);
            }

            vm.Password = string.Empty;
            return View(vm);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var user = vm.ConvertToDataModel();
                user = await _userService.Register(user);

                var principal = await _userService.Authenticate(user);
                await HttpContext
                    .SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }

            vm.Password = string.Empty;
            return View(vm);
        }
    }
}