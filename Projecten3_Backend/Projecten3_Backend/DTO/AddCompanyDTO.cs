using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.DTO
{
    public class AddCompanyDTO
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Mail { get; set; }

        public string Street { get; set; }

        public int HouseNumber { get; set; }

        public string City { get; set; }

        public int PostalCode { get; set; }

        public string Country { get; set; }

        public string Site { get; set; }
    }
}
