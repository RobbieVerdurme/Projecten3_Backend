using Projecten3_Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.DTO
{
    public class CompanyDTO
    {
        #region Properties
        public int CompanyId { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Mail { get; set; }

        public string Street { get; set; }

        public int HouseNumber { get; set; }

        public string City { get; set; }

        public int PostalCode { get; set; }

        public string Country { get; set; }

        public string Site { get; set; }

        public DateTime Contract { get; set; }
        #endregion

        #region Collections
        public IEnumerable<UserDTO> CompanyMembers { get; set; } = new List<UserDTO>();
        #endregion

        #region methods
        public static Company MapCompanyDTOToCompany(CompanyDTO cmp)
        {
            if(cmp != null)
            {
                Company newcmp = new Company()
                {
                    CompanyId = cmp.CompanyId,
                    Name = cmp.Name,
                    Phone = cmp.Phone,
                    Mail = cmp.Mail,
                    Street = cmp.Street,
                    HouseNumber = cmp.HouseNumber,
                    City = cmp.City,
                    PostalCode = cmp.PostalCode,
                    Country = cmp.Country,
                    Site = cmp.Site,
                    Contract = cmp.Contract
                };
                newcmp.CompanyMembers = cmp.CompanyMembers.Select( u => UserDTO.MapUserDTOToUser(u));
                return newcmp;
            } else {
                return null;
                    };
        }
        #endregion
    }
}
