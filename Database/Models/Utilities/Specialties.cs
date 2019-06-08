using System.ComponentModel.DataAnnotations;

namespace Database.Models.Utilities
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
