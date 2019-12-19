using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projecten3_Backend.Data;
using Projecten3_Backend.Data.IRepository;
using Projecten3_Backend.DTO;
using Projecten3_Backend.Model;
using Projecten3_Backend.Model.ManyToMany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.Controllers
{    
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region prop
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserRepository _userRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly ICompanyRepository _companyRepo;
        private readonly ITherapistRepository _therapistRepo;
        private readonly IChallengeRepository _challengeRepo;
        #endregion

        #region ctor
        public UsersController(IUserRepository userRepository, UserManager<IdentityUser> userManager, ICategoryRepository categoryRepository, ICompanyRepository companyRepo, ITherapistRepository therapistRepo, IChallengeRepository challengeRepo)
        {
            _userRepo = userRepository;
            _categoryRepo = categoryRepository;
            _companyRepo = companyRepo;
            _therapistRepo = therapistRepo;
            _challengeRepo = challengeRepo;
            _userManager = userManager;
        }
        #endregion

        [Route("api/users")]
        [HttpGet]
        public IActionResult GetUser()
        {
            return Ok(_userRepo.GetUsers().Select((u) => Model.User.MapUserToUserDTO(u, _userRepo.GetUserCategories(u.UserId).ToList())));
        }

        /// <summary>
        /// Get a specific user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// HTTP 404 if not found.
        /// HTTP 200 otherwise.
        /// </returns>
        [Route("api/users/details/{id:int}")]
        [HttpGet]
        public IActionResult GetUserWithChallenges(int id)
        {
            User user = _userRepo.GetById(id);

            if (user == null)
            {
                return NotFound();
            }
            List<ChallengesOfUserDTO> challenges = _challengeRepo.GetUserChallenges(id).Select(c => ChallengeUser.MapToChallengesOfUserDTO(c)).ToList();

            return Ok(Model.User.MapUserToUserWithChallengesDTO(user, challenges, _userRepo.GetUserCategories(user.UserId).ToList()));
        }

        /// <summary>
        /// Get a specific user with it's challenges
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// HTTP 404 if not found.
        /// HTTP 200 otherwise.
        /// </returns>
        [Route("api/users/{id:int}")]
        [HttpGet]
        public IActionResult GetUser(int id)
        {
            User user = _userRepo.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(Model.User.MapUserToUserDTO(user, _userRepo.GetUserCategories(id).ToList()));
        }

        /// <summary>
        /// Edit a user's personal details
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        /// HTTP 400 if the payload is malformed.
        /// HTTP 500 if saving failed.
        /// HTTP 303 if a user with the updated values already exists.
        /// HTTP 200 if successful.
        /// </returns>
        [Route("api/users/edit")]
        [HttpPut]
        [Authorize(Roles = UserRole.MULTIMED_AND_USER)]
        public async Task<ActionResult> PutUser(EditUserDTO user)
        {
            if (user == null || string.IsNullOrEmpty(user.FamilyName) || string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.Phone) || string.IsNullOrEmpty(user.Email) || user.Categories == null)
            {
                return BadRequest();
            }
            if (!_categoryRepo.CategoriesExist(user.Categories)) return BadRequest();
            List<Category> categories = _categoryRepo.GetCategoriesById(user.Categories).ToList();

            User u = _userRepo.GetById(user.UserId);
            if (u == null) return BadRequest();


            IdentityUser identityUser = await _userManager.FindByNameAsync(u.Email);
            if (identityUser == null) return BadRequest();
            identityUser.UserName = user.Email;
            identityUser.Email = user.Email;

            u.FirstName = user.FirstName;
            u.FamilyName = user.FamilyName;
            u.Email = user.Email;
            u.Phone = user.Phone;
            u.Contract = user.Contract;
            u.Categories = categories.Select(c => Model.User.MapCategoryToCategoryUser(c, u)).ToList();

            if (_userRepo.UserExists(u)) return StatusCode(303);
            try
            {
                await _userManager.UpdateAsync(identityUser);
                _userRepo.UpdateUser(u);
                _userRepo.SaveChanges();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            return Ok();
        }

        /// <summary>
        /// Add a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>
        /// HTTP 400 if the payload is malformed.
        /// HTTP 303 if such a user already exists.
        /// HTTP 500 if saving failed.
        /// HTTP 200 if successful.
        /// </returns>
        [Route("api/users/add")]
        [HttpPost]
        [Authorize(Policy = UserRole.MULTIMED,Roles = UserRole.MULTIMED)]
        public async Task<IActionResult> PostUser(AddUserDTO user)
        {
            if (user == null || string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.FamilyName) || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Phone)) return BadRequest();

            Company comp = _companyRepo.GetById(user.Company);
            if (comp == null) return BadRequest();
            if (!_categoryRepo.CategoriesExist(user.Categories)) return BadRequest();
            if (!_therapistRepo.TherapistsExist(user.Therapists)) return BadRequest();
            List<Category> categories = _categoryRepo.GetCategoriesById(user.Categories).ToList();

            User u = new User {
                FirstName = user.FirstName,
                FamilyName = user.FamilyName,
                Phone = user.Phone,
                Email = user.Email,
                Company = comp,
                Contract = comp.Contract
            };
            if (_userRepo.UserExists(u)) return StatusCode(303);

            try
            {
                List<CategoryUser> catUsers = _categoryRepo.GetCategoriesById(user.Categories).Select(c => new CategoryUser { Category = c, User = u }).ToList();
                u.AddCategories(catUsers);
                List<TherapistUser> tUser = _therapistRepo.GetTherapistsById(user.Therapists).Select(t => new TherapistUser { Therapist = t, User = u }).ToList();
                u.AddTherapists(tUser);
                IdentityUser userLogin = new IdentityUser { UserName = user.Email, Email = user.Email };
                var result = await _userManager.CreateAsync(userLogin, "Multimed@" + user.FirstName + user.FamilyName + user.Phone);
                await _userManager.AddToRoleAsync(userLogin, "User");
                if (result.Succeeded)
                {
                    //return ok so the user knows the account has been created
                    _userRepo.AddUser(u);
                    _userRepo.SaveChanges();
                    _categoryRepo.SaveChanges();
                    return Ok();
                }


                User usr = _userRepo.GetByEmail(u.Email);
                usr.AddCategories(categories.Select(c => Model.User.MapCategoryToCategoryUser(c, usr)).ToList());
                _userRepo.SaveChanges();
            }
            catch (Exception e) {
                return StatusCode(500);
            }
            return StatusCode(303);
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// HTTP 404 if the user was not found.
        /// HTTP 500 if deleting failed.
        /// HTTP 200 if deleted.
        /// </returns>
        [Route("api/users/delete/{id:int}")]
        [HttpDelete]
        [Authorize(Policy = UserRole.MULTIMED, Roles = UserRole.MULTIMED)]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var user = _userRepo.GetById(id);
            var identityUser = await _userManager.FindByNameAsync(user.Email);
            if (user == null || identityUser == null)
            {
                return NotFound();
            }
            try
            {
                await _userManager.DeleteAsync(identityUser);
                _userRepo.DeleteUser(id);
                _userRepo.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500);
            }

            return Ok();
        }

        /// <summary>
        /// get the therapist list from user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// a list of therapists
        /// </returns>
        [Route("api/users/therapist/{id}")]
        [HttpGet]
        public IActionResult GetTherapistUser(int id)
        {
            var user = _userRepo.GetById(id);
            if(user == null)
            {
                return BadRequest();
            }
            try
            {
                return Ok(_userRepo.GetUserTherapists(id).Select(t => Therapist.MapTherapistToTherapistDTO(t)));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
        
                /// leaderboard
        /// <summary>
        /// Get leaderboard
        /// </summary>
        /// <param CompanyId="id"></param>
        /// <returns>
        /// HTTP 404 if the company was not found.
        /// HTTP 500.
        /// HTTP 200.
        /// </returns>
        [Route("api/users/leaderboard/{id:int}")]
        [HttpGet]
        //[Authorize(Policy = UserRole.MULTIMED, Roles = UserRole.MULTIMED)]
        public IActionResult GetLeaderboard(int id)
        {
            var user = _userRepo.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            
             if (user.Company == null)
            {
                return NotFound();
            }

            return Ok(_userRepo.GetUsersOfCompany(user.Company.CompanyId).Select((u) => Model.User.MapUserToLeaderboardDTO(u)));
         }
            
            

        /// <summary>
        /// get the therapist list from user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// a list of therapists
        /// </returns>
        [Route("api/users/Category/{id}")]
        [HttpGet]
        public IActionResult GetCategoryUser(int id)
        {
            var user = _userRepo.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            
            try
            {
                IEnumerable<Category> th = _userRepo.GetUserCategories(id);
                return Ok(th);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
