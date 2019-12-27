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
            visit.Id = Guid.NewGuid();
            await _unitOfWork.VisitRepository.Create(visit);
            return await _unitOfWork.VisitRepository.Get(visit.Id);
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
            return await _dataContext.GetCollection<Visit>()
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id.Equals(visitId));
        }

        public async Task<Visit> GetVisitById(Guid id)
        {
            return await _dataContext.GetCollection<Visit>()
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task DeleteVisit(Visit visit)
        {
            await _dataContext.GetCollection<Visit>()
               .FindOneAndDeleteAsync(Builders<Visit>.Filter.Where(x => x.Id.Equals(visit.Id)));
        }

        public async Task<Visit> EditVisit(Visit visit)
        {
            var editedVisit = await _dataContext.GetCollection<Visit>()
               .FindOneAndReplaceAsync(Builders<Visit>.Filter.Where(x => x.Id.Equals(visit.Id)), visit);
            return editedVisit;
        }
    }
}
