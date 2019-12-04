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

namespace Projecten3_BackendTest.Controllers
{
    public class TherapistsControllerTest
    {
        #region Properties
        private readonly TherapistsController _therapistsController;
        private readonly Mock<ITherapistRepository> _therapistRepository;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<ICategoryRepository> _categoryRepository;
        private readonly DummyProject3_BackendContext _dummyData;
        #endregion

        #region Constructors
        public TherapistsControllerTest()
        {
            _dummyData = new DummyProject3_BackendContext();
            _therapistRepository = new Mock<ITherapistRepository>();
            _categoryRepository = new Mock<ICategoryRepository>();
            _userRepository = new Mock<IUserRepository>();
            _therapistsController = new TherapistsController(
                _therapistRepository.Object,
                _userRepository.Object,
                _categoryRepository.Object);
        }
        #endregion

        #region Get
        [Fact]
        public void GetTherapists_ReturnsOk()
        {
            _therapistRepository.Setup(tr => tr.GetTherapists()).Returns(_dummyData.Therapists);
            var okResult = _therapistsController.GetTherapist() as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void GetTherapist_ReturnsOk()
        {
            _therapistRepository.Setup(tr => tr.GetById(1)).Returns(_dummyData.Therapists.First);
            var okResult = _therapistsController.GetTherapist(1) as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void GetTherapist_ReturnsNotFound()
        {
            _therapistRepository.Setup(tr => tr.GetById(1));
            var okResult = _therapistsController.GetTherapist(1) as NotFoundResult;
            Assert.IsType<NotFoundResult>(okResult);
        }

        [Fact]
        public void GetTherapistTypes_ReturnsOk()
        {
            _therapistRepository.Setup(tr => tr.GetTherapistTypes()).Returns(_dummyData.TherapistTypes);
            var okResult = _therapistsController.GetTherapistTypes() as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
        #endregion

        #region Post
        [Fact]
        public void AddTherapist_ReturnsOk()
        {
            _therapistRepository.Setup(tr => tr.TherapistTypeExists(0)).Returns(true);
            _therapistRepository.Setup(tr => tr.GetTherapistTypes()).Returns(_dummyData.TherapistTypes);
            // To clean up, can't get first of _dummyData.TherapistTypes.First
            Therapist t = Therapist.MapAddTherapistDTOToTherapist(_dummyData.AddTherapistDTO, new TherapistType { TherapistTypeId = 0, Type = "therapisttype" });
            _therapistRepository.Setup(tr => tr.TherapistExists(t)).Returns(true);
            _therapistRepository.Setup(tr => tr.AddTherapist(It.IsAny<Therapist>()));
            var okResult = _therapistsController.AddTherapist(_dummyData.AddTherapistDTO) as OkResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void AddTherapist_ReturnsBadRequest()
        {
            _therapistRepository.Setup(tr => tr.TherapistTypeExists(0));
            _therapistRepository.Setup(tr => tr.GetTherapistTypes());
            // To clean up, can't get first of _dummyData.TherapistTypes.First
            Therapist t = Therapist.MapAddTherapistDTOToTherapist(_dummyData.AddTherapistDTO, new TherapistType { TherapistTypeId = 0, Type = "therapisttype" });
            _therapistRepository.Setup(tr => tr.TherapistExists(t)).Returns(true);
            _therapistRepository.Setup(tr => tr.AddTherapist(It.IsAny<Therapist>()));
            var okResult = _therapistsController.AddTherapist(_dummyData.AddTherapistDTO) as BadRequestResult;
            Assert.IsType<BadRequestResult>(okResult);
        }

        [Fact]
        public void AddTherapistType_ReturnsOk()
        {
            AddTherapistTypeDTO addTherapistTypeDTO = _dummyData.AddTherapistTypeDTO;
            _categoryRepository.Setup(cr => cr.CategoriesExist(addTherapistTypeDTO.Categories)).Returns(true);
            _therapistRepository.Setup(tr => tr.TherapistTypeExists(addTherapistTypeDTO.Type, addTherapistTypeDTO.Categories)).Returns(false);
            _categoryRepository.Setup(cr => cr.GetCategoriesById(addTherapistTypeDTO.Categories)).Returns(_dummyData.Categories);
            _therapistRepository.Setup(tr => tr.AddTherapistType(It.IsAny<TherapistType>()));
            var okResult = _therapistsController.AddTherapistType(addTherapistTypeDTO) as OkResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void AddTherapistType_ReturnsBadRequest()
        {
            AddTherapistTypeDTO addTherapistTypeDTO = _dummyData.AddTherapistTypeDTO;
            _categoryRepository.Setup(cr => cr.CategoriesExist(addTherapistTypeDTO.Categories)).Returns(false);
            _therapistRepository.Setup(tr => tr.TherapistTypeExists(addTherapistTypeDTO.Type, addTherapistTypeDTO.Categories)).Returns(true);
            _categoryRepository.Setup(cr => cr.GetCategoriesById(addTherapistTypeDTO.Categories)).Returns(_dummyData.Categories);
            _therapistRepository.Setup(tr => tr.AddTherapistType(It.IsAny<TherapistType>()));
            var okResult = _therapistsController.AddTherapistType(addTherapistTypeDTO) as BadRequestResult;
            Assert.IsType<BadRequestResult>(okResult);
        }
        #endregion

        #region Put
        [Fact]
        public void EditTherapist_ReturnsOk()
        {
            EditTherapistDTO editTherapistDTO = _dummyData.EditTherapistDTO;
            //_therapistRepository.Setup()
        }
        #endregion
    }
}
