using MultimedAPI.Models.ManyToManies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultimedAPI.Models
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

        public ICollection<OpeningTime> OpeningTimes { get; set; }

        public ICollection<TherapistUser> TherapistUsers { get; set; }

        #endregion

        #region Constructors

        public Therapist()
        {
            OpeningTimes = new List<OpeningTime>();
            TherapistUsers = new List<TherapistUser>();
        }

        public Therapist(string firstName, string lastName, string email, string phoneNumber, string website, string street, string houseNumber, string postalCode, string city, TherapistType therapistType)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Website = website;
            Street = street;
            HouseNumber = houseNumber;
            PostalCode = postalCode;
            City = city;
            TherapistType = TherapistType;
        }

        #endregion

        #region Methods



        #endregion
    }
}
