using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.DTO
{
    public class CheckDailyChallengeResponseDTO
    {
        public CheckDailyChallengeResponseDTO(DateTime timestamp)
        {
            TimeStamp = timestamp;
        }

        public DateTime TimeStamp { get; private set; }
    }
}
