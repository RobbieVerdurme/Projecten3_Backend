using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.Model
{
    public class Category
    {
        #region Properties

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            var category = obj as Category;
            return category != null &&
                   CategoryId == category.CategoryId &&
                   Name == category.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CategoryId, Name);
        }

        #endregion

        public static bool operator ==(Category c1, Category c2) {
            return c1.CategoryId == c2.CategoryId && c1.Name == c2.Name;
        }

        public static bool operator !=(Category c1, Category c2)
        {
            return c1.CategoryId != c2.CategoryId && c1.Name != c2.Name;
        }
    }
}
