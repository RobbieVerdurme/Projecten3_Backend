using Projecten3_Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.Data.IRepository
{
    public interface ITherapistRepository
    {
        IEnumerable<Therapist> GetTherapists();

        bool TherapistsExist(IList<int> ids);

        Therapist GetById(int id);

        Therapist GetByEmail(string email);

        void AddTherapist(Therapist therapist);

        void DeleteTherapist(int id);

        void UpdateTherapist(Therapist therapist);

        void SaveChanges();
    }
}
