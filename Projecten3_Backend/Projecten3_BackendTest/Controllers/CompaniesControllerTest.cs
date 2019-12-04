using Moq;
using Projecten3_Backend.Controllers;
using Projecten3_Backend.Data.IRepository;
using Projecten3_BackendTest.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using Projecten3_Backend.Model;
using Projecten3_Backend.DTO;

namespace Projecten3_BackendTest.Controllers
{
    public class CompaniesControllerTest : ControllerBase
    {
        #region Properties
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<ICompanyRepository> _companyRepository;
        private readonly DummyProject3_BackendContext _dummyData;
        private readonly CompaniesController _companiesController;
        #endregion

        #region Constructor
        public CompaniesControllerTest()
        {
            _dummyData = new DummyProject3_BackendContext();
            _companyRepository = new Mock<ICompanyRepository>();
            _userRepository = new Mock<IUserRepository>();
            _companiesController = new CompaniesController(_companyRepository.Object, _userRepository.Object);
        }
        #endregion

        #region Get
        [Fact]
        public void GetCompanies_ReturnsOk()
        {
            _companyRepository.Setup(cr => cr.GetAll()).Returns(_dummyData.Companies);
            var okResult = _companiesController.GetCompany() as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void GetCompany_ReturnsOk()
        {
            _companyRepository.Setup(cr => cr.GetById(1)).Returns(_dummyData.Companies.First);
            var okResult = _companiesController.GetCompany(1) as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void GetCompany_ReturnsNotFound()
        {
            _companyRepository.Setup(cr => cr.GetById(1));
            var okResult = _companiesController.GetCompany(1) as NotFoundResult;
            Assert.IsType<NotFoundResult>(okResult);
        }
        #endregion

        #region Post
        [Fact]
        public void AddCompany_ReturnsOk()
        {
            AddCompanyDTO addCompanyDTO = _dummyData.AddCompanyDTO;
            _companyRepository.Setup(cr => cr.CompanyExists(Company.MapAddCompanyDTOToCompany(addCompanyDTO))).Returns(false);
            _companyRepository.Setup(cr => cr.AddCompany(It.IsAny<Company>()));
            var okResult = _companiesController.AddCompany(addCompanyDTO) as OkResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

        }

        [Fact]
        public void AddCompany_ReturnsBadRequest()
        {
            _companyRepository.Setup(cr => cr.CompanyExists(It.IsAny<Company>())).Returns(true);
            var okResult = _companiesController.AddCompany(_dummyData.AddCompanyDTO) as StatusCodeResult;
            Assert.IsType<StatusCodeResult>(okResult);
            Assert.Equal(303, okResult.StatusCode);

        }
        #endregion

        #region Put
        [Fact]
        public void EditCompany_ReturnsOk()
        {
            _companyRepository.Setup(cr => cr.GetById(0)).Returns(_dummyData.Companies.First);
            _companyRepository.Setup(cr => cr.UpdateCompany(It.IsAny<Company>()));
            var okResult = _companiesController.EditCompany(_dummyData.EditCompanyDTO) as OkResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void EditCompany_ReturnsBadRequest()
        {
            _companyRepository.Setup(cr => cr.GetById(0));
            _companyRepository.Setup(cr => cr.UpdateCompany(It.IsAny<Company>()));
            var okResult = _companiesController.EditCompany(_dummyData.EditCompanyDTO) as StatusCodeResult;
            Assert.IsType<StatusCodeResult>(okResult);
            Assert.Equal(404, okResult.StatusCode);
        }
        #endregion

        #region Delete
        [Fact]
        public void DeleteCompany_ReturnsOk()
        {
            _companyRepository.Setup(cr => cr.GetById(0)).Returns(_dummyData.Companies.First);
            _companyRepository.Setup(cr => cr.DeleteCompany(0));
            var okResult = _companiesController.DeleteCompany(0) as OkResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void DeleteCompany_ReturnsNotFound()
        {
            var okResult = _companiesController.DeleteCompany(0) as NotFoundResult;
            Assert.IsType<NotFoundResult>(okResult);
        }
        #endregion
    }
}
