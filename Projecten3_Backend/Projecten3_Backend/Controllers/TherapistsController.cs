using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
    public class TherapistsController : ControllerBase
    {
        private readonly ITherapistRepository _therapistRepo;
        private readonly IUserRepository _userRepo;
        private readonly ICategoryRepository _categoryRepository;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public TherapistsController(ITherapistRepository repo, IUserRepository userRepo, ICategoryRepository categoryRepository, SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager)
        {
            _therapistRepo = repo;
            _userRepo = userRepo;
            _categoryRepository = categoryRepository;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [Route("api/therapist")]
        [HttpGet]
        public IActionResult GetTherapist()
        {
            return Ok(_therapistRepo.GetTherapists());
        }

        /// <summary>
        /// Get a specific therapist
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// HTTP 404 if not found.
        /// HTTP 200 otherwise.
        /// </returns>
        [Route("api/therapist/{id:int}")]
        [HttpGet]
        public IActionResult GetTherapist(int id)
        {
            Therapist t = _therapistRepo.GetById(id);

            if (t == null)
            {
                return NotFound();
            }

            return Ok(Therapist.MapTherapistToGetTherapistDetailsDTO(t));
        }

        /// <summary>
        /// Get the clients of a therapist
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// HTTP 404 if not found.
        /// HTTP 200 otherwise.
        /// </returns>
        [Route("api/therapist/clients/{id:int}")]
        [HttpGet]
        public IActionResult GetTherapistClients(int id)
        {
            Therapist t = _therapistRepo.GetById(id);

            if (t == null)
            {
                return NotFound();
            }
            return Ok(t.ClientList.Select(client => Model.User.MapUserToUserDTO(client, _userRepo.GetUserCategories(client.UserId).ToList())).ToList());
        }

        /// <summary>
        /// Edit a therapist
        /// </summary>
        /// <param name="therapist"></param>
        /// <returns>
        /// HTTP 400 if the payload is malformed.
        /// HTTP 303 if a therapist with the edited values already exists.
        /// HTTP 500 if saving failed.
        /// HTTP 200 if successful.
        /// </returns>
        [Authorize(Roles = UserRole.MULTIMED_AND_THERAPIST)]
        [Route("api/therapist/edit")]
        [HttpPut]
        public IActionResult EditTherapist(EditTherapistDTO therapist) {
            if (therapist == null || string.IsNullOrEmpty(therapist.City) || string.IsNullOrEmpty(therapist.Email) || string.IsNullOrEmpty(therapist.FirstName)
                || string.IsNullOrEmpty(therapist.LastName) || string.IsNullOrEmpty(therapist.PhoneNumber) || string.IsNullOrEmpty(therapist.Street)
                || string.IsNullOrEmpty(therapist.Website)) return BadRequest();

            if (therapist.PostalCode < 1000 || 9999 < therapist.PostalCode) return BadRequest();//Belgian postal codes
            if (therapist.HouseNumber < 1 || 999 < therapist.HouseNumber) return BadRequest();//House numbers
            TherapistType tt = _therapistRepo.GetTherapistType(therapist.TherapistTypeId);
            if (tt == null) return BadRequest();//Therapist type

            Therapist edited = _therapistRepo.GetById(therapist.TherapistId);

            if (edited == null) return BadRequest();

            edited = Therapist.MapEditTherapistDTOToTherapist(therapist, edited);

            _therapistRepo.UpdateTherapist(edited);
            try
            {
                _therapistRepo.SaveChanges();
            }
            catch (Exception) {
                return StatusCode(500);
            }
            return Ok();
        }

        /// <summary>
        /// Add a therapist
        /// </summary>
        /// <param name="therapist"></param>
        /// <returns>
        /// HTTP 400 if the payload is malformed.
        /// HTTP 303 if a therapist with the given values already exists.
        /// HTTP 500 if saving failed.
        /// HTTP 200 if successful.
        /// </returns>
        [Authorize(Roles = UserRole.MULTIMED)]
        [Route("api/therapist/add")]
        [HttpPost]
        public async Task<IActionResult> AddTherapist(AddTherapistDTO therapist) {
            if (therapist == null || string.IsNullOrEmpty(therapist.City) || string.IsNullOrEmpty(therapist.Email) || string.IsNullOrEmpty(therapist.FirstName)
                || string.IsNullOrEmpty(therapist.LastName) || string.IsNullOrEmpty(therapist.PhoneNumber) || string.IsNullOrEmpty(therapist.Street)
                || string.IsNullOrEmpty(therapist.Website)) return BadRequest();

            if (therapist.PostalCode < 1000 || 9999 < therapist.PostalCode) return BadRequest();//Belgian postal codes
            if (therapist.HouseNumber < 1 || 999 < therapist.HouseNumber) return BadRequest();//House numbers
            if (!_therapistRepo.TherapistTypeExists(therapist.TherapistTypeId)) return BadRequest();//Therapist type

            Therapist t = Therapist.MapAddTherapistDTOToTherapist(therapist, _therapistRepo.GetTherapistType(therapist.TherapistTypeId)); 

            if(_therapistRepo.TherapistExists(t)) return StatusCode(303);
            
            try
            {
                IdentityUser user = new IdentityUser { UserName = therapist.Email, Email = therapist.Email };
                var result = await _userManager.CreateAsync(user, "Multimed@" + therapist.FirstName + therapist.LastName + therapist.PhoneNumber);
                await _userManager.AddToRoleAsync(user, UserRole.THERAPIST);
                if (result.Succeeded)
                {
                    _therapistRepo.AddTherapist(t);
                    _therapistRepo.SaveChanges();
                    //return ok so the user knows the account has been created
                    return Ok();
                }
               
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
            return StatusCode(303);
        }

        /// <summary>
        /// Add a therapist type
        /// </summary>
        /// <param name="addTherapistType"></param>
        /// <returns>
        /// HTTP 400 if the payload is malformed.
        /// HTTP 303 if such a therapist type already exists.
        /// HTTP 500 if saving failed.
        /// HTTP 200 if successful.
        /// </returns>
        [Authorize(Policy = UserRole.MULTIMED, Roles = UserRole.MULTIMED)]
        [Route("api/therapist/type/add")]
        [HttpPost]
        public IActionResult AddTherapistType(AddTherapistTypeDTO addTherapistType) {
            if (addTherapistType == null || string.IsNullOrEmpty(addTherapistType.Type) || !_categoryRepository.CategoriesExist(addTherapistType.Categories)) return BadRequest();

            if (_therapistRepo.TherapistTypeExists(addTherapistType.Type, addTherapistType.Categories)) return StatusCode(303);
            TherapistType type = TherapistType.MapAddTherapistTypeDTOToTherapistType(addTherapistType, _categoryRepository.GetCategoriesById(addTherapistType.Categories));
            _therapistRepo.AddTherapistType(type);
            try
            {
                _therapistRepo.SaveChanges();
                return Ok();
            }
            catch (Exception) {
                return StatusCode(500);
            }
        }

        [Route("api/therapist/type")]
        [HttpGet]
        public IActionResult GetTherapistTypes() {
            return Ok(_therapistRepo.GetTherapistTypes());
        }

        /// <summary>
        /// Edit a therapist type.
        /// </summary>
        /// <param name="edit"></param>
        /// <returns>
        /// HTTP 400 if the payload is malformed.
        /// HTTP 303 if a therapisttype with these values already exists.
        /// HTTP 500 if saving failed.
        /// HTTP 200 if successful.
        /// </returns>
        [Authorize(Policy = UserRole.MULTIMED, Roles = UserRole.MULTIMED)]
        [Route("api/therapist/type/edit")]
        [HttpPut]
        public IActionResult EditTherapistType(EditTherapistTypeDTO edit) {
            if (edit == null || string.IsNullOrEmpty(edit.Type)) return BadRequest();

            TherapistType therapistType = _therapistRepo.GetTherapistType(edit.Id);
            if (therapistType == null || !_categoryRepository.CategoriesExist(edit.Categories)) return BadRequest();
            therapistType.Categories = _categoryRepository.GetCategoriesById(edit.Categories).ToList();
            therapistType.Type = edit.Type;

            
            _therapistRepo.EditTherapistType(therapistType);
            try
            {
                _therapistRepo.SaveChanges();
                return Ok();
            }
            catch (Exception) {
                return StatusCode(500);
            }
        }


        //Delete therapist type


        // DELETE therapist
        /// <summary>
        /// Delete a therapist.
        /// </summary>
        /// <param name="delete"></param>
        /// <returns>
        /// HTTP 400 if the payload is malformed.
        /// HTTP 303 if a therapisttype with these values already exists.
        /// HTTP 500 if saving failed.
        /// HTTP 200 if successful.
        /// </returns>
        [Authorize(Policy = UserRole.MULTIMED, Roles = UserRole.MULTIMED)]
        [Route("api/therapist/{id}")]
        [HttpDelete]
        public IActionResult DeleteTherapistById(int id)
        {
            Therapist therapist = _therapistRepo.GetById(id);
            if(therapist == null)
            {
                return BadRequest();
            }
            try
            {
                _therapistRepo.DeleteTherapist(id);
                _therapistRepo.SaveChanges();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
            return Ok();
        }
    }
}