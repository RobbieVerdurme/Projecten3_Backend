using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultimedAPI.Models.ManyToManies
{
    public class ChallengeUser
    {

        #region Properties

        public int ChallengeUserId { get; set; }

        public int ChallengeId { get; set; }

        public int UserId { get; set; }

        #endregion

        #region Navigational properties

        public User User { get; set; }

        public Challenge Challenge { get; set; }


        #endregion

        #region Constructors

        public ChallengeUser()
        {
        }

        public ChallengeUser(User user, Challenge challenge) : this()
        {
            User = user;
            UserId = User.UserId;

            Challenge = challenge;
            ChallengeId = Challenge.ChallengeId;
        }


        #endregion
    }
}
