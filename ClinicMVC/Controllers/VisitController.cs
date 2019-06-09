using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ClinicMVC.Models.VisitViewModels;
using Database.Models.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Implementations;

namespace ClinicMVC.Controllers
{
    [Authorize]
    public class VisitController : Controller
    {
        private readonly UserService _userService;
        private readonly VisitService _visitService;

        public VisitController(UserService userService, VisitService visitService)
        {
            this._userService = userService;
            this._visitService = visitService;
        }

        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Index()
        {
            var user = await _userService
                .GetById(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            var visits = await _visitService.GetPatientVisits(user);

            return View(visits);
        }

        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Create()
        {
            var user = await _userService
                .GetById(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            string nowstr = DateTime.Now.ToString("g", CultureInfo.CreateSpecificCulture("pl-PL"));
            DateTime now = DateTime.Parse(nowstr);

            return View(new CreateViewModel()
            {
                StartDate = now,
                PatientId = user.Id,
                Doctors = await _userService.GetDoctorsForSpeciality(Specialties.Pediatrics)
            });
        }


        [HttpGet]
        public JsonResult GetDoctorsWithSpeciality(Specialties speciality)
        {
            var doctors = new Dictionary<string, string>();
            _userService.GetDoctors()
               .Result
           .Where(x => x.IsDoctor() && x.Doctor.Speciality.Equals(speciality))
           .ToList()
           .ForEach(x => doctors.Add(x.Id.ToString(), x.GetFullName()));

            return Json(new { ok = false, data = doctors, message = "ok" });
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel vm)
        {
            if (vm.StartDate < DateTime.Now)
            {
                ModelState.AddModelError("StartDate", "Date must be in future!");
                vm.Doctors = await _userService.GetDoctorsForSpeciality(vm.Speciality);
                return View(vm);
            }

            if (ModelState.IsValid)
            {
                vm.StartDate = vm.StartDate.AddHours(2);
                var visit = vm.ConvertToDataModel();
                visit.Patient = await _userService.GetById(vm.PatientId);
                visit.Doctor = await _userService.GetById(vm.DoctorId);

                await _visitService
                .CreateVisit(visit);
                return RedirectToAction("Index");
            }
            vm.Doctors = await _userService.GetDoctorsForSpeciality(vm.Speciality);
            return View(vm);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var visit = await _visitService.GetVisitById(id);
            await _visitService.DeleteVisit(visit);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var visit = await _visitService.GetVisitById(id);
            return View(new EditViewModel()
            {
                Id = visit.Id,
                DoctorId = visit.Doctor.Id.ToString(),
                DoctorName = visit.Doctor.GetFullName(),
                PatientId = visit.Patient.Id,
                Speciality = visit.Doctor.Doctor.Speciality,
                StartDate = visit.StartDate,
                Doctors = await _userService.GetDoctorsForSpeciality(visit.Doctor.Doctor.Speciality, visit.Doctor)
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CreateViewModel vm)
        {
            if (vm.StartDate < DateTime.Now)
            {
                ModelState.AddModelError("StartDate", "Date must be in future!");
                return View(vm);
            }

            if (ModelState.IsValid)
            {
                vm.StartDate = vm.StartDate.AddHours(2);
                var visit = vm.ConvertToDataModel();
                visit.Doctor = await _userService.GetById(vm.DoctorId);
                visit.Patient = await _userService.GetById(vm.PatientId);
                await _visitService.EditVisit(visit);
                return RedirectToAction("Index");
            }

            return View(vm);
        }
    }
}