using Microsoft.EntityFrameworkCore;
using MultimedAPI.Models;
using MultimedAPI.Models.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultimedAPI.Data.Repositories
{
    public class ChallengeUserRepository : IChallengeUserRepository
    {

        private readonly MultimedDbContext _dbContext;
        private readonly DbSet<ChallengeUser> _challengeUsers;

        public ChallengeUserRepository(MultimedDbContext dbContext)
        {
            _dbContext = dbContext;
            _challengeUsers = _dbContext.ChallengeUsers;
        }

        public ChallengeUser GetById(int id)
        {
            return _challengeUsers.SingleOrDefault(c => c.ChallengeUserId == id);
        }

        public IEnumerable<Challenge> GetAllChallengesForUser(User user)
        {
            return _challengeUsers.Where(c => c.UserId == user.UserId).Select(c => c.Challenge);
        }

        public IEnumerable<User> GetAllUsersOfChallenge(Challenge challenge)
        {
            return _challengeUsers.Where(c => c.ChallengeId == challenge.ChallengeId).Select(c => c.User);
        }

        public void AddChallengeUser(ChallengeUser challengeUser)
        {
            _challengeUsers.Add(challengeUser);
        }

        public void DeleteChallengeUser(ChallengeUser challengeUser)
        {
            _challengeUsers.Remove(challengeUser);
        }        

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
