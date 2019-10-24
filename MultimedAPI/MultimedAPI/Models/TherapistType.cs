using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultimedAPI.Models
{
    public class TherapistType
    {

        #region Properties

        public int TherapistTypeId { get; set; }

        public string Type { get; set; }
        #endregion

        #region Collections

        public ICollection<Category> Categories{ get; set; }


        #endregion

        #region Constructors

        public TherapistType()
        {
            Categories = new List<Category>();
        }

        public TherapistType(string type)
        {
            Type = type;
        }

        #endregion

        #region Methods

         public void AddCategory(Category category) => Categories.Add(category);

        #endregion
    }
}
