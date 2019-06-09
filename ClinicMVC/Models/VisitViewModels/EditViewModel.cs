using Database.Models;
using Database.Models.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClinicMVC.Models.VisitViewModels
{
    public class EditViewModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Date")]
        public DateTime StartDate { get; set; }
        [Required]
        public Guid PatientId { get; set; }
        [Required]
        public Specialties Speciality { get; set; }
        [Required]
        [Display(Name = "Doctor")]
        public string DoctorId { get; set; }
        public string DoctorName { get; set; }
        public Dictionary<string, string> Doctors { get; set; }

        public Visit ConvertToDataModel()
        {
            var visit = new Visit();
            visit.Id = Id;
            visit.StartDate = StartDate;

            return visit;
        }
    }
}
