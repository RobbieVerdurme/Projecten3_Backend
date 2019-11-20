using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Projecten3_Backend.Data;
using Projecten3_Backend.Data.IRepository;
using Projecten3_Backend.Model;

namespace Projecten3_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
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
            if(model == null || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password)) return BadRequest();

            var user = await _userManager.FindByNameAsync(model.Username);

            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

                 if (result.Succeeded)
                {
                    var role = await _userManager.GetRolesAsync(user);
                    var userid = getUserid(user, role.First());
                    if(userid == null)
                    {
                        return StatusCode(401);
                    }
                    var token = GetToken(user, role.First(), userid);
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
        [Authorize( Roles = UserRole.MULTIMED)]
        [HttpPost("register")]
        public async Task<ActionResult<String>> Register(RegisterDTO model)
        {
            if (model == null || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password)) return BadRequest();

            IdentityUser user = new IdentityUser { UserName = model.Username, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            await _userManager.AddToRoleAsync(user, "User");
            if (result.Succeeded)
            {
                //return ok so the user knows the account has been created
                return Ok();
            }
            return StatusCode(303);
        }

        /// <summary>
        /// Register a therapist
        /// </summary>
        /// <param name="model">the therapist details</param>
        /// <returns></returns>
        [Authorize(Policy = UserRole.MULTIMED, Roles = UserRole.MULTIMED)]
        [HttpPost("registerTherapist")]
        public async Task<ActionResult<String>> RegisterTherapist(RegisterDTO model)
        {
            if (model == null || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password)) return BadRequest();
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
        [Authorize(Policy = UserRole.MULTIMED, Roles = UserRole.MULTIMED)]
        [HttpPost("registerMultimed")]
        public async Task<ActionResult<String>> RegisterMultimed(RegisterDTO model)
        {
            if (model == null || string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password)) return BadRequest();
            IdentityUser user = new IdentityUser { UserName = model.Username, Email = model.Email};
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

        private String GetToken(IdentityUser user, string role, string userid)
        {          

            // Create the token
            var claims = new[]
            {
              new Claim(JwtRegisteredClaimNames.Sub, user.Email),
              new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
              new Claim("Id", userid),
              new Claim("roles", role)
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

        private string getUserid(IdentityUser user, string role)
        {
            //getuserid from correct table

            if (role.Equals("Multimed"))
            {
                return "";
            }
            else if (role.Equals("Therapist"))
            {
                return _therapistRepo.GetByEmail(user.Email).TherapistId.ToString();
            }
            else
            {
                User usr = _userRepo.GetByEmail(user.Email);
                if (usr.Contract <= DateTime.Now)
                {
                    return null;
                }
                else
                {
                    return usr.UserId.ToString();
                }
            }
        }
    }
}
