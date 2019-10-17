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
                Challenge challenge = new Challenge("Rustdag", "Je hebt deze week al hard gewerkt. Je lichaam heeft natuurlijk ook rust nodig, dus doe vandaag eens lekker niets!", category1);
                _dbContext.Challenges.Add(challenge);
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
