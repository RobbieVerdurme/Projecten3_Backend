using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
        public ActionResult<User> AddUser(UserDTO userDTO)
        {
            User userToCreate = new User() { FirstName = userDTO.FirstName, FamilyName = userDTO.FamilyName, Email = userDTO.Email };
            _userRepository.AddUser(userToCreate);
            _userRepository.SaveChanges();
            return CreatedAtAction(nameof(AddUser), new { id = userToCreate.UserId }, userToCreate);
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