using Microsoft.EntityFrameworkCore;
using Projecten3_Backend.Data.IRepository;
using Projecten3_Backend.DTO;
using Projecten3_Backend.Model;
using Projecten3_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public UserDTO GetByEmail(string email)
        {
            User usr = _users.FirstOrDefault(u => u.Email == email);
            return User.MapUserToUserDTO(usr);
        }

        public UserDTO GetById(int id)
        {
            User usr = _users.FirstOrDefault(u => u.UserId == id);
            return User.MapUserToUserDTO(usr);
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            return _users.Select(u => User.MapUserToUserDTO(u)).ToList();
        }

        public void UpdateUser(UserDTO user)
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
        #endregion
    }
}
