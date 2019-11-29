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

        public ICollection<Category> Categories { get; set; } = new List<Category>();

        public ICollection<Challenge> ChallengesList => Challenges.Select(ch => ch.Challenge).ToList();

        #endregion

        #region Methods
        public void AddChallenges(List<ChallengeUser> challenges) => challenges.AddRange(challenges);

        public void AddTherapist(Therapist therapist) => Therapists.Add(new TherapistUser() { Therapist = therapist, TherapistId = therapist.TherapistId, User = this, UserId = this.UserId});

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
                    Categories = usr.Categories,
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
                    Categories = usr.Categories,
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
        #endregion
    }
}
