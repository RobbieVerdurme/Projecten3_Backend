﻿using Microsoft.EntityFrameworkCore;
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

        public void AddChallengesToUser(int userid, IList<int> challengeids)
        {
            User usr =_users.FirstOrDefault(u => u.UserId == userid);

            //Feth the challenges that might be added
            List<Challenge> challenges =_challenges.Where(c => challengeids.Contains(c.ChallengeId)).ToList();

            //Fetch the existing challenges for the user
            List<int> existing = _dbContext.ChallengeUser.Where(cu => cu.UserId == userid).Select( cu => cu.ChallengeId).ToList();

            //Add the not already existing challenges
            List<ChallengeUser> challengesToAdd = challenges.Where(c => !existing.Contains(c.ChallengeId)).Select(c =>
                new ChallengeUser() {
                    ChallengeId = c.ChallengeId,
                    Challenge = c,
                    User = usr,
                    UserId = usr.UserId
                }
            ).ToList();

            usr.AddChallenges(challengesToAdd);

            _dbContext.ChallengeUser.AddRange(challengesToAdd);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Check if a given challenge already exists
        /// </summary>
        /// <param name="challenge"></param>
        /// <returns></returns>
        public bool ChallengeExists(Challenge challenge)
        {
            return _dbContext.Challenges.Where(c => c.Title == challenge.Title && c.Description == challenge.Description && c.Category.CategoryId == challenge.Category.CategoryId).FirstOrDefault() != null;
        }

        /// <summary>
        /// Check if the given challenge id's actually map to valid challenges
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool ChallengesExist(IList<int> ids)
        {
            List<int> challenges = _dbContext.Challenges.Select(c => c.ChallengeId).ToList();
            foreach (int id in ids) {
                if (!challenges.Contains(id)) return false;
            }
            return true;
        }

        public void CompleteChallenge(ChallengeUser challenge, DateTime date)
        {
            challenge.CompletedDate = date;
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
            return _challenges.Include(c => c.Category).ToList();
        }

        /// <summary>
        /// Check if a user with the given <paramref name="userId"/> has completed a challenge on the given <paramref name="day"/>,<paramref name="month"/> and <paramref name="year"/>.
        /// If there is such a challenge and it also matches the category of <paramref name="categoryId"/>, then the user already did his challenge of the day for that category.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="categoryId"></param>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public bool UserHasCompletedDailyChallengeOfCategory(int userId, int categoryId, int day, int month, int year)
        {
            //Fetch the challenges
            return _dbContext.ChallengeUser.Include(c => c.Challenge).ThenInclude(cha => cha.Category)
                //of a user and have a given category
                .Where(c => c.UserId == userId && c.Challenge.Category.CategoryId == categoryId
                //and the value is present and matches the arguments
                && c.CompletedDate.HasValue
                && c.CompletedDate.Value.Year == year
                && c.CompletedDate.Value.Month == month
                && c.CompletedDate.Value.Day == day).ToList().Count != 0;
        }

        public ChallengeUser GetUserChallenge(int userId, int challengeId)
        {
            return _dbContext.ChallengeUser.Where(c => c.ChallengeId == challengeId && c.UserId == userId).Include(c => c.Challenge).ThenInclude(cha => cha.Category).FirstOrDefault();
        }

        public IEnumerable<ChallengeUser> GetUserChallenges(int userId)
        {
            return _dbContext.ChallengeUser.Where(c => c.UserId == userId).Include( c => c.Challenge).ThenInclude(cha => cha.Category).Include(c => c.User).ToList();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateChallenge(Challenge challenge)
        {
            _challenges.Update(challenge);
        }

        public IEnumerable<Challenge> GetChallengesOfCategories(IList<int> categoryIds)
        {
            return _challenges.Include(c => c.Category).Where(challenge => categoryIds.Contains(challenge.Category.CategoryId));
        }


        public IEnumerable<Challenge> GetChallengesOfCategoryAndLevel(int categoryId, int level)
        {
            return _challenges.Include(c => c.Category).Where(c => c.Category.CategoryId == categoryId && c.Level == level).ToList();
        }


        public DateTime GetTimeStamp() {
            return DateTime.Now;
        }
    }
}