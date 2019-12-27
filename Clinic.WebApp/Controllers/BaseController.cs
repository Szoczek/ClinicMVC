using Clinic.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace Clinic.WebApp.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Error()
        {
            return Error(null);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(Exception ex)
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = ex?.Message ?? "None" });
        }
    }
}
