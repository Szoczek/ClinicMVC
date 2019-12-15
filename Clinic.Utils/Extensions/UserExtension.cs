using Clinic.Database.Models;

namespace Clinic.Utils.Extensions
{
    public static class UserExtension
    {
        public static bool IsDoctor(this User user) => user.Doctor != null;
        public static bool IsPatient(this User user) => user.Patient != null;

        public static string GetFullName(this User user) => user.FirstName + " " + user.LastName;
    }
}
