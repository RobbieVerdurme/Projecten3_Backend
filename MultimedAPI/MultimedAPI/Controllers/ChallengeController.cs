using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    public class ChallengeController : ControllerBase
    {

        private readonly IChallengeRepository _challengeRepository;

        public ChallengeController(IChallengeRepository challengeRepository)
        {
            _challengeRepository = challengeRepository;
        }

        // GET: api/Challenge
        /// <summary>
        /// Get all challenges ordered by category
        /// </summary>
        /// <returns>array of recipes</returns>
        [HttpGet]
        public IEnumerable<Challenge> GetAllChallenges()
        {
            IEnumerable<Challenge> challenges = _challengeRepository.GetAll().OrderBy(r => r.Category);

            return challenges;
        }

        // GET: api/Challenge/5
        /// <summary>
        /// Get the challenge with given id
        /// </summary>
        /// <param name="id">the id of the challenge</param>
        /// <returns>The challenge</returns>
        [HttpGet("{id}")]
        public ActionResult<Challenge> GetChallenge(int id)
        {
            Challenge challenge = _challengeRepository.GetById(id);
            if (challenge == null) return NotFound();
            return challenge;
        }

        // POST: api/Challenge
        /// <summary>
        /// Adds a new challenge
        /// </summary>
        /// <param name="challenge">The new challenge</param>
        [HttpPost]
        public ActionResult<Challenge> AddChallenge(ChallengeDTO challenge)
        {
            Challenge challengeToCreate = new Challenge() { Title = challenge.Title, Description = challenge.Description, Category = challenge.Category };
            foreach(var u in challenge.ChallengeUsers)
            {
                challengeToCreate.AddUser(new User() { Email = u.User.Email, FirstName = u.User.FirstName, FamilyName = u.User.FamilyName });
            }
            _challengeRepository.AddChallenge(challengeToCreate);
            _challengeRepository.SaveChanges();

            return CreatedAtAction(nameof(AddChallenge), new { id = challengeToCreate.ChallengeId }, challengeToCreate);
        }


    }
}