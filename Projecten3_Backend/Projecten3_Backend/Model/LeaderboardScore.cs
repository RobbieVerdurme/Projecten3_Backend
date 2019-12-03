using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.Model
{
    public class LeaderboardScore
    {
        #region Properties
        public DateTime Date { get; set; }
        public int Score { get; set; } = 0;
        #endregion

        #region Methods
        public void RaiseScore()
        {
            Score++;
        }
            
        #endregion
    }
}
