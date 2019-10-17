using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultimedAPI.Models.IRepositories
{
    public interface IChallengeUserRepository
    {

        ChallengeUser GetById(int id);

        IEnumerable<Challenge> GetAllChallengesForUser(User user);

        IEnumerable<User> GetAllUsersOfChallenge(Challenge challenge);

        void AddChallengeUser(ChallengeUser challengeUser);

        void DeleteChallengeUser(ChallengeUser challengeUser);

        void SaveChanges();
    }
}
