using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Projecten3_Backend.DTO;

namespace Projecten3_Backend.Model
{
    public class LeaderboardScore
    {        
        #region Properties
        public DateTime Date { get; set; }
        public int Score { get; set; } = 0;
        #endregion

        #region Constructor
        public LeaderboardScore(DateTime now, int score)
        {
            this.Date = now;
            this.Score = score;
        }
        #endregion

        #region Methods
        public void RaiseScore()
        {
            Score++;
        }
        #endregion

    }
}
