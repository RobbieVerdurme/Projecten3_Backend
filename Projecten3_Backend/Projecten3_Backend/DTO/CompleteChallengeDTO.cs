using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.DTO
{
    /// <summary>
    /// This class defines a DTO for completing a challenge.
    /// </summary>
    public class CompleteChallengeDTO
    {
        public int ChallengeID { get; set; }

        public int UserID { get; set; }

        public int Rating { get; set; }

        public string Feedback { get; set; }

        public DateTime CompletedOn { get; set; }
    }
}
