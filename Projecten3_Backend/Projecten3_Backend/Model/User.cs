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

        #endregion

        #region Methods
        public void addChallenges(List<ChallengeUser> challenges) => challenges.AddRange(challenges);

        public void AddTherapist(Therapist therapist) => Therapists.Add(new TherapistUser() { Therapist = therapist, TherapistId = therapist.TherapistId, User = this, UserId = this.UserId});

        public static UserDTO MapUserToUserDTO(User usr)
        {
            UserDTO user = new UserDTO()
            {
                FirstName = usr.FirstName,
                FamilyName = usr.FamilyName,
                Email = usr.Email,
                Categories = usr.Categories
            };

            return user;
        }

        #endregion
    }
}
