using Projecten3_Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.DTO
{
    public class TherapistDTO
    {

        #region properties
        public int TherapistId { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }
        #endregion
    }
}
