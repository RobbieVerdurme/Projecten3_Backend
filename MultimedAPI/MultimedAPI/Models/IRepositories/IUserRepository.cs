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

        void RemoveUser(User user);

        void SaveChanges();
    }
}
