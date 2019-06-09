using Database.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ClinicMVC.Models.VisitViewModels
{
    public class StartViewModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Date")]
        public DateTime StartDate { get; set; }
        [Required]
        public Guid PatientId { get; set; }
        [Display(Name = "Patient")]
        public string PatientName{ get; set; }
        [Required]
        public Guid DoctorId { get; set; }
        [Display(Name = "Doctor")]
        public string DoctorName { get; set; }
        [Required]
        public string Notes { get; set; }

        public Visit ConvertToDataModel()
        {
            var visit = new Visit();
            visit.Id = Id;
            visit.StartDate = StartDate;
            visit.EndDate = DateTime.Now.AddHours(2);
            visit.Notes = Notes;

            return visit;
        }
    }
}
