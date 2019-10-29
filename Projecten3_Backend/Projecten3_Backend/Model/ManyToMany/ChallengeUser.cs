using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.Model.ManyToMany
{
    public class ChallengeUser
    {
        #region Properties

        public int ChallengeUserId { get; set; }

        public int ChallengeId { get; set; }

        public int UserId { get; set; }

        public DateTime? CompletedDate { get; set; }

        #endregion

        #region Navigational properties

        public virtual Challenge Challenge { get; set; }

        public virtual User User { get; set; }

        #endregion
    }
}
