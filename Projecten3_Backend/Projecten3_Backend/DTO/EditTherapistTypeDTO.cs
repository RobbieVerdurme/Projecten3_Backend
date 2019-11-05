using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.DTO
{
    public class EditTherapistTypeDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public IList<int> Categories { get; set; }
    }
}
