using Projecten3_Backend.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.Model
{
    public class Challenge
    {
        #region Properties

        public int ChallengeId { get; set; }

        public string Title { get; set; }

        public string ChallengeImage { get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }

        public override bool Equals(object obj)
        {
            var challenge = obj as Challenge;
            return challenge != null &&
                   Title == challenge.Title &&
                   Description == challenge.Description &&
                   EqualityComparer<Category>.Default.Equals(Category, challenge.Category);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Title, Description, Category);
        }

        #endregion

        #region Methodes
        public static Challenge MapAddChallengeDTOToChallenge(AddChallengeDTO addChallengeDTO)
        {
            return new Challenge()
            {
                Title = addChallengeDTO.Title,
                Description = addChallengeDTO.Description,
                ChallengeImage = addChallengeDTO.ChallengeImage
            };
        }
        #endregion
    }
}
