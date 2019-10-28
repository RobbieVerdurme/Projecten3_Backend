using MultimedAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultimedAPI.DTOs
{
    public class UserDTO
    {

        #region Properties

        public string FirstName { get; set; }

        public string FamilyName { get; set; }

        public string Email { get; set; }

        public ICollection<ChallengeDTO> Challenges { get; set; }

        public ICollection<Category> Categories { get; set; }
        #endregion
    }
}
