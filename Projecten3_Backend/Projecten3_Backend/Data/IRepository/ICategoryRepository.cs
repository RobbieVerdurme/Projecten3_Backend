using Projecten3_Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.Data.IRepository
{
    public interface ICategoryRepository
    {
        bool CategoriesExist(IList<int> ids);

        IEnumerable<Category> GetCategories();

        IEnumerable<Category> GetCategoriesById(IList<int> ids);

        Category GetById(int id);

        void AddCategory(Category category);

        void SaveChanges();
        bool CategoryExists(string name);

        void Update(Category category);
        void DeleteCategory(int id);
    }
}
