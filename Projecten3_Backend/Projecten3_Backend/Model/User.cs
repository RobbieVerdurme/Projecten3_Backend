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

        public DateTime Contract { get; set; }

        public int ExperiencePoints { get; set; }

        #endregion

        #region Collections

        public virtual ICollection<ChallengeUser> Challenges { get; set; } = new List<ChallengeUser>();

        public virtual ICollection<TherapistUser> Therapists { get; set; } = new List<TherapistUser>();

        public virtual ICollection<CategoryUser> Categories { get; set; } = new List<CategoryUser>();

        public ICollection<LeaderboardScore> LeaderboardScores { get; set; } = new List<LeaderboardScore>();

        #endregion

        #region Methods
        public void AddChallenges(List<ChallengeUser> challenges) => challenges.ForEach(ch => Challenges.Add(ch));

        public void AddTherapist(Therapist therapist) => Therapists.Add(new TherapistUser() { Therapist = therapist, TherapistId = therapist.TherapistId, User = this, UserId = this.UserId});

        public void AddCategories(List<CategoryUser> categoryUsers) => categoryUsers.ForEach(c => Categories.Add(c));

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
                    UserId = usr.UserId,
                    FirstName = usr.FirstName,
                    FamilyName = usr.FamilyName,
                    Email = usr.Email,
                    Phone = usr.Phone,
                    Categories = usr.Categories.Select(c => c.Category),
                    Contract = usr.Contract,
                    ExperiencePoints = usr.ExperiencePoints
                };

                return user;
            }
            else
            {
                return null;
            }

        }

        public static UserWithChallengesDTO MapUserToUserWithChallengesDTO(User usr, List<ChallengesOfUserDTO> challenges)
        {
            if(usr != null)
            {
                UserWithChallengesDTO user = new UserWithChallengesDTO()
                {
                    UserId = usr.UserId,
                    FirstName = usr.FirstName,
                    FamilyName = usr.FamilyName,
                    Email = usr.Email,
                    Phone = usr.Phone,
                    Categories = usr.Categories.Select(c => c.Category),
                    Contract = usr.Contract,
                    ExperiencePoints = usr.ExperiencePoints,
                    Challenges = challenges
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
                    FamilyName = usr.FamilyName,
                    FirstName = usr.FirstName,
                    Score = usr.GetCurrentLeaderboardScore()
                };
                return leaderboardEntry;
            }
            return null;
          }
        #endregion
    }
}
