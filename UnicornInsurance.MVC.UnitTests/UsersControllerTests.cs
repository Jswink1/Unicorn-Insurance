using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicornInsurance.MVC.Contracts;
using UnicornInsurance.MVC.Controllers;
using UnicornInsurance.MVC.Models;
using UnicornInsurance.MVC.UnitTests.Mocks;
using Xunit;

namespace UnicornInsurance.MVC.UnitTests
{
    public class UsersControllerTests
    {
        private Mock<IAuthenticationService> _authService;
        private UsersController _usersController;
        private LoginVM _login;
        private RegisterVM _register;

        public UsersControllerTests()
        {
            _authService = MockAutheticationService.GetAuthenticationService();

            _usersController = new UsersController(_authService.Object);

            _login = new LoginVM()
            {
                Email = "user@user.com",
                Password = "password"
            };

            _register = new RegisterVM()
            {
                Email = "user@user.com",
                Name = "user",
                UserName = "username",
                Password = "password"
            };
        }

        [Fact]
        public async Task Login_ReturnsLocalRedirect_WhenModelStateIsValid()
        {
            var result = await _usersController.Login(_login);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            redirectToActionResult.ControllerName.ShouldBe("Home");
            redirectToActionResult.ActionName.ShouldBe("Index");

            _authService.Verify(x => x.Authenticate(It.IsAny<LoginVM>()), Times.Once);
        }

        [Fact]
        public async Task Login_ReturnsView_WhenModelStateIsInvalid()
        {
            _usersController.ModelState.AddModelError("Password", "Required");
            var result = await _usersController.Login(_login);

            Assert.IsType<ViewResult>(result);

            _authService.Verify(x => x.Authenticate(It.IsAny<LoginVM>()), Times.Never);
        }

        [Fact]
        public async Task Register_ReturnsRedirect_WhenModelStateIsValid()
        {
            var result = await _usersController.Register(_register);

            var redirectResult = Assert.IsType<RedirectResult>(result);

            _authService.Verify(x => x.Register(It.IsAny<RegisterVM>()), Times.Once);
        }

        [Fact]
        public async Task Register_ReturnsView_WhenModelStateIsInvalid()
        {
            _usersController.ModelState.AddModelError("Password", "Required");
            var result = await _usersController.Register(_register);

            Assert.IsType<ViewResult>(result);

            _authService.Verify(x => x.Register(It.IsAny<RegisterVM>()), Times.Never);
        }

        [Fact]
        public async Task LogoutTest()
        {
            var result = await _usersController.Logout();

            var localRedirectResult = Assert.IsType<RedirectToActionResult>(result);

            _authService.Verify(x => x.Logout(), Times.Once);
        }

        [Fact]
        public async Task VerifyEmailTest()
        {
            var result = await _usersController.VerifyEmail("EmailToken");

            var viewResult = Assert.IsType<ViewResult>(result);

            var model = Assert.IsAssignableFrom<VerifyEmailVM>(viewResult.ViewData.Model);

            _authService.Verify(x => x.VerifyEmail("EmailToken"), Times.Once);
        }
    }
}
