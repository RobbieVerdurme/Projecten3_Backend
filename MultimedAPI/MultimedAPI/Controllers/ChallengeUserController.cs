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
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class ChallengeUserController : ControllerBase
    {

        private readonly IChallengeUserRepository _challengeUserRepository;
        private readonly IChallengeRepository _challengeRepository;
        private readonly IUserRepository _userRepository;

        public ChallengeUserController(IChallengeUserRepository challengeUserRepository, IChallengeRepository challengeRepository, IUserRepository userRepository)
        {
            _challengeUserRepository = challengeUserRepository;
            _challengeRepository = challengeRepository;
            _userRepository = userRepository;
        }

        // Get: api/ChallengeUser
        ///<summary>
        /// Get all challenges for this user
        ///</summary>
        ///<returns>Array of challenges</returns>
        [HttpGet]
        public IEnumerable<Challenge> GetAllChallengesForUser(User user)
        {
            return _challengeUserRepository.GetAllChallengesForUser(user);
        }

        // Get: api/ChallengeUser
        ///<summary>
        /// Get all users for this challenge
        ///</summary>
        ///<returns>Array of users</returns>
        [HttpGet]
        public IEnumerable<User> GetAllUsersOfChallenge(Challenge challenge)
        {
            return _challengeUserRepository.GetAllUsersOfChallenge(challenge);
        }


        // POST: api/ChallengeUser
        /// <summary>
        /// Adds a new challengeUser
        /// </summary>
        /// <param name="challengeUser">The new challengeUser/param>
        [HttpPost]
        public ActionResult<Challenge> AddChallengeUser(ChallengeUserDTO challengeUser)
        {
            Challenge challenge = _challengeRepository.GetById(challengeUser.ChallengeId);
            User user = _userRepository.GetUserById(challengeUser.UserId);
            ChallengeUser challengeUserToCreate = new ChallengeUser(user, challenge);
            _challengeUserRepository.AddChallengeUser(challengeUserToCreate);
            _challengeUserRepository.SaveChanges();

            return CreatedAtAction(nameof(AddChallengeUser), new { id = challengeUserToCreate.ChallengeUserId }, challengeUserToCreate);
        }

        // Delete: api/ChallengeUser/5
        /// <summary>
        /// Deletes a challengeUser
        /// </summary>
        /// <param name="id">Id of the challengeUser to be deleted</param>
        [HttpDelete("{id}")]
        public ActionResult<ChallengeUser> DeleteChallengeUser(int id)
        {
            ChallengeUser challengeUser = _challengeUserRepository.GetById(id);
            if(challengeUser == null)
            {
                return NotFound();
            }
            _challengeUserRepository.DeleteChallengeUser(challengeUser);
            _challengeUserRepository.SaveChanges();
            return challengeUser;
        }
    }
}