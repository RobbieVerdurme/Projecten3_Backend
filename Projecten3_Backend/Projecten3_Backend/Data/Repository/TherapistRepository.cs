using Microsoft.EntityFrameworkCore;
using Projecten3_Backend.Data.IRepository;
using Projecten3_Backend.Model;
using Projecten3_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.Data.Repository
{
    public class TherapistRepository : ITherapistRepository
    {
        #region  prop
        private readonly Projecten3_BackendContext _dbContext;
        private readonly DbSet<Therapist> _therapists;
        #endregion

        #region ctor
        public TherapistRepository(Projecten3_BackendContext dbContex)
        {
            _dbContext = dbContex;
            _therapists = _dbContext.Therapist;
        }
        #endregion

        public void AddTherapist(Therapist therapist)
        {
            _therapists.Add(therapist);
        }

        public void DeleteTherapist(int id)
        {
            Therapist th = _therapists.FirstOrDefault(t => t.TherapistId == id);
            _therapists.Remove(th);
        }

        public Therapist GetByEmail(string email)
        {
            return _therapists.FirstOrDefault(t => t.Email == email);
        }

        public Therapist GetById(int id)
        {
            return _therapists.FirstOrDefault(t => t.TherapistId == id);
        }

        public IEnumerable<Therapist> GetTherapists()
        {
            return _therapists.ToList();
        }

        /// <summary>
        /// Check if the given therapists exist
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool TherapistsExist(IList<int> ids)
        {
            List<int> therapists = _dbContext.Therapist.Select(c => c.TherapistId).ToList();
            foreach (int id in ids)
            {
                if (!therapists.Contains(id)) return false;
            }
            return true;
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateTherapist(Therapist therapist)
        {
            _therapists.Update(therapist);
        }
    }
}
