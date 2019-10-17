using Microsoft.EntityFrameworkCore;
using MultimedAPI.Models;
using MultimedAPI.Models.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultimedAPI.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly MultimedDbContext _dbContext;
        private readonly DbSet<Category> _categories;

        public CategoryRepository(MultimedDbContext dbContext)
        {
            _dbContext = dbContext;
            _categories = _dbContext.Categories;
        }

        public IEnumerable<Category> GetAll()
        {
            return _categories.OrderBy(c => c.Name);
        }

        public Category GetById(int id)
        {
            return _categories.SingleOrDefault(c => c.CategoryId == id);
        }

        public void AddCategory(Category category)
        {
            _categories.Add(category);
        }

        public void DeleteCategory(Category category)
        {
            _categories.Remove(category);
        }

        public void UpdateCategory(Category category)
        {
            _categories.Update(category);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        
    }
}
