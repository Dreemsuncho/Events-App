using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Moq;
using Xunit;

using Events.Web.Core;
using Events.Web.Controllers;
using Events.Web.Models;

namespace Events.Web.Tests
{
    public class AccountController_Tests
    {
        [Fact]
        public void Register()
        {
            // Arrange
            var securityAdapter = new Mock<ISecurityAdapter>();
            var controller = new AccountController(securityAdapter.Object);

            // Act
            var result = controller.Register();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Login()
        {
            // Arrange
            var securityAdapter = new Mock<ISecurityAdapter>();
            var controller = new AccountController(securityAdapter.Object);

            string returnUrl = "https://www.test-url.com";

            // Act
            var result = controller.Login(returnUrl);

            // Assert
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;

            Assert.IsType<LoginModel>(viewResult.Model);
        }

        [Fact]
        public async void Logout()
        {
            // Arrange
            var securityAdapter = new Mock<ISecurityAdapter>();
            var controller = new AccountController(securityAdapter.Object);

            var session = new Mock<ISession>();
            var httpContext = new DefaultHttpContext { Session = session.Object };

            controller.ControllerContext = new ControllerContext { HttpContext = httpContext };
            
            // Act
            var result = await controller.Logout();

            // Assert
            securityAdapter.Verify(x => x.Logout(), Times.Once);

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void ChangePassword()
        {
            // Arrange
            var securityAdapter = new Mock<ISecurityAdapter>();
            var controller = new AccountController(securityAdapter.Object);

            // Act
            var result = controller.ChangePassword();

            // Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}
