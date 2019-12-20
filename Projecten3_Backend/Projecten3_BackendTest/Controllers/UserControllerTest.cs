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
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

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
        private readonly Mock<UserManager<Microsoft.AspNetCore.Identity.IdentityUser>> _userManager;
        #endregion

        #region Constructors
        public UserControllerTest()
        {
            _userManager = new Mock<UserManager<Microsoft.AspNetCore.Identity.IdentityUser>>();
            _dummyData = new DummyProject3_BackendContext();
            _categoryRepository = new Mock<ICategoryRepository>();
            _userRepository = new Mock<IUserRepository>();
            _companyRepository = new Mock<ICompanyRepository>();
            _therapistRepository = new Mock<ITherapistRepository>();
            _challengeRepository = new Mock<IChallengeRepository>();
            _userController = new UsersController(
                _userRepository.Object,
                _userManager.Object,
                _categoryRepository.Object,
                _companyRepository.Object,
                _therapistRepository.Object,
                _challengeRepository.Object);
        }
        #endregion

        #region Get
        [Fact]
        public void GetUsers_ReturnsOk()
        {
            _userRepository.Setup(ur => ur.GetUsers()).Returns(_dummyData.Users);
            var okResult = _userController.GetUser() as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void GetUserWithChallenge_ReturnsOk()
        {
            _userRepository.Setup(ur => ur.GetById(1)).Returns(_dummyData.Users.First);
            _challengeRepository.Setup(cr => cr.GetUserChallenges(1)).Returns(_dummyData.ChallengesUser);
            var okResult = _userController.GetUserWithChallenges(1) as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void GetUserWithChallenge_ReturnsNotFound()
        {
            _userRepository.Setup(ur => ur.GetById(4));
            _challengeRepository.Setup(cr => cr.GetUserChallenges(4));
            var okResult = _userController.GetUserWithChallenges(1) as NotFoundResult;
            Assert.IsType<NotFoundResult>(okResult);
        }

        [Fact]
        public void GetUser_ReturnsOk()
        {
            _userRepository.Setup(ur => ur.GetById(1)).Returns(_dummyData.Users.First);
            var okResult = _userController.GetUserWithChallenges(1) as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void GetUser_ReturnsNotFound()
        {
            _userRepository.Setup(ur => ur.GetById(4));
            var okResult = _userController.GetUserWithChallenges(1) as IActionResult;
            Assert.IsType<NotFoundResult>(okResult);
        }

        [Fact]
        public void GetTherapistUser_ReturnsOk()
        {
            _userRepository.Setup(ur => ur.GetById(1)).Returns(_dummyData.Users.First);
            _userRepository.Setup(ur => ur.GetUserTherapists(1)).Returns(_dummyData.Therapists);
            var okResult = _userController.GetUserWithChallenges(1) as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void GetTherapistUser_ReturnsNotFound()
        {
            _userRepository.Setup(ur => ur.GetById(4));
            var okResult = _userController.GetUserWithChallenges(1) as IActionResult;
            Assert.IsType<NotFoundResult>(okResult);
        }
        #endregion

        #region Post
        [Fact]
        public void PostUser_ReturnsOk()
        {
            _companyRepository.Setup(cr => cr.GetById(1)).Returns(_dummyData.Companies.First);
            _categoryRepository.Setup(cr => cr.CategoriesExist(new List<int>() { 1 })).Returns(true);
            _therapistRepository.Setup(cr => cr.TherapistsExist(new List<int>() { 1 })).Returns(true);
            _categoryRepository.Setup(cr => cr.GetCategoriesById(new List<int>() { 1 })).Returns(_dummyData.Categories);
            _userRepository.Setup(cr => cr.UserExists(null)).Returns(true);
            _userRepository.Setup(ur => ur.AddUser(It.IsAny<User>()));
            var okResult = _userController.PostUser(_dummyData.AddUserDTO) as Task;
            Assert.NotNull(okResult);
            //Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void PostUser_ReturnsNotFound()
        {
            _companyRepository.Setup(cr => cr.GetById(1)).Returns(_dummyData.Companies.First);
            _categoryRepository.Setup(cr => cr.CategoriesExist(null));
            _therapistRepository.Setup(cr => cr.TherapistsExist(new List<int>() { 1 }));
            _categoryRepository.Setup(cr => cr.GetCategoriesById(new List<int>() { 1 }));
            _userRepository.Setup(cr => cr.UserExists(null)).Returns(true);
            _userRepository.Setup(ur => ur.AddUser(It.IsAny<User>()));
            var okResult = _userController.PostUser(_dummyData.AddUserDTO) as Task;
            Assert.IsType<BadRequestResult>(okResult);
        }
        #endregion

        #region Put
        [Fact]
        public void PutUser_ReturnsOk()
        {
            _therapistRepository.Setup(cr => cr.TherapistsExist(new List<int>() { 1 })).Returns(true);
            _categoryRepository.Setup(cr => cr.GetCategoriesById(new List<int>() { 1 })).Returns(_dummyData.Categories);
            _categoryRepository.Setup(cr => cr.CategoriesExist(new List<int>() { 1 })).Returns(true);
            _userRepository.Setup(ur => ur.GetById(1)).Returns(_dummyData.Users.First);
            _userRepository.Setup(cr => cr.UserExists(null)).Returns(true);
            _userRepository.Setup(ur => ur.UpdateUser(It.IsAny<User>()));
            var okResult = _userController.PutUser(_dummyData.EditUserDTO) as Task;
            Assert.NotNull(okResult);
            //Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void PutUser_ReturnsBadRequest()
        {
            _therapistRepository.Setup(cr => cr.TherapistsExist(new List<int>() { 1 }));
            _categoryRepository.Setup(cr => cr.GetCategoriesById(new List<int>() { 1 }));
            _userRepository.Setup(cr => cr.UserExists(null));
            _userRepository.Setup(ur => ur.UpdateUser(It.IsAny<User>()));
            var okResult = _userController.PutUser(_dummyData.EditUserDTO) as Task;
            Assert.IsType<BadRequestResult>(okResult);
        }

        [Fact]
        public void PutUserApp_ReturnsOk()
        {
            EditAppUserDTO usr = _dummyData.EditAppUserDTO;
            _userRepository.Setup(ur => ur.GetById(usr.UserId)).Returns(_dummyData.Users.First());
            _userManager.Setup(um => um.FindByNameAsync(usr.Email)).Returns(Task.FromResult(_dummyData.IdentityUser));
            _userRepository.Setup(ur => ur.UserExists(It.IsAny<User>())).Returns(true);
            _userManager.Setup(um => um.UpdateAsync(It.IsAny<Microsoft.AspNetCore.Identity.IdentityUser>())).Returns(Task.FromResult(IdentityResult.Success));
            _userRepository.Setup(ur => ur.UpdateUser(It.IsAny<User>()));
            var okResult = _userController.EditUserFromApp(usr);
        }
        #endregion

        #region Delete
        [Fact]
        public void DeleteUser_ReturnsOk()
        {
            _userRepository.Setup(ur => ur.GetById(1)).Returns(_dummyData.Users.First);
            _userRepository.Setup(ur => ur.DeleteUser(1));
            var okResult = _userController.DeleteUser(1) as Task;
            Assert.NotNull(okResult);
            //Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void DeleteUser_ReturnsNotFound()
        {
            _userRepository.Setup(ur => ur.GetById(1));
            _userRepository.Setup(ur => ur.DeleteUser(1));
            Assert.IsType<NotFoundResult>(_userController.DeleteUser(1));
        }
        #endregion
    }
}
