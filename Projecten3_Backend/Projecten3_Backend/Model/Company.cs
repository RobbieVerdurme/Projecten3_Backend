﻿using Projecten3_Backend.DTO;
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

        public int HouseNumber { get; set; }

        public string City { get; set; }

        public int PostalCode { get; set; }

        public string Country { get; set; }

        public string Site { get; set; }

        public DateTime Contract { get; set; }
        #endregion

        #region Collections
        public IEnumerable<User> CompanyMembers { get; set; } = new List<User>();
        #endregion

        #region methods
        public static CompanyDTO MapCompanyToCompanyDTO(Company cmp)
        {
            if (cmp != null)
            {
                CompanyDTO newcmp = new CompanyDTO()
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
                newcmp.CompanyMembers = cmp.CompanyMembers.Select(u => User.MapUserToUserDTO(u));
                return newcmp;
            }
            else
            {
                return null;
            };
        }
        #endregion
    }
}
