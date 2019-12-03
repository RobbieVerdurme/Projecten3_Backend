using Projecten3_Backend.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projecten3_BackendTest.Data
{
    public class DummyProject3_BackendContext
    {
        public Category Category { get; }
        public IEnumerable<User> Users { get; }
        public IEnumerable<Challenge> Challenges { get; }
        public IEnumerable<Therapist> Therapists { get; }
        //Init used data
        public DummyProject3_BackendContext()
        {
            this.Category = new Category { CategoryId = 0, Name = "Afvallen" };
            //Challenge challenge = new Challenge { ChallengeId = 0, Category = }
        }
    }
}
