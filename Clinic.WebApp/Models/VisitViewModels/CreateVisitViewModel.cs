using Clinic.Database.Models;
using Clinic.Database.Models.ExternalTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Clinic.WebApp.Models.VisitViewModels
{
    public class CreateVisitViewModel
    {
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
        public Dictionary<string, string> Doctors { get; set; }

        public Visit ConvertToDataModel()
        {
            var visit = new Visit
            {
                Id = Id,
                StartDate = StartDate
            };

            return visit;
        }
    }
}
