using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.DTO
{
    /// <summary>
    /// This class is the DTO for editing a <see cref="Model.User"/>
    /// </summary>
    public class EditUserDTO
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string FamilyName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime Contract { get; set; }

        public IList<int> Categories { get; set; }
    }
}
