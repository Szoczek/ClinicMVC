using Clinic.Database.Implementations;
using Clinic.Database.Models;
using Clinic.Services.Abstract;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Services.Implementations
{
    public class VisitService : ServiceBase, IVisitService
    {
        public VisitService(UnitOfWork uow) : base(uow) { }

        public async Task<Visit> CreateVisit(Visit visit)
        {
            await _unitOfWork.VisitRepository.Create(visit);
            return await GetVisitById(visit.Id);
        }
        public async Task<IEnumerable<Visit>> GetDoctorVisits(User doctor)
        {
            var visits = await _unitOfWork.VisitRepository.GetAll();
            return visits.Where(x => x.Doctor.Id.Equals(doctor.Id)).ToList();
        }
        public async Task<IEnumerable<Visit>> GetPatientVisits(User patient)
        {
            var visits = await _unitOfWork.VisitRepository.GetAll();
            return visits.Where(x => x.Patient.Id.Equals(patient.Id))
                 .ToList();
        }
        public async Task<Visit> GetVisitById(string id)
        {
            Guid visitId = Guid.Parse(id);
            return await GetVisitById(visitId);
        }
        public async Task<Visit> GetVisitById(Guid id)
        {
            return await _unitOfWork.VisitRepository.GetById(id);
        }
        public async Task DeleteVisit(Visit visit)
        {
            await _unitOfWork.VisitRepository.Delete(visit);
        }
        public async Task<Visit> EditVisit(Visit visit)
        {
            await _unitOfWork.VisitRepository.Update(visit);
            return await GetVisitById(visit.Id);
        }
    }
}
