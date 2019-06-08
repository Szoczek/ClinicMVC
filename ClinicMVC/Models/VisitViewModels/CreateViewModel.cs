using Database.Models;
using Database.Models.Utilities;
using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicMVC.Models.VisitViewModels
{
    public class CreateViewModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Date")]
        public DateTime StartDate { get; set; }
        [Required]
        public User Patient { get; set; }
        [Required]
        public Specialties Speciality { get; set; }
        [Required]
        [Display(Name = "Doctor")]
        public string DoctorId { get; set; }

        public Visit ConvertToDataModel()
        {
            var visit = new Visit();
            visit.Id = Id;
            visit.StartDate = StartDate;
            visit.Patient = Patient;

            return visit;
        }
    }
}
