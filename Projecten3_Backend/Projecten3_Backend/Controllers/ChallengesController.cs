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
        private readonly ICategoryRepository _categoryRepository;

        public ChallengesController(IChallengeRepository repo, IUserRepository userRepo, ICategoryRepository categoryRepository)
        {
            _repo = repo;
            _userRepo = userRepo;
            _categoryRepository = categoryRepository;
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
                _repo.SaveChanges();
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
        public IActionResult AddChallenge(AddChallengeDTO dto) {
            if (dto == null || dto.Title == null || dto.Description == null) return BadRequest();
            Category category = _categoryRepository.GetById(dto.CategoryId);
            if (category == null) return BadRequest();
            Challenge challenge = new Challenge() {
                Title = dto.Title,
                Description = dto.Description,
                ChallengeImage = dto.ChallengeImage,
                Category = category
            };
            //Already exists -> return a 303 See Other StatusCode
            if (_repo.ChallengeExists(challenge)) return StatusCode(303);
            try {
                _repo.AddChallenge(challenge);
                _repo.SaveChanges();
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
        [Route("api/challenge/user/{id}")]
        [HttpGet]
        public IActionResult GetUserChallenges(int id) {
            User user = _userRepo.GetById(id);

            if (user == null) return BadRequest();

            return Ok(_repo.GetUserChallenges(id).Select(c => ChallengeUser.MapToUserChallengeDTO(c)).ToList());
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
            User usr = _userRepo.GetById(complete.UserID);
            if (complete == null || usr == null) return BadRequest();

            ChallengeUser challenge = _repo.GetUserChallenge(complete.UserID,complete.ChallengeID);
            if (challenge == null) return BadRequest();

            if (challenge.CompletedDate != null) return StatusCode(304);

            try
            {
                _userRepo.AddExp(usr);
                _userRepo.SaveChanges();
                _repo.CompleteChallenge(challenge);
                _repo.SaveChanges();
            }
            catch (Exception) {
                return StatusCode(500);
            }

            return Ok(challenge);
        }

        /// <summary>
        /// For a given user, get the challenges that have a category which in itself is contained in the user's categories.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("api/challenge/category/user/{id:int}")]
        [HttpGet]
        public IActionResult GetChallengesForUserCategories(int id) {
            User user = _userRepo.GetById(id);
            if (user == null) return BadRequest();
            IList<int> categories = user.Categories.Select(c => c.CategoryId).ToList();
            return Ok(_repo.GetChallenges().Where(challenge => categories.Contains(challenge.Category.CategoryId)).ToList());
        }



        //Edit

        //Delete


    }
}
