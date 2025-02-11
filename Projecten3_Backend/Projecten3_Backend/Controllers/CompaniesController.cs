﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projecten3_Backend.Data;
using Projecten3_Backend.Data.IRepository;
using Projecten3_Backend.DTO;
using Projecten3_Backend.Model;


namespace Projecten3_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        #region prop
        private readonly ICompanyRepository _companyRepo;
        private readonly IUserRepository _userRepo;
        #endregion

        #region ctor
        public CompaniesController(ICompanyRepository companyRepository, IUserRepository userRepository)
        {
            _companyRepo = companyRepository;
            _userRepo = userRepository;
        }
        #endregion

        #region methodes
        [HttpGet]
        public IActionResult GetCompany()
        {
            return Ok(_companyRepo.GetAll().Select(c =>Company.MapCompanyToCompanyDTO(c)));
        }

        /// <summary>
        /// Get a specific company
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// HTTP 404 if not found.
        /// HTTP 200 otherwise.
        /// </returns>
        [Route("{id}")]
        [HttpGet]
        public IActionResult GetCompany(int id)
        {
            Company company = _companyRepo.GetById(id);

            if(company == null)
            {
                return NotFound();
            }
            return Ok(Company.MapCompanyToCompanyDTO(company));
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
        [HttpPost]
        public IActionResult AddCompany(AddCompanyDTO addCompany) {
            if (addCompany == null || string.IsNullOrEmpty(addCompany.City) || string.IsNullOrEmpty(addCompany.Country) || string.IsNullOrEmpty(addCompany.Mail)
                || string.IsNullOrEmpty(addCompany.Name) || string.IsNullOrEmpty(addCompany.Phone) || string.IsNullOrEmpty(addCompany.Street)) return BadRequest();
            if (addCompany.PostalCode < 1000 || 9999 < addCompany.PostalCode) return BadRequest();//Belgian postal codes
            if (addCompany.HouseNumber < 1 || 999 < addCompany.HouseNumber) return BadRequest();//House numbers
            Company company = Company.MapAddCompanyDTOToCompany(addCompany);

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
        /// Edit a company
        /// </summary>
        /// <param name="company"></param>
        /// <returns>
        /// HTTP 400 if the payload is malformed.
        /// HTTP 500 if saving failed.
        /// HTTP 303 if a company with the updated values already exists.
        /// HTTP 200 if successful.
        /// </returns>
        [Authorize(Policy = UserRole.MULTIMED, Roles = UserRole.MULTIMED)]
        [Route("edit")] 
        [HttpPut]
        public IActionResult EditCompany(EditCompanyDTO company)
        {
            if (company == null || string.IsNullOrEmpty(company.City) || string.IsNullOrEmpty(company.Country)
                || string.IsNullOrEmpty(company.Mail) || string.IsNullOrEmpty(company.Name) || string.IsNullOrEmpty(company.Phone) || string.IsNullOrEmpty(company.Street)) return BadRequest();

            if (company.PostalCode < 1000 || 9999 < company.PostalCode) return BadRequest();//Belgian postal codes
            if (company.HouseNumber < 1 || 999 < company.HouseNumber) return BadRequest();//House numbers

            Company companyFromDatabase = _companyRepo.GetById(company.CompanyId);

            if(companyFromDatabase == null)
            {
                return StatusCode(404);
            }
            companyFromDatabase = Company.MapEditCompanyDTOToCompany(company, companyFromDatabase);
            
            try
            {
                _companyRepo.UpdateCompany(companyFromDatabase);
                _companyRepo.SaveChanges();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
            return Ok();
        }

        /// <summary>
        /// Delete a company
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// HTTP 404 if the company was not found.
        /// HTTP 500 if deleting failed.
        /// HTTP 200 if deleted.
        /// </returns>
        [Route("{id}")]
        [HttpDelete]
        [Authorize(Policy = UserRole.MULTIMED, Roles = UserRole.MULTIMED)]
        public IActionResult DeleteCompany(int id)
        {
            if (_companyRepo.GetById(id) == null) return NotFound();
            try
            {
                _companyRepo.DeleteCompany(id);
                _companyRepo.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500);
            }
            return Ok();
        }
        
        #endregion
    }
}
