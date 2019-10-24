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
                #region Users
                await CreateUser("ShawnVanRanst", "shawnvanranst@gmail.com", "P@ssword123");
                User user = new User("Shawn", "Van Ranst", "shawnvanranst@gmail.com");
                _dbContext.Users.Add(user);
                
                #endregion

                #region Category
                Category category1 = new Category("Overgewicht");
                _dbContext.Categories.Add(category1);
                #endregion

                #region Challenges
                Challenge challenge1 = new Challenge("Lopen", "Loop vandaag 5 km", category1);
                _dbContext.Challenges.Add(challenge1);
                #endregion


                #region Save changes
                _dbContext.SaveChanges();
                #endregion
            }
        }

        private async Task CreateUser(string username, string email, string password)
        {
            var user = new IdentityUser { UserName = username, Email = email };
            await _userManager.CreateAsync(user, password);
        }
    }
}
