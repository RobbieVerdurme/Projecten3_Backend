using Projecten3_Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.DTO
{
    public class AddChallengeDTO
    {
        public string Title { get; set; }

        public string ChallengeImage { get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }
    }
}
