using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Clinic.Services.Abstract;
using Clinic.Utils;
using Clinic.WebApp.Controllers;
using Clinic.WebApp.Models.UserViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicMVC.Controllers
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = nameof(UserRoles.User))]
        public async Task<IActionResult> Account()
        {
            try
            {
                var user = await _userService
                .GetById(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
                return View(new UserViewModel()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Login = user.Login,
                    Password = user.Password
                });
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Account(UserViewModel vm)
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
            try
            {
                return View(new LoginViewModel());
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel vm, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var user = await _userService.Login(vm.Login, vm.Password);
                        var principal =  await _userService.Authenticate(user);
                        var authProperties = new AuthenticationProperties
                        {
                            AllowRefresh = true,
                            ExpiresUtc = DateTimeOffset.Now.AddDays(1),
                            IsPersistent = true,
                        };

                        await HttpContext
                            .SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
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
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync();

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            try
            {
                return View(new RegisterViewModel());
            }
            catch (Exception ex)
            {
                return Error(ex);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            try
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
            catch (Exception ex)
            {
                return Error(ex);
            }
        }
    }
}