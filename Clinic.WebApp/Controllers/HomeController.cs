using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Clinic.WebApp.Models;
using Clinic.Services.Implementations;
using Clinic.Utils;
using System.Threading.Tasks;

namespace ClinicMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly FakeDataService _dataService;
        public HomeController(FakeDataService dataService) => this._dataService = dataService;
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult About()
        {
            return View();
        }

        [Authorize(Roles = nameof(UserRoles.Admin))]
        public async Task GenerateData(int quantity)
        {
            await _dataService.GenerateDoctorsAsync(quantity);
            await _dataService.GeneratePatientsAsync(quantity);
            await _dataService.GenerateVisitsAsync(quantity);

            RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
