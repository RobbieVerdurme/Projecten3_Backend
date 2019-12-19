using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.Model.ManyToMany
{
    public class CategoryUser
    {
        #region Properties

        public int CategoryUserId { get; set; }

        public int CategoryId { get; set; }

        public int UserId { get; set; }

        #endregion

        #region Navigational properties

        public virtual Category Category { get; set; }

        public virtual User User { get; set; }

        #endregion
    }
}
