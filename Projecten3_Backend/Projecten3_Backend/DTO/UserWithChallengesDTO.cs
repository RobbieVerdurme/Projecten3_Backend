using Projecten3_Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.DTO
{
    public class UserWithChallengesDTO
    {
        #region Properties

        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string FamilyName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime Contract { get; set; }

        public ICollection<Category> Categories { get; set; }

        public ICollection<Challenge> Challenges { get; set; }

        public int ExperiencePoints { get; set; }
        #endregion
    }
}
