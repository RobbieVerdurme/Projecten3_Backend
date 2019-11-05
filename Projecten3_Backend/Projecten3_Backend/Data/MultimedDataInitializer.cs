using Microsoft.AspNetCore.Identity;
using Projecten3_Backend.Model;
using Projecten3_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Projecten3_Backend.Data
{
    public class MultimedDataInitializer
    {
        
        private readonly Projecten3_BackendContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public MultimedDataInitializer(Projecten3_BackendContext dbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitializeData()
        {
            _dbContext.Database.EnsureDeleted();
            if (_dbContext.Database.EnsureCreated())
            {
                await CreateRoles();

                #region Users
                //Multimeduser
                await CreateUser("SofieV@gmail.com", "P@ssword123", UserRole.MULTIMED);

                //therapist
                Therapist th = new Therapist() { FirstName = "Therapist", LastName = "De pape", Email = "Therapist.DePape@gmail.com" };
                _dbContext.Therapist.Add(th);
                await CreateUser(th.Email, "P@ssword123", UserRole.THERAPIST);

                //User
                User user = new User() {FirstName = "Robbie", FamilyName = "Verdurme", Email="robbievrdrm@gmail.com" };
                _dbContext.User.Add(user);
                await CreateUser(user.Email, "P@ssword123", UserRole.USER);


                #endregion

                #region Category
                Category category1 = new Category() { Name = "Overgewicht"};
                #endregion

                #region Challenges
                Challenge challenge1 = new Challenge() {Title = "Lopen", Description = "Loop vandaag 5 km", Category = category1 };
                _dbContext.Challenges.Add(challenge1);
                #endregion


                #region Save changes
                _dbContext.SaveChanges();
                #endregion
            }
        }

        private async Task CreateUser(string username, string password, string role)
        {
            var user = new IdentityUser { UserName = username, Email= username};
            await _userManager.CreateAsync(user, password);
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, role));
            await _userManager.AddToRoleAsync(user, role);
        }

        private async Task CreateRoles()
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRole.USER));
            await _roleManager.CreateAsync(new IdentityRole(UserRole.MULTIMED));
            await _roleManager.CreateAsync(new IdentityRole(UserRole.THERAPIST));
        }
    }
}
