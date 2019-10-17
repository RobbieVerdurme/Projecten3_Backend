using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultimedAPI.Models.IRepositories
{
    public interface IChallengeRepository
    {

        Challenge GetById(int id);

        IEnumerable<Challenge> GetAll();

        IEnumerable<Challenge> GetAllChallengesForCategory(Category category);

        void AddChallenge(Challenge challenge);

        void DeleteChallenge(Challenge challenge);

        void updateChallenge(Challenge challenge);

        void SaveChanges();
    }
}
