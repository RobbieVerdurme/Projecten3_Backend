using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.DTO
{
    /// <summary>
    /// This DTO is used for adding challenges to a user.
    /// </summary>
    public class ChallengesUserDTO
    {

        public int UserId { get; set; }

        public ICollection<int> ChallengeIds { get; set; }
    }
}
