using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultimedAPI.DTOs;
using MultimedAPI.Models;
using MultimedAPI.Models.IRepositories;


namespace MultimedAPI.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private static Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0abcdefghijklmnopqrstuvw123456789";
        public UserController(IUserRepository userRepository, UserManager<IdentityUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        #region-- Http GET --

        
        [HttpGet()]
        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            User user = _userRepository.GetById(id);
            if(user == null)
            {
                return NotFound();
            }
            return user;
        }

        #endregion

        #region -- POST --

        [HttpPost()]
        public async Task<ActionResult<User>> AddUserAsync(UserDTO userDTO)
        {
            User userToCreate = new User() { FirstName = userDTO.FirstName, FamilyName = userDTO.FamilyName, Email = userDTO.Email };
            _userRepository.AddUser(userToCreate);
            var user = new IdentityUser { UserName = userDTO.Email, Email = userDTO.Email };
            string password =  new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());
            await _userManager.CreateAsync(user, password);
            _userRepository.SaveChanges();

            return CreatedAtAction(nameof(AddUserAsync), new { id = userToCreate.UserId }, userToCreate);
        }
        #endregion

        #region -- Delete --

        [HttpDelete("{id}")]
        public ActionResult<User> DeleteUser(int id)
        {
            User user = _userRepository.GetById(id);
            if(user == null)
            {
                return NotFound();
            }
            _userRepository.DeleteUser(user);
            _userRepository.SaveChanges();
            return user;
        }
        #endregion
    }
}