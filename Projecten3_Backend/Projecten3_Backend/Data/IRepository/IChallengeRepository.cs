
using Projecten3_Backend.Model;
using Projecten3_Backend.Model.ManyToMany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.Data.IRepository
{
    public interface IChallengeRepository
    {
        IEnumerable<Challenge> GetChallenges();

        bool ChallengesExist(IList<int> ids);

        bool ChallengeExists(Challenge challenge);

        Challenge GetById(int id);

        void AddChallenge(Challenge challenge);

        void DeleteChallenge(int id);

        void UpdateChallenge(Challenge challenge);

        ChallengeUser GetUserChallenge(int userId,int challengeId);

        IEnumerable<ChallengeUser> GetUserChallenges(int userId);

        void CompleteChallenge(ChallengeUser challenge, DateTime date);

        void AddChallengesToUser(int userid, IList<int> challengeids);

        bool UserHasCompletedDailyChallengeOfCategory(int userId, int categoryId, int day, int month, int year);

        IEnumerable<Challenge> GetChallengesOfCategories(IList<int> categoryIds);

        IEnumerable<Challenge> GetChallengesOfCategoryAndLevel(int categoryId, int level);

        DateTime GetTimeStamp();

        void SaveChanges();
    }
}
