﻿using System;
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

        public int CategoryId { get; set; }

        public int Level { get; set; }
    }
}
