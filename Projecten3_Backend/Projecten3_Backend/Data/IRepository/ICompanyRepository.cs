using Projecten3_Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.Data.IRepository
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetAll();

        Company GetById(int id);

        void AddCompany(Company company);

        bool CompanyExists(Company company);

        void UpdateCompany(Company company);

        void DeleteCompany(int id);

        void SaveChanges();
        void AddEmployees(Company company, IList<int> employees);
    }
}
