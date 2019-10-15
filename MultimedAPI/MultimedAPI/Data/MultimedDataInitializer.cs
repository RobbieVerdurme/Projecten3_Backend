using Microsoft.AspNetCore.Identity;
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
