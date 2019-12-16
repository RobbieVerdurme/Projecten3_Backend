using Projecten3_Backend.DTO;
using Projecten3_Backend.Model;
using Projecten3_Backend.Model.ManyToMany;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projecten3_BackendTest.Data
{
    public class DummyProject3_BackendContext
    {
        #region Properties
        // Collections
        public IEnumerable<Category> Categories { get; }
        public IEnumerable<User> Users { get; }
        public IEnumerable<Challenge> Challenges { get; }
        public IEnumerable<ChallengeUser> ChallengesUser { get; }
        public IEnumerable<Therapist> Therapists { get; }
        public IEnumerable<TherapistUser> TherapistUser { get; }
        public IEnumerable<Company> Companies { get; }
        public IEnumerable<TherapistType> TherapistTypes { get; }
        public IEnumerable<OpeningTimes> OpeningTimes { get; }

        //DTO's
        public AddUserDTO AddUserDTO { get; }
        public EditUserDTO EditUserDTO { get; }
        public AddTherapistDTO AddTherapistDTO { get; }
        public AddTherapistTypeDTO AddTherapistTypeDTO { get; }
        public EditTherapistDTO EditTherapistDTO { get; }
        public EditTherapistTypeDTO EditTherapistTypeDTO { get; }
        public AddCompanyDTO AddCompanyDTO { get; }
        public EditCompanyDTO EditCompanyDTO { get; }
        public AddChallengeDTO AddChallengeDTO { get; }
        public CompleteChallengeDTO CompleteChallengeDTO { get; }

        public static DateTime CompleteChallengeDate = DateTime.Now;
        public ChallengesUserDTO ChallengesUserDTO { get; }

        //Objects
        public Category Category { get; }
        #endregion

        //Init used data
        public DummyProject3_BackendContext()
        {
            // Create objects
            Category category1 = new Category { CategoryId = 0, Name = "Afvallen" };

            Challenge challenge1 = new Challenge { ChallengeId = 0, ChallengeImage = "Image url", Description = "Loop 1500 meter", Title = "First run" };
            Challenge challenge2 = new Challenge { ChallengeId = 1, ChallengeImage = "Image url", Description = "Loop 2000 meter", Title = "Second run" };

            Company company1 = new Company { CompanyId = 0, City = "Wetteren", Contract = new DateTime(2020, 05, 15), Country = "België", HouseNumber = 3, Mail = "ruben.grillaert.y1033@student.hogent.be", Name = "Xenox", Phone = "0474139526", PostalCode = 9230, Site = "www.google.com", Street = "Kalkensteenweg" };

            User user1 = new User { UserId = 0, Phone = "0473139526", Contract = new DateTime(2020, 05, 15), Email = "ruben.grillaert@hotmail.com", ExperiencePoints = 0, FamilyName = "Grillaert", FirstName = "Ruben" };
            User user2 = new User { UserId = 1, Phone = "0412345678", Contract = new DateTime(2020, 05, 15), Email = "test.m@ail.com", ExperiencePoints = 0, FamilyName = "Boel", FirstName = "Arno" };

            Therapist therapist1 = new Therapist { TherapistId = 0, City = "stad", Email = "em@il.com", FirstName = "Thor", HouseNumber = 2, LastName = "Krets", PhoneNumber = "093661686", PostalCode = 9000, Street = "straat", Website = "www.test.be" };

            OpeningTimes openingTimes1 = new OpeningTimes { OpeningTimesId = 0, Interval = "09:00 - 18:30" };
            OpeningTimes openingTimes2 = new OpeningTimes { OpeningTimesId = 1, Interval = "09:00 - 18:30" };

            TherapistType therapistType1 = new TherapistType { TherapistTypeId = 0, Type = "therapisttype",  };

            // Create many to many objects
            ChallengeUser challengeUser1 = new ChallengeUser { Challenge = challenge1, ChallengeId = challenge1.ChallengeId, ChallengeUserId = 0, User = user1, UserId = user1.UserId };
            ChallengeUser challengeUser2 = new ChallengeUser { Challenge = challenge2, ChallengeId = challenge2.ChallengeId, ChallengeUserId = 1, User = user1, UserId = user1.UserId };

            TherapistUser therapistUser1 = new TherapistUser { TherapistUserId = 0, Therapist = therapist1, TherapistId = therapist1.TherapistId, User = user1, UserId = user1.UserId };
            TherapistUser therapistUser2 = new TherapistUser { TherapistUserId = 1, Therapist = therapist1, TherapistId = therapist1.TherapistId, User = user2, UserId = user2.UserId };
            // Set connections between objects
            challenge1.Category = category1;
            challenge2.Category = category1;

            company1.CompanyMembers = new List<User>() { user1, user2 };

            therapist1.OpeningTimes = new List<OpeningTimes>() { openingTimes1 };
            therapist1.TherapistType = therapistType1;
            therapistType1.Categories = new List<Category>() { category1 };

            user1.Categories = new List<Category>() { category1 };
            user2.Categories = new List<Category>() { category1 };
            user1.Challenges = new List<ChallengeUser>() { challengeUser1, challengeUser2 };
            user2.Challenges = new List<ChallengeUser>();
            user1.Company = company1;
            user2.Company = company1;
            user1.Therapists = new List<TherapistUser>() { therapistUser1 };
            user2.Therapists = new List<TherapistUser>() { therapistUser2 };

            // Init properties
            Category = category1;
            Categories = new[] { category1 };
            Companies = new[] { company1 };
            Challenges = new[] { challenge1, challenge2 };
            Therapists = new[] { therapist1 };
            Users = new[] { user1, user2 };
            ChallengesUser = new[] { challengeUser1, challengeUser2 };
            TherapistTypes = new[] { therapistType1 };
            OpeningTimes = new[] { openingTimes1, openingTimes2, openingTimes2, openingTimes2, openingTimes2, openingTimes2, openingTimes2 };

            // Create DTO's
            AddUserDTO = new AddUserDTO { Categories = new List<int>() { 1 }, Company = 1, Email = "mail@mailto.com", FamilyName = "test", FirstName = "test", Phone = "0471236548", Therapists = new List<int>() { 1 } };
            EditUserDTO = new EditUserDTO { Categories = new List<int>() { 1 }, Email = "mail@mailto.com", FamilyName = "test", FirstName = "test", Phone = "0471236548", UserId = 1, Contract = new DateTime()};
            AddTherapistDTO = new AddTherapistDTO { City = "test", Email = "test", FirstName = "test", HouseNumber = 1, LastName = "test", PhoneNumber = "test", PostalCode = 9000, Street = "test", TherapistTypeId = 0, Website = "test" };
            AddTherapistTypeDTO = new AddTherapistTypeDTO { Categories = new List<int>() { 1 }, Type = "test" };
            EditTherapistDTO = new EditTherapistDTO { City = "test", Email = "test", FirstName = "test", HouseNumber = 1, LastName = "test", PhoneNumber = "test", PostalCode = 5000, Street = "test", TherapistId = 0, Website = "test", TherapistTypeId = 0, Clients = new List<int>() { 1 }, OpeningTimes = new List<string>() { "test1", "test1", "test1", "test1", "test1", "test1", "test1" } };
            EditTherapistTypeDTO = new EditTherapistTypeDTO { Id = 0, Categories = new List<int>() { 0 }, Type = "therapisttype" };
            AddCompanyDTO = new AddCompanyDTO { City = "test", Contract = new DateTime(), Country = "test", HouseNumber = 3, Mail = "test", Name = "test", Phone = "test", PostalCode = 9000, Site = "test", Street = "test" };
            EditCompanyDTO = new EditCompanyDTO { City = "test", Contract = new DateTime(), Country = "test", HouseNumber = 3, Mail = "test", Name = "test", Phone = "test", PostalCode = 9000, Site = "test", Street = "test", CompanyId = 0, CompanyMembers = new List<User>() { user1, user2 } };
            AddChallengeDTO = new AddChallengeDTO { CategoryId = 0, ChallengeImage = "test", Description = "test", Title = "test" };
            CompleteChallengeDTO = new CompleteChallengeDTO { ChallengeID = 0, UserID = 0, Rating = 3, Feedback = "test", CompletedOn = CompleteChallengeDate };
            ChallengesUserDTO = new ChallengesUserDTO { ChallengeIds = new List<int>() { 0 }, UserId = 0 };
        }
    }
}
