using Microsoft.EntityFrameworkCore;
using MultimedAPI.Models;
using MultimedAPI.Models.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultimedAPI.Data.Repositories
{
    public class TherapistRepository : ITherapistRepository
    {

        private readonly MultimedDbContext _dbContext;
        private readonly DbSet<Therapist> _therapists;

        public TherapistRepository(MultimedDbContext dbContext)
        {
            _dbContext = dbContext;
            _therapists = _dbContext.Therapists;
        }

        public IEnumerable<Therapist> GetAll()
        {
            return _therapists.Include(t => t.TherapistUsers).Include(t => t.OpeningTimes).ToList();
        }

        public Therapist GetById(int id)
        {
            return _therapists.Include(t => t.TherapistUsers).Include(t => t.OpeningTimes).SingleOrDefault(t => t.TherapistId == id);
        }

        public void UpdateTherapist(Therapist therapist)
        {
            _therapists.Update(therapist);
        }

        public void AddTherapist(Therapist therapist)
        {
            _therapists.Add(therapist);
        }

        public void DeleteTherapist(Therapist therapist)
        {
            _therapists.Remove(therapist);
        }

        

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

    }
}
