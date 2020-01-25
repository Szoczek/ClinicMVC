using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Clinic.Services.Implementations;
using Clinic.Utils;
using System.Threading.Tasks;
using Clinic.WebApp.Controllers;
using System;
using Clinic.Services.Abstract;

namespace Clinic.WebApp.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly IFakeDataService _dataService;
        public HomeController(IFakeDataService dataService) => this._dataService = dataService;

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

        [AllowAnonymous]
        public IActionResult Privacy()
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

        [AllowAnonymous]
        public IActionResult About()
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

        [Authorize(Roles = nameof(UserRoles.Admin))]
        public async Task GenerateData(int quantity)
        {
            try
            {
                await _dataService.GenerateDoctorsAsync(quantity);
                await _dataService.GeneratePatientsAsync(quantity);
                await _dataService.GenerateVisitsAsync(quantity);

                RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Error(ex);
            }
        }
    }
}
