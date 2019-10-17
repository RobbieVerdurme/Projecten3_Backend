        using Microsoft.AspNetCore.Identity;
using MultimedAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultimedAPI.Data
{
    public class MultimedDataInitializer
    {

        private readonly MultimedDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public MultimedDataInitializer(MultimedDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if(_dbContext.Database.EnsureCreated())
            {
                await CreateUser("ShawnVanRanst", "shawnvanranst@gmail.com", "P@ssword123");
                User user = new User("Shawn", "Van Ranst", "shawnvanranst@gmail.com");
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();
            }
        }

        private async Task CreateUser(string username, string email, string password)
        {
            var user = new IdentityUser { UserName = username, Email = email };
            await _userManager.CreateAsync(user, password);
        }
    }
}
