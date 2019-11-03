using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.Data
{
    /// <summary>
    /// This class defines constants for the user roles.
    /// </summary>
    public class UserRole
    {
        public const string USER = "User";
        public const string MULTIMED = "Multimed";
        public const string THERAPIST = "Therapist";
        //For both multimed and user in Roles of Authorize
        //Roles accepts a comma separated list
        public const string MULTIMED_AND_USER = MULTIMED + "," + USER;
        //For both multimed and therapist in Roles of Authorize
        //Roles accepts a comma separated list
        public const string MULTIMED_AND_THERAPIST = MULTIMED + "," + THERAPIST;
    }
}
