using Microsoft.EntityFrameworkCore;
using Projecten3_Backend.Data.IRepository;
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
            return _users.FirstOrDefault(u => u.Email == email);
        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(u => u.UserId == id);
        }

        public IEnumerable<User> GetUsers()
        {
            return _users.ToList();
        }

        public void UpdateUser(User user)
        {
            User usr = _users.FirstOrDefault(u => u.UserId == user.UserId);

            usr.FirstName = user.FirstName;
            usr.FamilyName = user.FamilyName;
            usr.Phone = user.Phone;
            usr.Email = user.Email;
            usr.Categories = user.Categories;

            _users.Update(usr);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public bool AlreadyExists(User u)
        {
            return _dbContext.User.Where(
                (user) => user.FirstName == u.FirstName && user.FamilyName == u.FamilyName && user.Email == u.Email && u.Phone == user.Phone && u.Company.CompanyId == user.Company.CompanyId)
                .FirstOrDefault() != null;
        }
        #endregion
    }
}
