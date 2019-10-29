using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projecten3_Backend.Data.IRepository;
using Projecten3_Backend.DTO;
using Projecten3_Backend.Model;
using Projecten3_Backend.Models;
using System.Collections.Generic;
using System.Linq;

namespace Projecten3_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region prop
        private readonly IUserRepository _userRepo;
        #endregion

        #region ctor
        public UsersController(IUserRepository userRepository)
        {
            _userRepo = userRepository;
        }
        #endregion

        // GET: api/Users
        [HttpGet]
        public IEnumerable<UserDTO> GetUser()
        {
            return _userRepo.GetUsers();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _userRepo.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public IActionResult PutUser(int id, UserDTO user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            try
            {
                _userRepo.UpdateUser(user);
                _userRepo.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500);
                }
            }

            return Ok();
        }

        // POST: api/Users
        [HttpPost]
        public IActionResult PostUser(User user)
        {
            //create login for the user of the app

            //add user in db
            _userRepo.AddUser(user);
            _userRepo.SaveChanges();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
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

        private bool UserExists(int id)
        {
            return _userRepo.GetById(id) != null;
        }
    }
}
