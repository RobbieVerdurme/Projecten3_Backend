
using Projecten3_Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.Data.IRepository
{
    public interface IChallengeRepository
    {
        IEnumerable<Challenge> GetChallenges();

        Challenge GetById(int id);

        void AddChallenge(Challenge challenge);

        void DeleteChallenge(int id);

        void UpdateChallenge(Challenge challenge);

        void CompleteChallenge(int userid, int challengeid);

        void AddChallengesToUser(int userid, List<int> challengeids);

        void SaveChanges();
    }
}
