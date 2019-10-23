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

        public ICollection<ChallengeUser> ChallengesUser { get; set; }

        #endregion

        #region Constructors

        public User()
        {
            ChallengesUser = new List<ChallengeUser>();
        }

        public User(string firstName, string familyName, string email)
        {
            FirstName = firstName;
            FamilyName = familyName;
            Email = email;
        }



        #endregion

        #region Methods

        public void AddChallengeUser(ChallengeUser challengeUser) => ChallengesUser.Add(challengeUser);

        public Challenge GetChallenge(int id) => ChallengesUser.SingleOrDefault(c => c.ChallengeId == id).Challenge;

        #endregion
    }
}
