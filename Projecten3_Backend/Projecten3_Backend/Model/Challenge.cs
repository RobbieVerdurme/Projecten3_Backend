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

        public static bool operator ==(Challenge c1, Challenge c2) {
            if (c1 == null && c2 == null) return true;
            if (c1 != null && c2 == null || c1 == null && c2 != null) return false;
            return c1.Category == c2.Category && c1.Description == c2.Description && c1.Title == c2.Title;
        }

        public static bool operator !=(Challenge c1, Challenge c2)
        {
            if (c1 == null && c2 == null) return false;
            if (c1 != null && c2 == null || c1 == null && c2 != null) return true;
            return c1.Category != c2.Category && c1.Description != c2.Description && c1.Title != c2.Title;
        }
    }
}
