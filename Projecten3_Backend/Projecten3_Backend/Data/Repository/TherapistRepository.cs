using Microsoft.EntityFrameworkCore;
using Projecten3_Backend.Data.IRepository;
using Projecten3_Backend.Model;
using Projecten3_Backend.Model.ManyToMany;
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
            if(th != null) _therapists.Remove(th);
        }

        public Therapist GetByEmail(string email)
        {
            return _therapists.Include(t => t.Clients).Include(t => t.OpeningTimes).FirstOrDefault(t => t.Email == email);
        }

        public Therapist GetById(int id)
        {
            return _therapists.Include(t => t.Clients).ThenInclude(cl => cl.User).Include(t => t.OpeningTimes).Include(t => t.TherapistType).ThenInclude(tt => tt.Categories).FirstOrDefault(t => t.TherapistId == id);
        }

        public IEnumerable<Therapist> GetTherapists()
        {
            return _therapists.Include(t => t.Clients).Include(t => t.OpeningTimes).Include(t => t.TherapistType).ToList();
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

        public bool TherapistTypeExists(int id)
        {
            return _dbContext.TherapistType.FirstOrDefault(type => type.TherapistTypeId == id) != null;
        }

        public IEnumerable<TherapistType> GetTherapistTypes()
        {
            return _dbContext.TherapistType.Include(t => t.Categories).ToList();
        }

        public bool HasInvalidOpeningTimes(IList<string> times)
        {
            return Therapist.HasInvalidOpeningTimes(times);
        }

        public bool TherapistExists(Therapist therapist)
        {
            return _dbContext.Therapist.Where(t => t.Email == therapist.Email).FirstOrDefault() != null;
        }

        public TherapistType GetTherapistType(int id)
        {
            return _dbContext.TherapistType.Where(t => t.TherapistTypeId == id).Include(t => t.Categories).FirstOrDefault();
        }

        public IEnumerable<OpeningTimes> GetOpeningTimesForTherapist(int id)
        {
            Therapist ther = _dbContext.Therapist.Where(t => t.TherapistId == id).Include(t => t.OpeningTimes).FirstOrDefault();
            if (ther == null) return null;
            return ther.OpeningTimes.ToList();
        }

        public bool TherapistTypeExists(TherapistType therapistType)
        {
            return _dbContext.TherapistType.Where(t => t.Type == therapistType.Type).FirstOrDefault() != null;
        }

        public void EditTherapistType(TherapistType therapistType)
        {
            _dbContext.TherapistType.Update(therapistType);
        }

        public bool TherapistTypeExists(string type, IList<int> categories)
        {
            TherapistType t = _dbContext.TherapistType.Where(therapistType => therapistType.Type == type).FirstOrDefault();
            if (t == null) return false;
            List<int> categoryIds = t.Categories.Select( c => c.CategoryId).ToList();
            foreach (int c in categories) {
                if (!categoryIds.Contains(c)) return false;
            }
            return true;
        }

        public void AddTherapistType(TherapistType therapistType)
        {
            _dbContext.TherapistType.Add(therapistType);
        }

        public IEnumerable<Therapist> GetTherapistsById(IList<int> ids)
        {
            return _dbContext.Therapist.Where(t => ids.Contains(t.TherapistId)).ToList();
        }

        public void AddTherapistsUsers(List<TherapistUser> therapistUsers)
        {
            _dbContext.TherapistUser.AddRange(therapistUsers);
        }

        public void EditOpeningsTimes(List<OpeningTimes> ot)
        {
            ot.ForEach(openingTime => _dbContext.OpeningTimes.Update(openingTime));
        }
    }
}
