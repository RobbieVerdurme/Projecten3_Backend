using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.Model
{
    public class TherapistType
    {
        #region Properties

        public int TherapistTypeId { get; set; }

        public string Type { get; set; }
        #endregion

        #region Collections

        public ICollection<Category> Categories { get; set; }


        #endregion

        #region Methods

        public void AddCategory(Category category) => Categories.Add(category);

        public override bool Equals(object obj)
        {
            var type = obj as TherapistType;
            return type != null &&
                   TherapistTypeId == type.TherapistTypeId &&
                   Type == type.Type;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TherapistTypeId, Type);
        }

        public static bool operator ==(TherapistType t1, TherapistType t2) {
            return t1.TherapistTypeId == t2.TherapistTypeId && t1.Type == t2.Type;
        }

        public static bool operator !=(TherapistType t1, TherapistType t2)
        {
            return t1.TherapistTypeId != t2.TherapistTypeId && t1.Type != t2.Type;
        }

        #endregion
    }
}
