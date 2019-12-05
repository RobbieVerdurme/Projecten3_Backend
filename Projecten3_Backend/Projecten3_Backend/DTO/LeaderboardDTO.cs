using System;
namespace Projecten3_Backend.DTO
{
    public class LeaderboardDTO
    {
            #region Properties

            public int UserId { get; set; }

            public string FirstName { get; set; }

            public string FamilyName { get; set; }

            public int Score { get; set; }
            #endregion
    }
}
