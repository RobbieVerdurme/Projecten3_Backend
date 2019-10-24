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
        /// <param name="email">The email of the user</param>
        ///<returns>Array of challenges</returns>
        [HttpGet]
        public IEnumerable<Challenge> GetAllChallengesForUser(string email)
        {
            User user = _userRepository.GetByEmail(email);
            return _challengeUserRepository.GetAllChallengesForUser(user);
        }

        // Get: api/ChallengeUser/5
        ///<summary>
        /// Get all users for this challenge
        ///</summary>
        /// <param name="id">Id of the challenge</param>
        ///<returns>Array of users</returns>
        [HttpGet("{id}")]
        public IEnumerable<User> GetAllUsersOfChallenge(int id)
        {
            Challenge challenge = _challengeRepository.GetById(id);
            return _challengeUserRepository.GetAllUsersOfChallenge(challenge);
        }


        // POST: api/ChallengeUser
        /// <summary>
        /// Adds a new challengeUser
        /// </summary>
        /// <param name="challengeUser">The new challengeUser/param>
        [HttpPost]
        public ActionResult<User> AddChallengesForUser(ChallengesUserDTO challengeUserDTO)
        {
            User user = _userRepository.GetById(challengeUserDTO.UserId);
            foreach(int id in challengeUserDTO.ChallengeIds)
            {
                Challenge challenge = _challengeRepository.GetById(id);
                user.AddChallengeUser(challenge);
            }
            _userRepository.UpdateUser(user);
            _userRepository.SaveChanges();
            return NoContent();
            //METHODE WERKT MAAR RESPONS DOET NOG IETS VREEMDS
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