using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultimedAPI.Models.IRepositories
{
    public interface IChallengeRepository
    {

        Challenge GetChallenge(int id);

        IEnumerable<Challenge> GetAllChallenges();

        IEnumerable<Challenge> GetAllChallengesForCategory(Category category);

        void AddChallenge(Challenge challenge);

        void DeleteChallenge(Challenge challenge);

        void updateChallenge(Challenge challenge);

        void SaveChanges();
    }
}
