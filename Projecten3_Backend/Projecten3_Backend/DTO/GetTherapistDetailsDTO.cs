﻿using Projecten3_Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.DTO
{
    public class GetTherapistDetailsDTO
    {
        public int TherapistId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Website { get; set; }

        public string Street { get; set; }

        public int HouseNumber { get; set; }

        public int PostalCode { get; set; }

        public string City { get; set; }

        public TherapistType TherapistType { get; set; }

        public ICollection<OpeningTimes> OpeningTimes { get; set; }

        public ICollection<UserDTO> Clients { get; set; }
    }
}
