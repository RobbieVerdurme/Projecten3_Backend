using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.DTO
{
    public class AddEmployeesDTO
    {
        public int CompanyId { get; set; }

        public IList<int> Employees { get; set; }
    }
}
