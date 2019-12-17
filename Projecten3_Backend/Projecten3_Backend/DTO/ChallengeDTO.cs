using Projecten3_Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.DTO
{
    public class ChallengeDTO
    {
        public int ChallengeId { get; set; }

        public string Title { get; set; }

        public string ChallengeImage { get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }

        public int Level { get; set; }


        public ChallengeDTO(Challenge challenge)
        {
            ChallengeId = challenge.ChallengeId;
            Title = challenge.Title;
            ChallengeImage = challenge.ChallengeImage;
            Description = challenge.Description;
            Category = challenge.Category;
            Level = challenge.Level;
        }
    }
}
