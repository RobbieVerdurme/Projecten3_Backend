using Projecten3_BackendTest.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Castle.Core.Configuration;
using Projecten3_Backend.Data.IRepository;
using Microsoft.AspNetCore.Identity;
using Projecten3_Backend.Controllers;

namespace Projecten3_BackendTest.Controllers
{
    public class AccountControllerTest : ControllerBase
    {
        #region Properties
        private readonly DummyProject3_BackendContext _dummyData;
        private readonly Mock<SignInManager<IdentityUser>> _signInManager;
        private readonly Mock<UserManager<IdentityUser>> _userManager;
        private readonly Mock<IUserRepository> _userRepo;
        private readonly Mock<ITherapistRepository> _therapistRepo;
        private readonly Mock<IConfiguration> _config;
        private readonly AccountController _accountController;
        #endregion

        #region Constructors
        public AccountControllerTest()
        {
            /*
            _dummyData = new DummyProject3_BackendContext();
            _signInManager = new Mock<SignInManager<IdentityUser>>();
            _userManager = new Mock<UserManager<IdentityUser>>();
            _userRepo = new Mock<IUserRepository>();
            _therapistRepo = new Mock<ITherapistRepository>();
            _config = new Mock<IConfiguration>();
            _accountController = new AccountController(
                _signInManager.Object,
                _userManager.Object,
                _userRepo.Object,
                _therapistRepo.Object,
                _config.Object);
                */
        }
        #endregion
    }
}
