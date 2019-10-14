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
        public IEnumerable<UserDTO> GetUsers()
        {
            throw new NotImplementedException();
        }

        
        [HttpGet("{id}")]
        public ActionResult<UserDTO> GetUser(int id)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region -- POST --

        [HttpPost()]
        public ActionResult<User> CreateUser(User user)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region -- PUT --

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User user)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region -- Delete --

        [HttpDelete("{id}")]
        public ActionResult<User> DeleteUser(int id)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}