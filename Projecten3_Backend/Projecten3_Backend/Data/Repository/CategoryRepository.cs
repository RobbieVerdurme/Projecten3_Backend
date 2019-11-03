using Projecten3_Backend.Data.IRepository;
using Projecten3_Backend.Model;
using Projecten3_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.Data.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Projecten3_BackendContext _dbContext;

        public CategoryRepository(Projecten3_BackendContext context)
        {
            _dbContext = context;
        }

        public bool CategoriesExist(IList<int> ids)
        {
            IList<int> categories = _dbContext.Category.Select( c => c.CategoryId).ToList();
            foreach (int id in ids)
            {
                if (!categories.Contains(id)) return false;
            }
            return true;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _dbContext.Category.ToList();
        }

        public IEnumerable<Category> GetCategoriesById(IList<int> ids)
        {
            return _dbContext.Category.Where(c => ids.Contains(c.CategoryId)).ToList();
        }
    }
}
