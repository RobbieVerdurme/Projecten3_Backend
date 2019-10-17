using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultimedAPI.Models
{
    public class Challenge
    {

        #region Properties

        public int ChallengeId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Category Category{ get; set; }

        #endregion

        #region Collections

        public ICollection<ChallengeUser> ChallengeUsers{ get; set; }


        #endregion

        #region Constructors

        public Challenge()
        {
            ChallengeUsers = new List<ChallengeUser>();
        }

        public Challenge(string title, string description, Category category)
        {
            Title = title;
            Description = description;
            Category = category;
        }


        #endregion

        #region Methods

        public void AddUser(User User) => ChallengeUsers.Add(new ChallengeUser(User, this));

        public User GetChallenge(int id) => ChallengeUsers.SingleOrDefault(c => c.UserId== id).User;

        #endregion
    }
}
