using Database.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Services
{
    public interface IVisitService
    {
        Task<Visit> CreateVisit(Visit visit);
        Task<Visit> GetVisitById(string id);
        Task<Visit> GetVisitById(Guid id);
        Task DeleteVisit(Visit visit);
        Task<Visit> EditVisit(Visit visit);
        Task<IEnumerable<Visit>> GetPatientVisits(User patient);
        Task<IEnumerable<Visit>> GetDoctorVisits(User doctor);
    }
}
