using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.Model
{
    public class Company
    {
        #region Properties
        public int CompanyId { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Mail { get; set; }

        public string Street { get; set; }

        public string HouseNumber { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public string Site { get; set; }
        #endregion

        #region Collections
        public ICollection<User> CompanyMembers { get; set; } = new List<User>();
        #endregion

        #region Methodes
        public void AddCompanyMember(User user) => CompanyMembers.Add(user);
        #endregion
    }
}
