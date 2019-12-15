using Clinic.Database.Models;
using System;

namespace Clinic.Utils.Extensions
{
    public static class PatientExtension
    {
        public static bool IsMinor(this Patient patient) => DateTime.Now
                .Subtract(patient.DateOfBirth)
                .TotalDays < Consts.ADULTHOOD_IN_DAYS;
    }
}
