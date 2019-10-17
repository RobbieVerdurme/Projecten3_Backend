﻿using Microsoft.EntityFrameworkCore;
using MultimedAPI.Models;
using MultimedAPI.Models.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultimedAPI.Data.Repositories
{
    public class ChallengeRepository : IChallengeRepository
    {

        private readonly MultimedDbContext _dbContext;
        private readonly DbSet<Challenge> _challenges;

        public ChallengeRepository(MultimedDbContext dbContext)
        {
            _dbContext = dbContext;
            _challenges = _dbContext.Challenges;
        }

        public Challenge GetChallenge(int id)
        {
            return _challenges.Include(c => c.ChallengeUsers).SingleOrDefault(c => c.ChallengeId == id);
        }

        public IEnumerable<Challenge> GetAllChallenges()
        {
            return _challenges.Include(c => c.ChallengeUsers).OrderBy(c => c.Title);
        }

        public IEnumerable<Challenge> GetAllChallengesForCategory(Category category)
        {
            return _challenges.Include(c => c.ChallengeUsers).Where(c => c.Category == category).OrderBy(c => c.Title);
        }

        public void AddChallenge(Challenge challenge)
        {
            _challenges.Add(challenge);
        }

        public void DeleteChallenge(Challenge challenge)
        {
            _challenges.Remove(challenge);
        }

        public void updateChallenge(Challenge challenge)
        {
            _challenges.Update(challenge);
        }
        
        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        
    }
}
