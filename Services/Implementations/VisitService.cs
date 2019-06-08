using Database;
using Database.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class VisitService : IVisitService
    {
        private readonly DataContext _dataContext;
        public VisitService(DataContext dataContext) => this._dataContext = dataContext;

        public async Task<Visit> CreateVisit(Visit visit)
        {
            visit.Id = Guid.NewGuid();
            await _dataContext.GetCollection<Visit>().InsertOneAsync(visit);

            return await _dataContext.GetCollection<Visit>()
                .AsQueryable()
                .FirstOrDefaultAsync(x => x.Id.Equals(visit.Id));
        }

        public async Task<IEnumerable<Visit>> GetDoctorVisits(User doctor)
        {
            return await _dataContext.GetCollection<Visit>()
                .AsQueryable()
                .Where(x => x.Doctor.Id.Equals(doctor.Id))
                .ToListAsync();
        }

        public async Task<IEnumerable<Visit>> GetPatientVisits(User patient)
        {
            return await _dataContext.GetCollection<Visit>()
                 .AsQueryable()
                 .Where(x => x.Patient.Id.Equals(patient.Id))
                 .ToListAsync();
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
