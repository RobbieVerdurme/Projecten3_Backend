using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Projecten3_Backend.Data.IRepository;

namespace Projecten3_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserRepository _userRepo;
        private readonly ITherapistRepository _therapistRepo;
        private readonly IConfiguration _config;

        public AccountController(
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        IUserRepository userRepository,
        ITherapistRepository therapistRepository,
        IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _therapistRepo = therapistRepository;
            _userRepo = userRepository;
            _config = config;

        }

        /// <summary>
        /// Login
        /// </summary>
        /// <param name="model">the login details</param>
        [HttpPost]
        public async Task<ActionResult<String>> CreateToken(LoginDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                 if (result.Succeeded)
                {
                    var role = await _userManager.GetRolesAsync(user);
                    var token = GetToken(user, role.First());
                    //returns json object                   
                    return Ok(token); 
                }
            }
            return BadRequest();
        }

        /// <summary>
        /// Register a user
        /// </summary>
        /// <param name="model">the user details</param>
        /// <returns></returns>
        [Authorize(Policy = "Multimed", Roles = "Multimed")]
        [HttpPost("register")]
        public async Task<ActionResult<String>> Register(RegisterDTO model)
        {
            IdentityUser user = new IdentityUser { UserName = model.Username, Email = model.Username };
            var result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, "User");
            if (result.Succeeded)
            {
                //return ok so the user knows the account has been created
                return Ok();
            }
            return StatusCode(500);
        }

        /// <summary>
        /// Register a therapist
        /// </summary>
        /// <param name="model">the therapist details</param>
        /// <returns></returns>
        [Authorize(Policy = "Multimed", Roles = "Multimed")]
        [HttpPost("registerTherapist")]
        public async Task<ActionResult<String>> RegisterTherapist(RegisterDTO model)
        {
            IdentityUser user = new IdentityUser { UserName = model.Username };
            var result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, "Therapist");
            if (result.Succeeded)
            {
                //return ok so the user knows the account has been created
                return Ok();
            }
            return StatusCode(500);
        }

        /// <summary>
        /// Register a multimed user
        /// </summary>
        /// <param name="model">the multimed user details</param>
        /// <returns></returns>
        [Authorize(Policy = "Multimed", Roles = "Multimed")]
        [HttpPost("registerMultimed")]
        public async Task<ActionResult<String>> RegisterMultimed(RegisterDTO model)
        {
            IdentityUser user = new IdentityUser { UserName = model.Username};
            var result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, "Multimed");
            if (result.Succeeded)
            {
                //return ok so the user knows the account has been created
                return Ok();
            }
            return StatusCode(500);
        }

        /// <summary>
        /// Checks if an username is available as username
        /// </summary>
        /// <returns>true if the username is not registered yet</returns>
        /// <param name="username">Username.</param>/
        [AllowAnonymous]
        [HttpGet("checkusername")]
        public async Task<ActionResult<Boolean>> CheckAvailableUserName(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            return user == null;
        }

        private String GetToken(IdentityUser user, string role)
        {
            //getuserid from correct table
            var userid = role.Equals("Multimed")
                ?""
                :role.Equals("Therapist")
                    ? _therapistRepo.GetByEmail(user.Email).TherapistId.ToString()
                    : _userRepo.GetByEmail(user.Email).UserId.ToString();
            

            // Create the token
            var claims = new[]
            {
              new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
              new Claim("Id", userid),
              new Claim("Role", role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
              null, null,
              claims,
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
