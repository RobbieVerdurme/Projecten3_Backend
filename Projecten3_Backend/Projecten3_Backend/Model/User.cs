using Projecten3_Backend.DTO;
using Projecten3_Backend.Model.ManyToMany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.Model
{
    public class User
    {
        #region Properties

        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string FamilyName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        #endregion

        #region Collections

        public ICollection<Challenge> Challenges { get; set; } = new List<Challenge>();

        public virtual ICollection<TherapistUser> Therapists { get; set; } = new List<TherapistUser>();

        public ICollection<Category> Categories { get; set; } = new List<Category>();

        #endregion

        #region Methods

        public void AddChallenge(Challenge challenge) => Challenges.Add(new Challenge() { Title = challenge.Title, Description = challenge.Description, Category = challenge.Category });

        public void AddTherapist(Therapist therapist) => Therapists.Add(new TherapistUser() { Therapist = therapist, TherapistId = therapist.TherapistId, User = this, UserId = this.UserId});
        //public void AddTherapist(Therapist therapist) => Therapists.Add(new Therapist() {FirstName = therapist.FirstName, LastName = therapist.LastName, Email = therapist.Email, PhoneNumber = therapist.PhoneNumber,Website = therapist.Website,Street = therapist.Street, HouseNumber =  therapist.HouseNumber,PostalCode =  therapist.PostalCode,City =  therapist.City, TherapistType = therapist.TherapistType });

        public static UserDTO MapUserToUserDTO(User usr)
        {
            UserDTO user = new UserDTO()
            {
                FirstName = usr.FirstName,
                FamilyName = usr.FamilyName,
                Email = usr.Email,
                Categories = usr.Categories
            };

            return user;
        }

        #endregion
    }
}
