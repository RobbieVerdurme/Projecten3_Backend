using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultimedAPI.Models.IRepositories
{
    public interface ICategoryRepository
    {

        IEnumerable<Category> GetAll();

        Category GetById(int id);

        void AddCategory(Category category);

        void UpdateCategory(Category category);

        void DeleteCategory(Category category);

        void SaveChanges();
    }
}
