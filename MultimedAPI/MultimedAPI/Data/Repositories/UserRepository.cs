using Microsoft.EntityFrameworkCore;
using MultimedAPI.Models;
using MultimedAPI.Models.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultimedAPI.Data.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly MultimedDbContext _dbContext;
        private readonly DbSet<User> _users;

        public UserRepository(MultimedDbContext dbContext)
        {
            _dbContext = dbContext;
            _users = _dbContext.Users;
        }

        public User GetById(int id)
        {
            return _users.SingleOrDefault(u => u.UserId == id);
        }

        public User GetByEmail(string email)
        {
            return _users.SingleOrDefault(u => u.Email == email);
        }

        public IEnumerable<User> GetUsers()
        {
            return _users.ToList();
        }

        public void AddUser(User user)
        {
            _users.Add(user);
        }
        

        public void DeleteUser(User user)
        {
            _users.Remove(user);
        }

        public void UpdateUser(User user)
        {
            _users.Update(user);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
