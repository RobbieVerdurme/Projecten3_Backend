using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultimedAPI.Models.IRepositories
{
    public interface ITherapistRepository
    {

        IEnumerable<Therapist> GetAll();

        Therapist GetById(int id);

        void AddTherapist(Therapist therapist);

        void UpdateTherapist(Therapist therapist);

        void DeleteTherapist(Therapist therapist);

        void SaveChanges();
    }
}
