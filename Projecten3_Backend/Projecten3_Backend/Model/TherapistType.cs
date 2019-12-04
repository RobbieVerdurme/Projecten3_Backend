using Projecten3_Backend.DTO;
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

        public static TherapistType MapAddTherapistTypeDTOToTherapistType(AddTherapistTypeDTO addTherapistType, IEnumerable<Category> categories)
        {
            return new TherapistType
            {
                Categories = new List<Category>(categories),
                Type = addTherapistType.Type
            };
        }

        #endregion
    }
}
