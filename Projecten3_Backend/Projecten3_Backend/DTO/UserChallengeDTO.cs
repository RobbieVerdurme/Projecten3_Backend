using Projecten3_Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.DTO
{
    /// <summary>
    /// This class represents a DTO for retrieving a user's challenges.
    /// </summary>
    public class UserChallengeDTO
    {
        public int UserId { get; set; }

        public string UserFirstName { get; set; }

        public string UserFamilyName { get; set; }

        public Challenge Challenge { get; set; }

        public DateTime? CompletedDate { get; set; }
    }
}
