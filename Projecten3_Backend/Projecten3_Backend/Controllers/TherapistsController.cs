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
    public class TherapistsController : ControllerBase
    {
        private readonly ITherapistRepository _repo;
        private readonly IUserRepository _userRepo;
        private readonly ICategoryRepository _categoryRepository;

        public TherapistsController(ITherapistRepository repo, IUserRepository userRepo, ICategoryRepository categoryRepository)
        {
            _repo = repo;
            _userRepo = userRepo;
            _categoryRepository = categoryRepository;
        }

        [Route("api/therapist")]
        [HttpGet]
        public IEnumerable<Therapist> GetTherapist()
        {
            return _repo.GetTherapists();
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
        public ActionResult<Therapist> GetTherapist(int id)
        {
            Therapist t = _repo.GetById(id);

            if (t == null)
            {
                return NotFound();
            }

            return t;
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
            if (!_repo.TherapistTypeExists(therapist.TherapistTypeId)) return BadRequest();//Therapist type
            if (therapist.OpeningTimes == null || _repo.HasInvalidOpeningTimes(therapist.OpeningTimes)) return BadRequest(); //Opening times
            if (!_userRepo.UsersExist(therapist.Clients)) return BadRequest();//Clients

            Therapist edited = _repo.GetById(therapist.TherapistId);
            List<OpeningTimes> openingTimes = new List<OpeningTimes>(_repo.GetOpeningTimesForTherapist(therapist.TherapistId));
            for (int i = 0; i < 7; i++) {
                openingTimes[i].Interval = therapist.OpeningTimes[i];
            }
            if (edited == null) return BadRequest();
            edited.City = therapist.City;
            edited.Clients = _userRepo.GetUsers().Where(u => therapist.Clients.Contains(u.UserId)).Select(u => new TherapistUser {
                UserId = u.UserId,
                User = u,
                TherapistId = edited.TherapistId,
                Therapist = edited
            }).ToList();
            edited.Email = therapist.Email;
            edited.FirstName = therapist.FirstName;
            edited.HouseNumber = therapist.HouseNumber;
            edited.LastName = therapist.LastName;
            edited.OpeningTimes = openingTimes;
            edited.PhoneNumber = therapist.PhoneNumber;
            edited.PostalCode = therapist.PostalCode;
            edited.Street = therapist.Street;
            edited.Website = therapist.Website;

            if (_repo.TherapistExists(edited)) return StatusCode(303);

            _repo.UpdateTherapist(edited);
            try
            {
                _repo.SaveChanges();
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
        public IActionResult AddTherapist(AddTherapistDTO therapist) {
            if (therapist == null || string.IsNullOrEmpty(therapist.City) || string.IsNullOrEmpty(therapist.Email) || string.IsNullOrEmpty(therapist.FirstName)
                || string.IsNullOrEmpty(therapist.LastName) || string.IsNullOrEmpty(therapist.PhoneNumber) || string.IsNullOrEmpty(therapist.Street)
                || string.IsNullOrEmpty(therapist.Website)) return BadRequest();

            if (therapist.PostalCode < 1000 || 9999 < therapist.PostalCode) return BadRequest();//Belgian postal codes
            if (therapist.HouseNumber < 1 || 999 < therapist.HouseNumber) return BadRequest();//House numbers
            if (!_repo.TherapistTypeExists(therapist.TherapistTypeId)) return BadRequest();//Therapist type

            Therapist t = new Therapist {
                City = therapist.City,
                Email = therapist.Email,
                FirstName = therapist.FirstName,
                HouseNumber = therapist.HouseNumber,
                LastName = therapist.LastName,
                PhoneNumber = therapist.PhoneNumber,
                PostalCode = therapist.PostalCode,
                Street = therapist.Street,
                TherapistType = _repo.GetTherapistTypes().First(tt => tt.TherapistTypeId == therapist.TherapistTypeId),
                Website = therapist.Website
            };

            if(_repo.TherapistExists(t)) return StatusCode(303);
            _repo.AddTherapist(t);
            try
            {
                _repo.SaveChanges();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
            return Ok();
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

            if (_repo.TherapistTypeExists(addTherapistType.Type, addTherapistType.Categories)) return StatusCode(303);

            _repo.AddTherapistType(new TherapistType {
                Categories = new List<Category>(_categoryRepository.GetCategoriesById(addTherapistType.Categories)),
                Type = addTherapistType.Type
            });
            try
            {
                _repo.SaveChanges();
                return Ok();
            }
            catch (Exception) {
                return StatusCode(500);
            }
        }

        [Route("api/therapist/type")]
        [HttpGet]
        public IEnumerable<TherapistType> GetTherapistTypes() {
            return _repo.GetTherapistTypes();
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
            if (edit == null || string.IsNullOrEmpty(edit.Type) || _categoryRepository.CategoriesExist(edit.Categories)) return BadRequest();

            TherapistType therapistType = _repo.GetTherapistType(edit.Id);
            therapistType.Categories = new List<Category>(_categoryRepository.GetCategoriesById(edit.Categories));
            therapistType.Type = edit.Type;

            if (_repo.TherapistTypeExists(therapistType)) return StatusCode(303);
            _repo.EditTherapistType(therapistType);
            try
            {
                _repo.SaveChanges();
                return Ok();
            }
            catch (Exception) {
                return StatusCode(500);
            }
        }
        

        //Delete therapist type

        // DELETE therapist




    }
}
