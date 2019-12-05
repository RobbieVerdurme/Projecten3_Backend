using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.DTO
{
    public class UserWithChallengesDTO : UserDTO
    {
        public ICollection<ChallengesOfUserDTO> Challenges { get; set; }
    }
}
