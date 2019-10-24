using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultimedAPI.Models.ManyToManies
{
    public class TherapistUser
    {

        #region Properties

        public int TherapistUserId { get; set; }

        public int TherapistId { get; set; }


        public int UserId { get; set; }

        #endregion

        #region Navigational properties

        public Therapist Therapist { get; set; }

        public User User { get; set; }

        #endregion

        #region Constructors

        public TherapistUser()
        {

        }

        public TherapistUser(Therapist therapist, User user)
        {
            Therapist = therapist;
            User = user;

            TherapistId = Therapist.TherapistId;
            UserId = User.UserId;
        }


        #endregion

    }
}
