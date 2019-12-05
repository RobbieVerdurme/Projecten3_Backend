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

        public void RaiseScore()
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

        public int GetCurrentScore()
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
                    Score = usr.GetCurrentScore()
                };
                return leaderboardEntry;
            }
            else
            {
                return null;
            }

        }

        //public override bool Equals(object obj)
        //{
        //    var user = obj as User;
        //    return user != null &&
        //           FirstName == user.FirstName &&
        //           FamilyName == user.FamilyName &&
        //           Email == user.Email &&
        //           Phone == user.Phone &&
        //           EqualityComparer<Company>.Default.Equals(Company, user.Company);
        //}

        //public override int GetHashCode()
        //{
        //    return HashCode.Combine(FirstName, FamilyName, Email, Phone, Company);
        //}

        //public static bool operator ==(User u1, User u2) {
        //    if (u1 == null && u2 == null) return true;
        //    if (u1 != null && u2 == null || u1 == null && u2 != null) return false;

        //    return u1.Company == u2.Company && u1.Email == u2.Email && u1.FamilyName == u2.FamilyName && u1.FirstName == u2.FirstName && u1.Phone == u2.Phone;
        //}

        //public static bool operator !=(User u1, User u2)
        //{
        //    if (u1 == null && u2 == null) return false;
        //    if (u1 != null && u2 == null || u1 == null && u2 != null) return true;

        //    return u1.Company != u2.Company && u1.Email != u2.Email && u1.FamilyName != u2.FamilyName && u1.FirstName != u2.FirstName && u1.Phone != u2.Phone;
        //}

        #endregion
    }
}
