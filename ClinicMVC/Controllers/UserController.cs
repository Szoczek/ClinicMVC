using System;
using System.Threading.Tasks;
using ClinicMVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Implementations;

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
                    ModelState.AddModelError("Password", "Password or login invalid");
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