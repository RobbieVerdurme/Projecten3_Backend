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
                   Name == category.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name);
        }

        #endregion

        //public static bool operator ==(Category c1, Category c2) {
        //    if (c1 == null && c2 == null) return true;
        //    if (c1 != null && c2 == null || c1 == null && c2 != null) return false;

        //    return c1.Name == c2.Name;
        //}

        //public static bool operator !=(Category c1, Category c2)
        //{
        //    if (c1 == null && c2 == null) return false;
        //    if (c1 != null && c2 == null || c1 == null && c2 != null) return true;

        //    return c1.Name != c2.Name;
        //}
    }
}
