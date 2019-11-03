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
using Projecten3_Backend.Model;
using Projecten3_Backend.Models;

namespace Projecten3_Backend.Controllers
{
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly Projecten3_BackendContext _context;
        private readonly ICategoryRepository _repo;

        public CategoriesController(ICategoryRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Edit a category
        /// </summary>
        /// <param name="category"></param>
        /// <returns>
        /// HTTP 400 if payload is malformed.
        /// HTTP 303 if there is already a category with the new values.
        /// HTTP 500 if saving failed.
        /// HTTP 200 if successful.
        /// </returns>
        [Authorize(Policy = UserRole.MULTIMED, Roles = UserRole.MULTIMED)]
        [Route("api/category/edit")]
        [HttpPut]
        public IActionResult EditCategory(Category category) {
            if (category == null || string.IsNullOrEmpty(category.Name)) return BadRequest();
            if (_repo.CategoryExists(category.Name)) return StatusCode(303);

            Category edited = _repo.GetById(category.CategoryId);
            if (edited == null) return BadRequest();
            edited.Name = category.Name;
            
            _repo.Update(edited);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception) {
                return StatusCode(500);
            }
            return Ok();
        }

        /// <summary>
        /// Delete a category
        /// </summary>
        /// <param name="id"></param>
        /// <returns>
        /// HTTP 404 if the category wasn't found.
        /// HTTP 500 if saving failed.
        /// HTTP 200 if deleted.
        /// </returns>
        [Authorize(Policy = UserRole.MULTIMED, Roles = UserRole.MULTIMED)]
        [Route("api/category/delete/{id:int}")]
        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
            var category = _repo.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            try
            {
                _repo.DeleteCategory(id);
                _repo.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500);
            }

            return Ok();
        }

        [Route("api/category")]
        [HttpGet]
        public IEnumerable<Category> GetCategories() {
            return _repo.GetCategories();
        }


        /// <summary>
        /// Add a category.
        /// </summary>
        /// <param name="category"></param>
        /// <returns>
        /// HTTP 400 if the payload is malformed.
        /// HTTP 500 if saving failed.
        /// HTTP 303 if such a category already exists.
        /// HTTP 200 if saved.
        /// </returns>
        [Authorize(Policy = UserRole.MULTIMED, Roles = UserRole.MULTIMED)]
        [Route("api/category/add")]
        [HttpPost]
        public IActionResult AddCategory(Category category) {
            if (category == null || string.IsNullOrEmpty(category.Name)) return BadRequest();
            if (_repo.CategoryExists(category.Name)) return StatusCode(303);

            _repo.AddCategory(category);

            try
            {
                _repo.SaveChanges();
            }
            catch (Exception) {
                return StatusCode(500);
            }
            return Ok();
        }
    }
}
