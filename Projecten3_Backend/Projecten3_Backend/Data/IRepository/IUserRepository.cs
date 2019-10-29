﻿using Projecten3_Backend.DTO;
using Projecten3_Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.Data.IRepository
{
    public interface IUserRepository
    {
        IEnumerable<UserDTO> GetUsers();

        UserDTO GetById(int id);

        UserDTO GetByEmail(string email);

        void AddUser(User user);

        void DeleteUser(int id);

        void UpdateUser(UserDTO user);

        void SaveChanges();
    }
}
