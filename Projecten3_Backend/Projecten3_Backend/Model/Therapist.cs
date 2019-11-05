using Projecten3_Backend.Model.ManyToMany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Projecten3_Backend.Model
{
    public class Therapist
    {
        /// <summary>
        /// This is a regex pattern for an opening hour.
        /// It matches any hour between 0:00 and 23:59 (inclusive).
        /// Hours before 10 o'clock are written without leading zero, thus 9:00 is valid but 09:00 isn't.
        /// </summary>
        private const string OPENING_TIMES_HOUR_PATTERN = @"((1?[0-9]|2[0-3]):[0-5][0-9])";

        #region Properties
        /// <summary>
        /// Regex for opening hours
        /// The pattern starts with a day counter, followed by a single space. Monday = 0, Sunday = 6.
        /// This is followed by one or two intervals of time. When a second interval is present it is preceded by a space.
        /// Each interval follows the pattern:
        /// 
        /// OPENING_TIMES_HOUR_PATTERN - OPENING_TIMES_HOUR_PATTERN
        /// 
        /// (Note the single spaces surrounding the hyphen)
        /// 
        /// Valid formats:
        /// 0 9:00 - 12:00
        /// 1 23:59 - 0:00
        /// 6 12:00 - 23:30
        /// 0 9:00 - 10:00 19:00 - 20:00
        /// 
        /// </summary>
        private static string OPENING_TIMES_REGEX_PATTERN = $"^[0-6] {OPENING_TIMES_HOUR_PATTERN} - {OPENING_TIMES_HOUR_PATTERN}( {OPENING_TIMES_HOUR_PATTERN} - {OPENING_TIMES_HOUR_PATTERN})?$";

        public int TherapistId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Website { get; set; }

        public string Street { get; set; }

        public int HouseNumber { get; set; }

        public int PostalCode { get; set; }

        public string City { get; set; }

        public TherapistType TherapistType { get; set; }

        #endregion

        #region Collections

        public ICollection<OpeningTimes> OpeningTimes { get; set; } = new List<OpeningTimes>();

        public virtual ICollection<TherapistUser> Clients { get; set; } = new List<TherapistUser>();

        #endregion

        #region methods
        public void AddClient(User client) => Clients.Add(new TherapistUser() { Therapist = this, TherapistId = TherapistId, User = client, UserId = client.UserId });

        public static bool HasInvalidOpeningTimes(IList<string> times)
        {
            Regex regex = new Regex(OPENING_TIMES_REGEX_PATTERN);
            if (times == null || times.Count != 7) return true;

            foreach (string time in times) {
                if (time == null || !regex.IsMatch(time)) return true;
            }
            return false;
        }

        public override bool Equals(object obj)
        {
            var therapist = obj as Therapist;
            return therapist != null &&
                   FirstName == therapist.FirstName &&
                   LastName == therapist.LastName &&
                   Email == therapist.Email &&
                   PhoneNumber == therapist.PhoneNumber &&
                   Website == therapist.Website &&
                   Street == therapist.Street &&
                   HouseNumber == therapist.HouseNumber &&
                   PostalCode == therapist.PostalCode &&
                   City == therapist.City &&
                   EqualityComparer<TherapistType>.Default.Equals(TherapistType, therapist.TherapistType);
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(FirstName);
            hash.Add(LastName);
            hash.Add(Email);
            hash.Add(PhoneNumber);
            hash.Add(Website);
            hash.Add(Street);
            hash.Add(HouseNumber);
            hash.Add(PostalCode);
            hash.Add(City);
            hash.Add(TherapistType);
            return hash.ToHashCode();
        }

        public static bool operator ==(Therapist t1, Therapist t2) {
            return t1.City == t2.City && t1.Email == t2.Email && t1.FirstName == t2.FirstName && t1.HouseNumber == t2.HouseNumber && t1.LastName == t2.LastName
                && t1.PhoneNumber == t2.PhoneNumber && t1.PostalCode == t2.PostalCode && t1.Street == t2.Street
                && t1.TherapistType == t2.TherapistType && t1.Website == t2.Website;
        }

        public static bool operator !=(Therapist t1, Therapist t2)
        {
            return t1.City != t2.City && t1.Email != t2.Email && t1.FirstName != t2.FirstName && t1.HouseNumber != t2.HouseNumber && t1.LastName != t2.LastName
                && t1.PhoneNumber != t2.PhoneNumber && t1.PostalCode != t2.PostalCode && t1.Street != t2.Street
                && t1.TherapistType != t2.TherapistType && t1.Website != t2.Website;
        }
        #endregion
    }
}
