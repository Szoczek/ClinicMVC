using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ClinicMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Clinic.Services.Implementations;

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

        [Authorize(Roles = "Admin")]
        public void GenerateData(int quantity)
        {
            _dataService.GenerateDoctors(quantity);
            _dataService.GeneratePatients(quantity);
            _dataService.GenerateVisits(quantity);

            RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
