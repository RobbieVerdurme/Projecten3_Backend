using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.Model.ManyToMany
{
    public class TherapistUser
    {
        #region Properties

        public int TherapistUserId { get; set; }

        public int TherapistId { get; set; }

        public int UserId { get; set; }

        #endregion

        #region Navigational properties

        public virtual Therapist Therapist { get; set; }

        public virtual User User { get; set; }

        #endregion
    }
}
