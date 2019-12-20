using Microsoft.AspNetCore.Identity;
using Projecten3_Backend.Model;
using Projecten3_Backend.Model.ManyToMany;
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

               
                //Multimeduser
                await CreateUser("SofieV","SofieV@gmail.com", "P@ssword123", UserRole.MULTIMED);
                
                #region Save changes
                _dbContext.SaveChanges();
                #endregion
            }
        }

        private async Task CreateUser(string username,string email, string password, string role)
        {
            var user = new IdentityUser { UserName = username, Email= email};
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
