using Clinic.Database.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Clinic.WebApp.Models.VisitViewModels
{
    public class StartVisitViewModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Date")]
        public DateTime StartDate { get; set; }
        [Required]
        public Guid PatientId { get; set; }
        [Display(Name = "Patient")]
        public string PatientName { get; set; }
        [Required]
        public Guid DoctorId { get; set; }
        [Display(Name = "Doctor")]
        public string DoctorName { get; set; }
        [Required]
        public string Notes { get; set; }

        public Visit ConvertToDataModel()
        {
            var visit = new Visit
            {
                Id = Id,
                StartDate = StartDate,
                EndDate = DateTime.Now.AddHours(2),
                Notes = Notes
            };

            return visit;
        }
    }
}
