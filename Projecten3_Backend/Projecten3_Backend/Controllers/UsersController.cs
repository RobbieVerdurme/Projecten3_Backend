using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projecten3_Backend.Data;
using Projecten3_Backend.Data.IRepository;
using Projecten3_Backend.DTO;
using Projecten3_Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Projecten3_Backend.Controllers
{    
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region prop
        private readonly IUserRepository _userRepo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly ICompanyRepository _companyRepo;
        private readonly ITherapistRepository _therapistRepo;
        #endregion

        #region ctor
        public UsersController(IUserRepository userRepository, ICategoryRepository categoryRepository, ICompanyRepository companyRepo, ITherapistRepository therapistRepo)
        {
            _userRepo = userRepository;
            _categoryRepo = categoryRepository;
            _companyRepo = companyRepo;
            _therapistRepo = therapistRepo;
            
        }
        #endregion

        [Route("api/users")]
        [HttpGet]
        public IActionResult GetUser()
        {
            return Ok(_userRepo.GetUsers().Select((u) => Model.User.MapUserToUserDTO(u)));
        }

        /// <summary>
        /// Get a specific user
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
            var user = _userRepo.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(Model.User.MapUserToUserDTO(user));
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
        public IActionResult PutUser(EditUserDTO user)
        {
            if (user == null || string.IsNullOrEmpty(user.FamilyName) || string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.Phone) || string.IsNullOrEmpty(user.Email) || user.Categories == null)
            {
                return BadRequest();
            }

            IList<int> categoryIds = new List<int>(user.Categories);
            if (!_categoryRepo.CategoriesExist(categoryIds)) return BadRequest();

            User u = _userRepo.GetById(user.UserId);
            if (u == null) return BadRequest();
            IList<Category> categories = _categoryRepo.GetCategories().Where(c => categoryIds.Contains(c.CategoryId)).ToList();

            u.FirstName = user.FirstName;
            u.FamilyName = user.FamilyName;
            u.Email = user.Email;
            u.Phone = user.Phone;
            u.Categories = categories;
            u.Contract = user.Contract;
            

            if (_userRepo.UserExists(u)) return StatusCode(303);

            _userRepo.UpdateUser(u);
            try
            {
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
        public IActionResult PostUser(AddUserDTO user)
        {
            if (user == null || string.IsNullOrEmpty(user.FirstName) || string.IsNullOrEmpty(user.FamilyName) || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Phone)) return BadRequest();

            Company comp = _companyRepo.GetById(user.Company);
            if (comp == null) return BadRequest();
            if (!_categoryRepo.CategoriesExist(user.Categories) || !_therapistRepo.TherapistsExist(user.Therapists)) return BadRequest();

            User u = new User {
                FirstName = user.FirstName,
                FamilyName = user.FamilyName,
                Phone = user.Phone,
                Email = user.Email,
                Company = comp,
                Categories = new List<Category>(_categoryRepo.GetCategoriesById(user.Categories)),
                Contract = comp.Contract
            };
            if (_userRepo.UserExists(u)) return StatusCode(303);


            _userRepo.AddUser(u);
            try
            {
                _userRepo.SaveChanges();
            }
            catch (Exception) {
                return StatusCode(500);
            }

            return Ok();
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
        public IActionResult DeleteUser(int id)
        {
            var user = _userRepo.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            try
            {
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
                IEnumerable<Therapist> th = _userRepo.GetUserTherapists(id);
                return Ok(th.Select(t => Therapist.MapTherapistToTherapistDTO(t)));
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
