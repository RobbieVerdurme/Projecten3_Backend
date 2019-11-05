using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.DTO
{
    /// <summary>
    /// This class is the DTO for adding a new user.
    /// </summary>
    public class AddUserDTO
    {
        public string FirstName { get; set; }

        public string FamilyName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int Company { get; set; }

        public IList<int> Categories { get; set; }

        public IList<int> Therapists { get; set; }
    }
}
