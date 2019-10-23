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

        public MultimedDataInitializer(MultimedDbContext context, UserManager<IdentityUser> userManager)
        {
            _dbContext = context;
            _userManager = userManager;
        }

        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if(_dbContext.Database.EnsureCreated())
            {
                Category category1 = new Category("Overweight");
                _dbContext.Categories.Add(category1);

                Challenge challenge1 = new Challenge("Rustdag", "Je hebt deze week al hard gewerkt. Je lichaam heeft natuurlijk ook rust nodig, dus doe vandaag eens lekker niets!", category1);
                _dbContext.Challenges.Add(challenge1);

                User user1 = new User() { FirstName = "Arno", FamilyName = "Boel", Email = "banaan@hotmail.com"};

                _dbContext.Users.Add(user1);
                //_dbContext.ChallengeUsers.Add(new ChallengeUser(user1, challenge1));

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
