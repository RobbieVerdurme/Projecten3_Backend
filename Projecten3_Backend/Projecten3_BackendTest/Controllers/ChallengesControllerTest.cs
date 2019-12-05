using Moq;
using Projecten3_Backend.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using Projecten3_Backend.Data.IRepository;
using Projecten3_BackendTest.Data;
using Microsoft.AspNetCore.Mvc;
using Projecten3_Backend.DTO;
using Projecten3_Backend.Model;
using Projecten3_Backend.Model.ManyToMany;

namespace Projecten3_BackendTest.Controllers
{
    public class ChallengesControllerTest : ControllerBase
    {
        #region Properties
        private readonly ChallengesController _challengesController;
        private readonly Mock<IChallengeRepository> _challengeRepository;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<ICategoryRepository> _categoryRepository;
        private readonly DummyProject3_BackendContext _dummyData;
        #endregion

        #region Constructors
        public ChallengesControllerTest()
        {
            _dummyData = new DummyProject3_BackendContext();
            _challengeRepository = new Mock<IChallengeRepository>();
            _userRepository = new Mock<IUserRepository>();
            _categoryRepository = new Mock<ICategoryRepository>();
            _challengesController = new ChallengesController(
                _challengeRepository.Object,
                _userRepository.Object,
                _categoryRepository.Object);
        }
        #endregion

        #region Get
        [Fact]
        public void GetChallenges_ReturnOk()
        {
            _challengeRepository.Setup(cr => cr.GetChallenges()).Returns(_dummyData.Challenges);
            var okResult = _challengesController.GetChallenges() as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void GetUserChallenges_ReturnOk()
        {
            _userRepository.Setup(ur => ur.GetById(0)).Returns(_dummyData.Users.First);
            _challengeRepository.Setup(cr => cr.GetUserChallenges(0)).Returns(_dummyData.ChallengesUser);
            var okResult = _challengesController.GetUserChallenges(0) as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void GetUserChallenges_ReturnBadRequest()
        {
            _userRepository.Setup(ur => ur.GetById(0));
            var okResult = _challengesController.GetUserChallenges(1) as BadRequestResult;
            Assert.IsType<BadRequestResult>(okResult);
        }

        [Fact]
        public void GetChallengesForUserCategories_ReturnsOk()
        {
            _userRepository.Setup(ur => ur.GetById(0)).Returns(_dummyData.Users.First);
            _challengeRepository.Setup(cr => cr.GetChallenges()).Returns(_dummyData.Challenges);
            var okResult = _challengesController.GetChallengesForUserCategories(0) as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void GetChallengesForUserCategories_ReturnsBadRequest()
        {
            _userRepository.Setup(ur => ur.GetById(0));
            var okResult = _challengesController.GetChallengesForUserCategories(1) as BadRequestResult;
            Assert.IsType<BadRequestResult>(okResult);
        }
        #endregion

        #region Post
        [Fact]
        public void AddChallengesToUser_ReturnsOk()
        {
            List<int> chal = new List<int>() { 0 };
            _userRepository.Setup(ur => ur.GetById(0)).Returns(_dummyData.Users.First);
            _challengeRepository.Setup(ch => ch.ChallengesExist(chal)).Returns(true);
            _challengeRepository.Setup(ch => ch.AddChallengesToUser(0, chal));
            var okResult = _challengesController.AddChallengesToUser(_dummyData.ChallengesUserDTO) as OkResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void AddChallengesToUser_ReturnsBadRequest()
        {
            List<int> chal = new List<int>() { 0 };
            _userRepository.Setup(ur => ur.GetById(0));
            var okResult = _challengesController.AddChallengesToUser(_dummyData.ChallengesUserDTO) as BadRequestResult;
            Assert.IsType<BadRequestResult>(okResult);
        }

        [Fact]
        public void AddChallenge_ReturnsOk()
        {
            _categoryRepository.Setup(cr => cr.GetById(_dummyData.AddChallengeDTO.CategoryId)).Returns(_dummyData.Categories.First);
            _challengeRepository.Setup(cr => cr.ChallengeExists(It.IsAny<Challenge>())).Returns(false);
            _challengeRepository.Setup(cr => cr.AddChallenge(It.IsAny<Challenge>()));
            var okResult = _challengesController.AddChallenge(_dummyData.AddChallengeDTO) as OkResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void AddChallenge_ReturnsBadRequest()
        {
            _categoryRepository.Setup(cr => cr.GetById(_dummyData.AddChallengeDTO.CategoryId));
            var okResult = _challengesController.AddChallenge(_dummyData.AddChallengeDTO) as BadRequestResult;
            Assert.IsType<BadRequestResult>(okResult);
        }

        [Fact]
        public void CompleteChallenge_ReturnsOk()
        {
            CompleteChallengeDTO completeChallengeDTO = _dummyData.CompleteChallengeDTO;
            _userRepository.Setup(ur => ur.GetById(0)).Returns(_dummyData.Users.First);
            _challengeRepository.Setup(cr => cr.GetUserChallenge(completeChallengeDTO.UserID, completeChallengeDTO.ChallengeID)).Returns(_dummyData.ChallengesUser.First);
            _userRepository.Setup(ur => ur.AddExp(It.IsAny<User>()));
            _challengeRepository.Setup(cr => cr.CompleteChallenge(It.IsAny<ChallengeUser>()));
            var okResult = _challengesController.CompleteChallenge(completeChallengeDTO) as OkResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void CompleteChallenge_ReturnsBadRequest()
        {
            _userRepository.Setup(ur => ur.GetById(0));
            var okResult = _challengesController.CompleteChallenge(_dummyData.CompleteChallengeDTO) as BadRequestResult;
            Assert.IsType<BadRequestResult>(okResult);
        }
        #endregion
    }
}
