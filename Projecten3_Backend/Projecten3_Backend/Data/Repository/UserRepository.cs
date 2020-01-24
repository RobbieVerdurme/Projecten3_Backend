using Microsoft.EntityFrameworkCore;
using Projecten3_Backend.Data.IRepository;
using Projecten3_Backend.DTO;
using Projecten3_Backend.Model;
using Projecten3_Backend.Models;
using System.Collections.Generic;
using System.Linq;

namespace Projecten3_Backend.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        #region  prop
        private readonly Projecten3_BackendContext _dbContext;
        private readonly DbSet<User> _users;
        #endregion

        #region ctor
        public UserRepository(Projecten3_BackendContext dbContex)
        {
            _dbContext = dbContex;
            _users = _dbContext.User;
        }
        #endregion

        #region methods
        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public void DeleteUser(int id)
        {
            User usr = _users.FirstOrDefault(u => u.UserId == id);
            _users.Remove(usr);
        }

        public User GetByEmail(string email)
        {
            return _users.Include(u => u.Categories).Include(u => u.Company).FirstOrDefault(u => u.Email == email);
        }

        public User GetById(int id)
        {
            return _users.Include(u => u.Categories).Include(u => u.Company).FirstOrDefault(u => u.UserId == id);
        }

        public void RaiseLeaderboardScore(int id)
        {
            _users.FirstOrDefault(u => u.UserId == id).RaiseLeaderboardScore();
        }

        public IEnumerable<User> GetUsers()
        {
            return _users.Include(u => u.Categories).ToList();
        }

        public void UpdateUser(User user)
        {
            User usr = _users.FirstOrDefault(u => u.UserId == user.UserId);

            usr.FirstName = user.FirstName;
            usr.FamilyName = user.FamilyName;
            usr.Phone = user.Phone;
            usr.Email = user.Email;
            usr.Categories = user.Categories;
            usr.Contract = user.Contract;

            _users.Update(usr);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public bool UserExists(User u)
        {
            return _dbContext.User.Where((user) => u.Email == user.Email).FirstOrDefault() != null;
        }

        public bool UsersExist(IList<int> ids)
        {
            List<int> existingClients = _dbContext.User.Select(u => u.UserId).ToList();
            foreach (int id in ids)
            {
                if (!existingClients.Contains(id)) return false;
            }
            return true;
        }

        public void AddExp(User user)
        {
            User usr = _users.FirstOrDefault(u => u.UserId == user.UserId);
            usr.ExperiencePoints += 1;
            _users.Update(usr);
        }

        public IEnumerable<Therapist> GetUserTherapists(int id)
        {
            return _dbContext.TherapistUser.Where(c => c.UserId == id).Include(th => th.Therapist).ThenInclude(u => u.TherapistType).Include(u => u.User).Select(t => t.Therapist).ToList();
        }

        public IEnumerable<User> GetUsersOfCompany(int companyId)
        {
            return _users.Where(u => u.Company.CompanyId == companyId).Include( u => u.LeaderboardScores);
        }

        public IEnumerable<User> GetClientsOfTherapist(IList<int> clients)
        {
            return _users.Where(u => clients.Contains(u.UserId));
        }
        
        public IEnumerable<Category> GetUserCategories(int id)
        {
            return _dbContext.CategoryUser.Where(c => c.UserId == id).Include(c => c.Category).Select(c=> c.Category).ToList();
        }
        #endregion
    }
}
