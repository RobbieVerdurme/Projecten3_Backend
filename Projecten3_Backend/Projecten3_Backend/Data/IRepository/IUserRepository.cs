using Projecten3_Backend.DTO;
using Projecten3_Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.Data.IRepository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();

        User GetById(int id);

        User GetByEmail(string email);

        void AddUser(User user);

        void DeleteUser(int id);

        void UpdateUser(User user);
        void RaiseLeaderboardScore(int id);

        void SaveChanges();
        bool UserExists(User u);

        bool UsersExist(IList<int> ids);
    }
}
