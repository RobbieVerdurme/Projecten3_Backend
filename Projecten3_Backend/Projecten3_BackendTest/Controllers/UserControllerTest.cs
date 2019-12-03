using Microsoft.AspNetCore.Mvc;
using Moq;
using Projecten3_Backend.Controllers;
using Projecten3_Backend.Data.IRepository;
using Projecten3_BackendTest.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using Projecten3_Backend.Model;
using Projecten3_Backend.DTO;
using System.Net;

namespace Projecten3_BackendTest.Controllers
{
    public class UserControllerTest : ControllerBase
    {
        #region Properties
        private readonly UsersController _userController;
        private readonly Mock<ICategoryRepository> _categoryRepository;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<ICompanyRepository> _companyRepository;
        private readonly Mock<ITherapistRepository> _therapistRepository;
        private readonly Mock<IChallengeRepository> _challengeRepository;
        private readonly DummyProject3_BackendContext _dummyData;
        #endregion

        public UserControllerTest()
        {
            _dummyData = new DummyProject3_BackendContext();
            _categoryRepository = new Mock<ICategoryRepository>();
            _userRepository = new Mock<IUserRepository>();
            _companyRepository = new Mock<ICompanyRepository>();
            _therapistRepository = new Mock<ITherapistRepository>();
            _challengeRepository = new Mock<IChallengeRepository>();
            _userController = new UsersController(
                _userRepository.Object,
                _categoryRepository.Object,
                _companyRepository.Object,
                _therapistRepository.Object,
                _challengeRepository.Object);
        }
        #region Get
        [Fact]
        public void GetUser_ReturnsOk()
        {
            _userRepository.Setup(ur => ur.GetUsers()).Returns(_dummyData.Users);
            var okResult = _userController.GetUser() as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
        #endregion
        #region Post

        #endregion
    }
}
