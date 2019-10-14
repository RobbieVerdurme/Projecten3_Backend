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

        public ICollection<Challenge> Challenges { get; set; }
        #endregion

        #region Constructors

        public User()
        {
            Challenges = new List<Challenge>();
        }

        public User(string firstName, string familyName, string email)
        {
            FirstName = firstName;
            FamilyName = familyName;
            Email = email;
        }



        #endregion

        #region Methods

        public void AddChallenge(Challenge challenge) => Challenges.Add(challenge);

        public Challenge GetChallenge(int id) => Challenges.SingleOrDefault(c => c.ChallengeId == id);

        #endregion
    }
}
