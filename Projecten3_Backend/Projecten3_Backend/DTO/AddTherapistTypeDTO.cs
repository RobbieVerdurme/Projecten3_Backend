using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.DTO
{
    public class AddTherapistTypeDTO
    {
        public string Type { get; set; }

        public IList<int> Categories { get; set; }
    }
}
