using Projecten3_Backend.Model.ManyToMany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.Model
{
    public class Therapist
    {
        #region Properties

        public int TherapistId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Website { get; set; }

        public string Street { get; set; }

        public string HouseNumber { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public TherapistType TherapistType { get; set; }

        #endregion

        #region Collections

        public ICollection<OpeningTime> OpeningTimes { get; set; } = new List<OpeningTime>();

        public virtual ICollection<TherapistUser> Clients { get; set; } = new List<TherapistUser>();

        #endregion

        #region methods
        public void AddClient(User client) => Clients.Add(new TherapistUser() { Therapist = this, TherapistId = this.TherapistId, User = client, UserId = client.UserId });
        #endregion
    }
}
