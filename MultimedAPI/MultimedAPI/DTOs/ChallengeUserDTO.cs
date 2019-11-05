using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultimedAPI.DTOs
{
    public class ChallengesUserDTO
    {

        public int UserId { get; set; }

        public ICollection<int> ChallengeIds { get; set; }
    }
}
