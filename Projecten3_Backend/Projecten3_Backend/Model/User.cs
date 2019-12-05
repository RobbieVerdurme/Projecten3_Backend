using Projecten3_Backend.DTO;
using Projecten3_Backend.Model.ManyToMany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.Model
{
    public class User
    {
        #region Properties

        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string FamilyName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public virtual Company Company { get; set; }


        #endregion

        #region Collections

        public virtual ICollection<ChallengeUser> Challenges { get; set; } = new List<ChallengeUser>();

        public virtual ICollection<TherapistUser> Therapists { get; set; } = new List<TherapistUser>();

        public  ICollection<Category> Categories { get; set; } = new List<Category>();

        public ICollection<LeaderboardScore> LeaderboardScores { get; set; } = new List<LeaderboardScore>();

        #endregion

        #region Methods
        public void AddChallenges(List<ChallengeUser> challenges) => challenges.AddRange(challenges);

        public void AddTherapist(Therapist therapist) => Therapists.Add(new TherapistUser() { Therapist = therapist, TherapistId = therapist.TherapistId, User = this, UserId = this.UserId});

        public void AddLeaderboardScores(List<LeaderboardScore> leaderboardScores) => leaderboardScores.AddRange(leaderboardScores);

        public void RaiseLeaderboardScore()
        {
            DateTime currentDate = DateTime.Now;
            if(LeaderboardScores.Where(c => c.Date.Year==currentDate.Year && c.Date.Month==currentDate.Month).FirstOrDefault() != null)
            {
                LeaderboardScores.Where(c => c.Date.Year == currentDate.Year && c.Date.Month == currentDate.Month).FirstOrDefault().RaiseScore();
            }
            else
            {
                LeaderboardScores.Add(new LeaderboardScore(DateTime.Now, 1));
            }
        }

        public int GetCurrentLeaderboardScore()
        {
            DateTime currentDate = DateTime.Now;
            if (LeaderboardScores.Where(c => c.Date.Year == currentDate.Year && c.Date.Month == currentDate.Month).FirstOrDefault() != null)
            {
                return LeaderboardScores.Where(c => c.Date.Year == currentDate.Year && c.Date.Month == currentDate.Month).FirstOrDefault().Score;
            }
            else
            {
                return 0;
            }
        }

        public static UserDTO MapUserToUserDTO(User usr)
        {
            if(usr != null) {
                UserDTO user = new UserDTO()
                {
                    FirstName = usr.FirstName,
                    FamilyName = usr.FamilyName,
                    Email = usr.Email,
                    Categories = usr.Categories
                };

                return user;
            }
            else
            {
                return null;
            }

        }

        public static LeaderboardDTO MapUserToLeaderboardDTO(User usr)
        {
            if (usr != null)
            {
                LeaderboardDTO leaderboardEntry = new LeaderboardDTO()
                {
                    UserId = usr.UserId,
                    FirstName = usr.FirstName,
                    FamilyName = usr.FamilyName,
                    Score = usr.GetCurrentLeaderboardScore()
                };
                return leaderboardEntry;
            }
            else
            {
                return null;
            }

        }
        #endregion
    }
}
