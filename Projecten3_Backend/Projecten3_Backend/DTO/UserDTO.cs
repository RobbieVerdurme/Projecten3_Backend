using Projecten3_Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.DTO
{
    /// <summary>
    /// This DTO is the output DTO for a <see cref="User"/>
    /// </summary>
    public class UserDTO
    {
        #region Properties

        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string FamilyName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime Contract { get; set; }

        public ICollection<Category> Categories { get; set; }
        #endregion

        #region methods
        public static User MapUserDTOToUser(UserDTO user)
        {
            if (user != null) {
                User usr = new User()
                {
                    FirstName = user.FirstName,
                    FamilyName = user.FamilyName,
                    Email = user.Email,
                    Categories = user.Categories,
                    Contract = user.Contract
                };

                return usr;
            } else {
                return null;
            }

        }
        #endregion
    }
}
