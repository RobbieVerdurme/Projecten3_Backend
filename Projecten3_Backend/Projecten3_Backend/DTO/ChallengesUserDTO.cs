using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.DTO
{
    public class ChallengesUserDTO
    {

        public int UserId { get; set; }

        public ICollection<int> ChallengeIds { get; set; }
    }
}
