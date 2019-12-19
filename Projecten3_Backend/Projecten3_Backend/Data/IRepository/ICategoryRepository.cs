using Projecten3_Backend.Model;
using Projecten3_Backend.Model.ManyToMany;
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
        bool CategoryExists(string category);

        void Update(Category category);
        void DeleteCategory(int id);
        void AddCategoriesUsers(List<CategoryUser> categoryUsers);
    }
}
