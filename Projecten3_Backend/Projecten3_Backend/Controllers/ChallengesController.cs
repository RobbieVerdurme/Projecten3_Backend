using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projecten3_Backend.Data;
using Projecten3_Backend.Data.IRepository;
using Projecten3_Backend.DTO;
using Projecten3_Backend.Model;
using Projecten3_Backend.Model.ManyToMany;
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
        [Authorize(Roles = UserRole.MULTIMED_AND_THERAPIST)]
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
        /// <returns>
        /// HTTP 400 if the payload is malformed.
        /// HTTP 303 if such a challenge already exists.
        /// HTTP 200 if added.
        /// HTTP 500 if saving failed.
        /// </returns>
        [Route("api/challenge/add")]
        [Authorize(Roles = UserRole.MULTIMED_AND_THERAPIST)]
        [HttpPost]
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

        /// <summary>
        /// Get a user's challenges
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        /// HTTP 400 if the user doesn't exist.
        /// HTTP 200 otherwise.
        /// </returns>
        [Route("api/challenge/user/{id:int}")]
        [HttpGet]
        public IActionResult GetUserChallenges(int userId) {
            User user = _userRepo.GetById(userId);

            if (user == null) return BadRequest();

            return Ok(_repo.GetUserChallenges(userId).Select(c => ChallengeUser.MapToUserChallengeDTO(c)).ToList());
        }

        [Route("api/challenge")]
        [HttpGet]
        public IActionResult GetChallenges() {
            return Ok(_repo.GetChallenges().ToList());
        }

        /// <summary>
        /// Complete a challenge.
        /// </summary>
        /// <param name="complete"></param>
        /// <returns>
        /// HTTP 400 if the user and or challenge is invalid.
        /// HTTP 304 if said challenge was already completed.
        /// HTTP 500 if saving failed.
        /// HTTP 200 if successful.
        /// </returns>
        [Authorize(Policy = UserRole.USER,Roles = UserRole.USER)]
        [Route("api/challenge/complete")]
        [HttpPost]
        public IActionResult CompleteChallenge(CompleteChallengeDTO complete) {
            if (complete == null || _userRepo.GetById(complete.UserID) == null) return BadRequest();

            ChallengeUser challenge = _repo.GetUserChallenge(complete.UserID,complete.ChallengeID);
            if (challenge == null) return BadRequest();

            if (challenge.CompletedDate != null) return StatusCode(304);

            try
            {
                _repo.CompleteChallenge(challenge);
                _repo.SaveChanges();
            }
            catch (Exception) {
                return StatusCode(500);
            }

            return Ok();
        }




        //Edit

        //Delete


    }
}
