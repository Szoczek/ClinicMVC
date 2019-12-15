using System.ComponentModel.DataAnnotations;

namespace Clinic.Database.Models.ExternalTypes
{
    public enum Specialties
    {
        Pediatrics,
        Surgery,
        [Display(Name = "Internal medicine")]
        InternalMedicine,
        [Display(Name = "Family medicine")]
        FamilyMedicine,
        Psychiatry,
        Pathology
    }
}
