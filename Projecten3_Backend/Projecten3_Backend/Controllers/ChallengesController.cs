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
        [Route("api/challenge/user/add")]
        [HttpPost]
        public IActionResult AddChallengesToUser(ChallengesUserDTO payload)
        {
            
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
