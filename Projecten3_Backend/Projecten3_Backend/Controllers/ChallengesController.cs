using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projecten3_Backend.Data.IRepository;
using Projecten3_Backend.DTO;
using Projecten3_Backend.Model;
using Projecten3_Backend.Models;

namespace Projecten3_Backend.Controllers
{
    [ApiController]
    public class ChallengesController : ControllerBase
    {
        private readonly IChallengeRepository _repo;
        private readonly IUserRepository _userRepo;

        public ChallengesController(IChallengeRepository repo, IUserRepository userRepo)
        {
            _repo = repo;
            _userRepo = userRepo;
        }

        /// <summary>
        /// Add a set of challenges to a user.
        /// </summary>
        /// <param name="payload">The JSON body that was sent.</param>
        /// <returns>
        /// HTTP 400 if the payload is malformed.
        /// HTTP 500 if saving failed.
        /// HTTP 200 if successful.
        /// </returns>
        [Route("api/challenge/user/add")]
        [HttpPost]
        public IActionResult AddChallengesToUser(ChallengesUserDTO payload)
        {
            if(payload == null || payload.ChallengeIds == null) return BadRequest();
            User user = _userRepo.GetById(payload.UserId);
            List<int> challenges = new List<int>(payload.ChallengeIds);
            if (user == null || !_repo.ChallengesExist(challenges)) return BadRequest();
            try {
                _repo.AddChallengesToUser(payload.UserId, challenges);
            } catch (Exception)
            {
                return StatusCode(500);
            }
            return Ok();
        }

        /// <summary>
        /// Add a challenge.
        /// </summary>
        /// <param name="challenge">The challenge to add</param>
        /// <returns></returns>
        public IActionResult AddChallenge(Challenge challenge) {
            if (challenge == null || challenge.Title == null || challenge.Description == null || challenge.Category == null) return BadRequest();
            //Already exists -> return a 303 See Other StatusCode
            if (_repo.ChallengeExists(challenge)) return StatusCode(303);
            try {
                _repo.AddChallenge(challenge);
                return Ok();
            }
            catch (Exception) {
                return StatusCode(500);
            }
        }
        //Add

        //Edit

        //Get user challenges(android)

        //Get all

        //Delete

        //Add challenges to user

        //Complete challenge(android)
    }
}
