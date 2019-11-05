using MultimedAPI.Models.ManyToManies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultimedAPI.Models
{
    public class User
    {

        #region Properties

        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string FamilyName { get; set; }

        public string Email { get; set; }

        #endregion

        #region Collections

        public ICollection<ChallengeUser> ChallengesUsers { get; set; }

        public ICollection<TherapistUser> TherapistUsers{ get; set; }

        public ICollection<Category> Categories { get; set; }

        #endregion

        #region Constructors

        public User()
        {
            ChallengesUsers = new List<ChallengeUser>();
            TherapistUsers = new List<TherapistUser>();
            Categories = new List<Category>();
        }

        public User(string firstName, string familyName, string email)
        {
            FirstName = firstName;
            FamilyName = familyName;
            Email = email;
        }



        #endregion

        #region Methods

        public void AddChallengeUser(Challenge challenge) => ChallengesUsers.Add(new ChallengeUser(this, challenge));

        public void AddTherapistUser(Therapist therapist) => TherapistUsers.Add(new TherapistUser(therapist, this));


        #endregion
    }
}
