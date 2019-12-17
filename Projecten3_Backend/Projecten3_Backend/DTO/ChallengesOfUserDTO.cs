using Projecten3_Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.DTO
{
    public class ChallengesOfUserDTO
    {
        public int ChallengeId { get; set; }

        public string Title { get; set; }

        public string ChallengeImage { get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }

        public DateTime? CompletedDate { get; set; }

        public int Level { get; set; }

        //stars in the android app
        public int Rating { get; set; }

        public string Feedback { get; set; }
    }
}
