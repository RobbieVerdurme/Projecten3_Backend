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

                //categories
                Category c = new Category() { Name = "Ondergewicht" };
                _dbContext.Add(c);

                Challenge ch = new Challenge() { Category = c, Description = "Loop 2 km", ChallengeImage = "" , Title = "Lopen"};

                #region Users
                //Multimeduser
                await CreateUser("SofieV","SofieV@gmail.com", "P@ssword123", UserRole.MULTIMED);

                //TherapistType
                TherapistType thType = new TherapistType() { Type = "", Categories = new List<Category> { c } };
                _dbContext.Add(thType);
                //Therapist
                Therapist th = new Therapist() {FirstName = "Test",LastName = "Th", HouseNumber = 1, PhoneNumber = "", PostalCode = 9000, Street = "", Website = "", City = "Gent", Email = "TestTh@gmail.com", TherapistType = thType};
                await CreateUser("TestTh",th.Email, "P@ssword123", UserRole.THERAPIST);
                _dbContext.Add(th);

                //company
                Company cmp = new Company() { Name = "Multimed", Street = "Multimedstraat", City = "Gent", Contract = DateTime.Now, Country = "Belgie", Mail = "Multimed@gmail.com", Phone = "", PostalCode = 9000, HouseNumber = 1, Site = "multimed.be" };
                _dbContext.Add(cmp);

                //user
                User usr = new User() { FirstName = "Boeferrob", Company = cmp, Email = "Boeferrob@live.be" };
                usr.AddTherapist(th);
                ChallengeUser chUsr = new ChallengeUser() { ChallengeUserId = usr.UserId, User = usr, ChallengeId = ch.ChallengeId, Challenge = ch };
                usr.AddChallenges(new List<ChallengeUser> {
                    chUsr
                });
                await CreateUser("Boeferrob", usr.Email, "P@ssword123", UserRole.USER);
                _dbContext.Add(usr);
                _dbContext.ChallengeUser.Add(chUsr);
                #endregion



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
