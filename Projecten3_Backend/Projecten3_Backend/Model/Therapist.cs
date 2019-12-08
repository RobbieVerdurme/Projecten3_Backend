using Projecten3_Backend.DTO;
using Projecten3_Backend.Model.ManyToMany;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ICollection<User> ClientList => Clients.Select(cl => cl.User).ToList();

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

        public static TherapistDTO MapTherapistToTherapistDTO(Therapist therapist)
        {
            TherapistDTO thDTO = new TherapistDTO()
            {
                Email = therapist.Email,
                Firstname = therapist.FirstName,
                Lastname = therapist.LastName,
                TherapistId = therapist.TherapistId
            };
            return thDTO;
        }

        public static Therapist MapAddTherapistDTOToTherapist(AddTherapistDTO therapist, TherapistType type)
        {
            return new Therapist
            {
                City = therapist.City,
                Email = therapist.Email,
                FirstName = therapist.FirstName,
                HouseNumber = therapist.HouseNumber,
                LastName = therapist.LastName,
                PhoneNumber = therapist.PhoneNumber,
                PostalCode = therapist.PostalCode,
                Street = therapist.Street,
                TherapistType = type,
                Website = therapist.Website
            };
        }

        public static Therapist MapEditTherapistDTOToTherapist(EditTherapistDTO therapist, Therapist edited, List<OpeningTimes> openingTimes, ICollection<TherapistUser> therapistUsers)
        {
            edited.City = therapist.City;
            edited.Clients = therapistUsers;
            edited.Email = therapist.Email;
            edited.FirstName = therapist.FirstName;
            edited.HouseNumber = therapist.HouseNumber;
            edited.LastName = therapist.LastName;
            edited.OpeningTimes = openingTimes;
            edited.PhoneNumber = therapist.PhoneNumber;
            edited.PostalCode = therapist.PostalCode;
            edited.Street = therapist.Street;
            edited.Website = therapist.Website;
            return edited;
        }

        public static GetTherapistDetailsDTO MapTherapistToGetTherapistDetailsDTO(Therapist therapist)
        {

            return new GetTherapistDetailsDTO
            {
                City = therapist.City,
                Clients = new List<UserDTO>(therapist.ClientList.Select(client => User.MapUserToUserDTO(client)).ToList()),
                Email = therapist.Email,
                FirstName = therapist.FirstName,
                HouseNumber = therapist.HouseNumber,
                LastName = therapist.LastName,
                OpeningTimes = therapist.OpeningTimes,
                PhoneNumber = therapist.PhoneNumber,
                PostalCode = therapist.PostalCode,
                Street = therapist.Street,
                TherapistId = therapist.TherapistId,
                TherapistType = therapist.TherapistType,
                Website = therapist.Website
            };
        }
        #endregion
    }
}
