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
        #endregion

        #region Collections
        public ICollection<User> CompanyMembers { get; set; } = new List<User>();
        #endregion

        #region Methodes

        public override bool Equals(object obj)
        {
            var company = obj as Company;
            return company != null &&
                   Name == company.Name &&
                   Phone == company.Phone &&
                   Mail == company.Mail &&
                   Street == company.Street &&
                   HouseNumber == company.HouseNumber &&
                   City == company.City &&
                   PostalCode == company.PostalCode &&
                   Country == company.Country &&
                   Site == company.Site;
        }

        public override int GetHashCode()
        {
            var hash = new HashCode();
            hash.Add(Name);
            hash.Add(Phone);
            hash.Add(Mail);
            hash.Add(Street);
            hash.Add(HouseNumber);
            hash.Add(City);
            hash.Add(PostalCode);
            hash.Add(Country);
            hash.Add(Site);
            return hash.ToHashCode();
        }

        public static bool operator ==(Company c1, Company c2) {
            return c1.City == c2.City && c1.Country == c2.Country && c1.HouseNumber == c2.HouseNumber && c1.Mail == c2.Mail && c1.Name == c2.Name && c1.Phone == c2.Phone
                && c1.PostalCode == c2.PostalCode && c1.Site == c2.Site && c1.Street == c2.Street;
        }

        public static bool operator !=(Company c1, Company c2)
        {
            return  c1.City != c2.City && c1.Country != c2.Country && c1.HouseNumber != c2.HouseNumber && c1.Mail != c2.Mail && c1.Name != c2.Name && c1.Phone != c2.Phone
                && c1.PostalCode != c2.PostalCode && c1.Site != c2.Site && c1.Street != c2.Street;
        }
        #endregion
    }
}
