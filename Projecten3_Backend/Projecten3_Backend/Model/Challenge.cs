using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.Model.ManyToMany
{
    public class Challenge
    {
        #region Properties

        public int ChallengeId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }

        #endregion

        #region Constructors
        #endregion
    }
}
