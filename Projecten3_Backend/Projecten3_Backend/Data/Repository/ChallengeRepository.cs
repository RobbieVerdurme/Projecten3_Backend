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
    public class ChallengeRepository : IChallengeRepository
    {
        #region prop
        private readonly Projecten3_BackendContext _dbContext;
        private readonly DbSet<Challenge> _challenges;
        private readonly DbSet<User> _users;
        #endregion

        #region ctor
        public ChallengeRepository(Projecten3_BackendContext context)
        {
            _dbContext = context;
            _challenges = _dbContext.Challenges;
            _users = _dbContext.User;
        }
        #endregion

        public void AddChallenge(Challenge challenge)
        {
            _challenges.Add(challenge);
        }

        public void AddChallengesToUser(int userid, List<int> challengeids)
        {
            User usr =_users.FirstOrDefault(u => u.UserId == userid);
            List<Challenge> challenges =_challenges.Where(c => challengeids.Contains(c.ChallengeId)).ToList();

            usr.addChallenges(challenges.Select(c =>
                new ChallengeUser() {
                    ChallengeId = c.ChallengeId,
                    Challenge = c,
                    User = usr,
                    UserId = usr.UserId
                }
            ).ToList());
            _dbContext.SaveChanges();
        }

        public void CompleteChallenge(int userid, int challengeid)
        {
            User usr = _users.FirstOrDefault(u => u.UserId == userid);
            ChallengeUser challenge = usr.Challenges.FirstOrDefault(c => c.ChallengeId == challengeid);
            challenge.CompletedDate = DateTime.Now;
        }

        public void DeleteChallenge(int id)
        {
            Challenge challenge = _challenges.FirstOrDefault(c => c.ChallengeId == id);
            _challenges.Remove(challenge);
        }

        public Challenge GetById(int id)
        {
            return _challenges.FirstOrDefault(c => c.ChallengeId == id);
        }

        public IEnumerable<Challenge> GetChallenges()
        {
            return _challenges.ToList();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateChallenge(Challenge challenge)
        {
            _challenges.Update(challenge);
        }
    }
}
