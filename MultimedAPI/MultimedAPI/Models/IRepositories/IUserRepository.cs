using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultimedAPI.Models.IRepositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();

        User GetById(int id);

        User GetByEmail(string email);

        void AddUser(User user);

        void DeleteUser(User user);

        void UpdateUser(User user);

        void SaveChanges();
    }
}
