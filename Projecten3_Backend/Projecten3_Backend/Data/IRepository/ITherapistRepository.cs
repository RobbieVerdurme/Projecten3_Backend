using Projecten3_Backend.Model;
using Projecten3_Backend.Model.ManyToMany;
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

        bool TherapistExists(Therapist therapist);

        bool TherapistTypeExists(int id);

        bool TherapistTypeExists(TherapistType therapistType);

        bool TherapistTypeExists(string type, IList<int> categories);

        IEnumerable<TherapistType> GetTherapistTypes();

        TherapistType GetTherapistType(int id);

        bool HasInvalidOpeningTimes(IList<string> times);

        Therapist GetById(int id);

        Therapist GetByEmail(string email);

        IEnumerable<OpeningTimes> GetOpeningTimesForTherapist(int id);

        void AddTherapist(Therapist therapist);

        void AddTherapistType(TherapistType therapistType);

        void DeleteTherapist(int id);

        void UpdateTherapist(Therapist therapist);

        void EditTherapistType(TherapistType therapistType);

        IEnumerable<Therapist> GetTherapistsById(IList<int> ids);

        void AddTherapistsUsers(List<TherapistUser> therapistUsers);

        void EditOpeningsTimes(List<OpeningTimes> ot);

        void SaveChanges();
    }
}
