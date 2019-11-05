using Microsoft.EntityFrameworkCore;
using Projecten3_Backend.Data.IRepository;
using Projecten3_Backend.Model;
using Projecten3_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projecten3_Backend.Data.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        #region  prop
        private readonly Projecten3_BackendContext _dbContext;
        private readonly DbSet<Company> _companies;
        #endregion

        #region ctor
        public CompanyRepository(Projecten3_BackendContext dbContex)
        {
            _dbContext = dbContex;
            _companies = _dbContext.Company;
        }
        #endregion

        public void AddCompany(Company company)
        {
            _companies.Add(company);
        }

        public bool CompanyExists(Company company)
        {
            return _dbContext.Company.Where(c => c == company).FirstOrDefault() != null;
        }

        public void DeleteCompany(Company company)
        {
            _companies.Remove(company);
        }

        public IEnumerable<Company> GetAll()
        {
            return _companies.Include(c => c.CompanyMembers);
        }

        public Company GetById(int id)
        {
            return _companies.Include(c => c.CompanyMembers).FirstOrDefault(c => c.CompanyId == id);
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void UpdateCompany(Company company)
        {
            _companies.Update(company);
        }
    }
}
