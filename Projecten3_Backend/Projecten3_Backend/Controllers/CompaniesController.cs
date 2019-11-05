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
using Projecten3_Backend.Models;

namespace Projecten3_Backend.Controllers
{
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepo;
        private readonly IUserRepository _userRepo;

        public CompaniesController(ICompanyRepository companyRepository, IUserRepository userRepository)
        {
            _companyRepo = companyRepository;
            _userRepo = userRepository;
        }

        [Route("api/company")]
        [HttpGet]
        public IEnumerable<Company> GetCompany()
        {
            return _companyRepo.GetAll();
        }

        /// <summary>
        /// Get a specific company
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// HTTP 404 if not found.
        /// HTTP 200 otherwise.
        /// </returns>
        [Route("api/company/{id:int}")]
        [HttpGet]
        public IActionResult GetCompany(int id)
        {
            Company company = _companyRepo.GetById(id);

            if (company == null)
            {
                return NotFound();
            }

            return Ok(company);
        }

        /// <summary>
        /// Add a company
        /// </summary>
        /// <param name="addCompany"></param>
        /// <returns>
        /// HTTP 400 if the payload is malformed.
        /// HTTP 303 if such a company already exists.
        /// HTTP 500 if saving failed.
        /// HTTP 200 if successful.
        /// </returns>
        [Authorize(Policy = UserRole.MULTIMED, Roles = UserRole.MULTIMED)]
        [Route("api/company/add")]
        [HttpPost]
        public IActionResult AddCompany(AddCompanyDTO addCompany) {
            if (addCompany == null || string.IsNullOrEmpty(addCompany.City) || string.IsNullOrEmpty(addCompany.Country) || string.IsNullOrEmpty(addCompany.Mail)
                || string.IsNullOrEmpty(addCompany.Name) || string.IsNullOrEmpty(addCompany.Phone) || string.IsNullOrEmpty(addCompany.Street)
                || string.IsNullOrEmpty(addCompany.Site)) return BadRequest();
            if (addCompany.PostalCode < 1000 || 9999 < addCompany.PostalCode) return BadRequest();//Belgian postal codes
            if (addCompany.HouseNumber < 1 || 999 < addCompany.HouseNumber) return BadRequest();//House numbers
            Company company = new Company {
                City = addCompany.City,
                Country = addCompany.Country,
                HouseNumber = addCompany.HouseNumber,
                Mail = addCompany.Mail,
                Name = addCompany.Name,
                Phone = addCompany.Phone,
                PostalCode = addCompany.PostalCode,
                Site = addCompany.Site,
                Street = addCompany.Street
            };

            if (_companyRepo.CompanyExists(company)) return StatusCode(303);
            _companyRepo.AddCompany(company);
            try
            {
                _companyRepo.SaveChanges();
                return Ok();
            }
            catch (Exception) {
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Add employees to a company
        /// </summary>
        /// <param name="addEmployees"></param>
        /// <returns>
        /// HTTP 400 if the payload is malformed.
        /// HTTP 500 if saving failed.
        /// HTTP 200 if successful.
        /// </returns>
        [Authorize(Policy = UserRole.MULTIMED,Roles = UserRole.MULTIMED)]
        [Route("api/company/employee/add")]
        [HttpPut]
        public IActionResult AddEmployees(AddEmployeesDTO addEmployees) {
            if (addEmployees == null || addEmployees.Employees == null) return BadRequest();

            Company company = _companyRepo.GetById(addEmployees.CompanyId);
            if (company == null) return BadRequest();
            if (!_userRepo.UsersExist(addEmployees.Employees)) return BadRequest();

            _companyRepo.AddEmployees(company, addEmployees.Employees);
            try
            {
                _companyRepo.SaveChanges();
                return Ok();
            }
            catch (Exception) {
                return StatusCode(500);
            }
        }

        //edit

        //Delete


    }
}
