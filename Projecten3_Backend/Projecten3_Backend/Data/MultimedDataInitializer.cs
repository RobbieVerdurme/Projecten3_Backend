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
                User usr = new User() { FamilyName = "test", Email = "test@teest" };
                _dbContext.User.Add(usr);

                Company c = new Company { Name = "textC", CompanyMembers = new List<User> {usr } };
                _dbContext.Company.Add(c);
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
