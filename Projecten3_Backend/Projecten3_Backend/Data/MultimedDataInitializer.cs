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

                //categories
                Category c = new Category() { Name = "Ondergewicht" };
                _dbContext.Add(c);

                //challenges
                Challenge ch = new Challenge() { ChallengeImage = "", Description = "Loop 2 km", Title = "Lopen", Category = c, Level = 1 };

                //TherapistType
                TherapistType thType = new TherapistType() { Type = "Diëtist", Categories = new List<Category> { c } };
                _dbContext.Add(thType);

                //OpeningTimes
                List<OpeningTimes> otList = new List<OpeningTimes>()
                {
                    new OpeningTimes(){Interval = "9-17"},
                    new OpeningTimes(){Interval = "9-17"},
                    new OpeningTimes(){Interval = "9-17"},
                    new OpeningTimes(){Interval = "9-17"},
                    new OpeningTimes(){Interval = "9-17"},
                    new OpeningTimes(){Interval = "9-17"},
                    new OpeningTimes(){Interval = "9-17"},
                };
                //Therapist
                Therapist th = new Therapist() {FirstName = "Therapist",LastName = "De Peape", HouseNumber = 1, PhoneNumber = "0474139526", PostalCode = 9000, Street = "test", Website = "www.google.com", City = "Gent", Email = "TherapistDePeape@multimed.com", TherapistType = thType, OpeningTimes = otList };
                await CreateUser("TestTh",th.Email, "P@ssword123", UserRole.THERAPIST);
                _dbContext.Add(th);

                //company
                Company cmp = new Company() { Name = "Multimed", Street = "Multimedstraat", City = "Gent", Contract = DateTime.Now.AddYears(30), Country = "Belgie", Mail = "Multimed@gmail.com", Phone = "04785889764", PostalCode = 9000, HouseNumber = 1, Site = "multimed.be"};
                _dbContext.Add(cmp);

                //user
                User usr = new User() { FirstName = "Boefer",FamilyName = "rob",Phone = "0478995888",ExperiencePoints = 16, Company = cmp, Email = "Boeferrob@live.be", Contract = cmp.Contract};
                usr.AddTherapist(th);
                //many to many from user
                List<CategoryUser> cusr = new List<CategoryUser> { 
                    new CategoryUser() { Category = c, User = usr, UserId = usr.UserId, CategoryId = c.CategoryId }
                };

                ChallengeUser chUsr = new ChallengeUser() { ChallengeUserId = usr.UserId, User = usr, ChallengeId = ch.ChallengeId, Challenge = ch };

                usr.AddChallenges(new List<ChallengeUser> {
                    chUsr
                });

                usr.AddCategories(cusr);

                

                //account user
                await CreateUser("Boeferrob", usr.Email, "P@ssword123", UserRole.USER);
                _dbContext.Add(usr);
                _dbContext.ChallengeUser.Add(chUsr);


                th.AddClient(usr);
                


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
