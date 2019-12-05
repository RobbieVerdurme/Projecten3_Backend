using Projecten3_Backend.Controllers;
using Projecten3_BackendTest.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using Moq;
using Projecten3_Backend.Data.IRepository;
using Microsoft.AspNetCore.Mvc;
using Projecten3_Backend.Model;

namespace Projecten3_BackendTest.Controllers
{
    public class CategoriesControllerTest : ControllerBase
    {
        #region Properties
        private readonly CategoriesController _categoriesController;
        private readonly DummyProject3_BackendContext _dummyData;
        private readonly Mock<ICategoryRepository> _categoryRepository;
        #endregion

        #region Constructors
        public CategoriesControllerTest()
        {
            _dummyData = new DummyProject3_BackendContext();
            _categoryRepository = new Mock<ICategoryRepository>();
            _categoriesController = new CategoriesController(_categoryRepository.Object);
        }
        #endregion

        #region Get
        [Fact]
        public void GetCategories_ReturnsOk()
        {
            _categoryRepository.Setup(cr => cr.GetCategories()).Returns(_dummyData.Categories);
            var okResult = _categoriesController.GetCategories() as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
        #endregion

        #region Post
        [Fact]
        public void AddCategory_ReturnsOk()
        {
            _categoryRepository.Setup(cr => cr.CategoryExists(It.IsAny<Category>())).Returns(false);
            _categoryRepository.Setup(cr => cr.AddCategory(It.IsAny<Category>()));
            var okResult = _categoriesController.AddCategory(_dummyData.Category) as OkResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void AddCategory_ReturnsStatusCodeAndFail()
        {
            _categoryRepository.Setup(cr => cr.CategoryExists(It.IsAny<Category>())).Returns(true);
            var okResult = _categoriesController.AddCategory(_dummyData.Category) as StatusCodeResult;
            Assert.NotNull(okResult);
            Assert.Equal(303, okResult.StatusCode);
        }
        #endregion

        #region Put
        [Fact]
        public void EditCategory_ReturnsOk()
        {
            Category cat = _dummyData.Category;
            _categoryRepository.Setup(cr => cr.CategoryExists(It.IsAny<Category>())).Returns(false);
            _categoryRepository.Setup(cr => cr.Update(It.IsAny<Category>()));
            _categoryRepository.Setup(cr => cr.GetById(0)).Returns(cat);
            var okResult = _categoriesController.AddCategory(cat) as OkResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void EditCategory_ReturnsStatusCodeAndFail()
        {
            _categoryRepository.Setup(cr => cr.CategoryExists(It.IsAny<Category>())).Returns(true);
            var okResult = _categoriesController.AddCategory(_dummyData.Category) as StatusCodeResult;
            Assert.NotNull(okResult);
            Assert.Equal(303, okResult.StatusCode);
        }
        #endregion

        #region Delete
        [Fact]
        public void DeleteCategory_ReturnOk()
        {
            _categoryRepository.Setup(cr => cr.GetById(0)).Returns(_dummyData.Category);
            _categoryRepository.Setup(cr => cr.DeleteCategory(0));
            var okResult = _categoriesController.DeleteCategory(0) as OkResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }

        [Fact]
        public void DeleteCategory_ReturnNotFound()
        {
            _categoryRepository.Setup(cr => cr.GetById(0));
            _categoryRepository.Setup(cr => cr.DeleteCategory(0));
            var okResult = _categoriesController.DeleteCategory(0) as NotFoundResult;
            Assert.IsType<NotFoundResult>(okResult);
        }
        #endregion
    }
}
