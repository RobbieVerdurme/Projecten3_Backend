using Projecten3_Backend.DTO;
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

        //stars in the android app
        public int Rating { get; set; }

        public string Feedback { get; set; }
        #endregion

        #region Navigational properties

        public virtual Challenge Challenge { get; set; }

        public virtual User User { get; set; }

        #endregion

        public static UserChallengeDTO MapToUserChallengeDTO(ChallengeUser challengeUser) {
            return challengeUser == null || challengeUser.User == null || challengeUser.Challenge == null ? null: new UserChallengeDTO {
                UserId = challengeUser.UserId,
                UserFirstName = challengeUser.User.FirstName,
                UserFamilyName = challengeUser.User.FamilyName,
                CompletedDate = challengeUser.CompletedDate,
                Challenge = challengeUser.Challenge,
                Rating = challengeUser.Rating,
                Feedback = challengeUser.Feedback
            };
        }

        public static ChallengesOfUserDTO MapToChallengesOfUserDTO(ChallengeUser challengeUser)
        {
            return challengeUser == null || challengeUser.User == null || challengeUser.Challenge == null ? null : new ChallengesOfUserDTO
            {
                ChallengeId = challengeUser.Challenge.ChallengeId,
                Category = challengeUser.Challenge.Category,
                ChallengeImage = challengeUser.Challenge.ChallengeImage,
                Description = challengeUser.Challenge.Description,
                Title = challengeUser.Challenge.Title,
                CompletedDate = challengeUser.CompletedDate,
                Rating = challengeUser.Rating,
                Feedback = challengeUser.Feedback
            };
        }
    }
}
