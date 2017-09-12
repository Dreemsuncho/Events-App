using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using Xunit;
using Moq;

using Events.Web.Controllers;
using Events.Web.Core;
using Events.Web.Models;

namespace Events.Web.Tests
{
    public class AccountApiController_Tests
    {
        /// <summary>
        /// Boolean from task from function that specifies the result from async mock methods.
        /// </summary>
        private Func<Task<bool>> _BoolFromTaskFunction(bool result) => () => Task.Run(() => result);

        [Fact]
        public async void Login_success()
        {
            // Arrange
            var securityAdapter = new Mock<ISecurityAdapter>();
            var controller = new AccountApiController(securityAdapter.Object);

            var model = new LoginModel
            {
                LoginEmail = "test@login.com",
                Password = "test password",
                RememberMe = false
            };

            securityAdapter.Setup(x => x.UserExists(model.LoginEmail)).Returns(_BoolFromTaskFunction(true));
            securityAdapter.Setup(x => x.CheckPassword(model.LoginEmail, model.Password)).Returns(_BoolFromTaskFunction(true));
            securityAdapter.Setup(x => x.Login(model.LoginEmail, model.Password, model.RememberMe)).Returns(_BoolFromTaskFunction(true));

            // Act
            var result = await controller.Login(model);

            // Assert
            Assert.IsType<OkObjectResult>(result);

            securityAdapter.Verify(x => x.UserExists(model.LoginEmail), Times.Once);
            securityAdapter.Verify(x => x.CheckPassword(model.LoginEmail, model.Password), Times.Once);
            securityAdapter.Verify(x => x.Login(model.LoginEmail, model.Password, model.RememberMe), Times.Once);
        }

        [Fact]
        public async void Login_fail()
        {
            // Arrange
            var securityAdapter = new Mock<ISecurityAdapter>();
            var controller = new AccountApiController(securityAdapter.Object);

            var model = new LoginModel { LoginEmail = "", Password = "", };

            // Act
            var result = await controller.Login(model);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);

            var objectResult = result as ObjectResult;
            var errors = objectResult.Value as ICollection<string>;

            Assert.NotNull(errors);

            Assert.True(errors.Count == 4);
        }

        [Fact]
        public async void Register_success()
        {
            // Arrange
            var securityAdapter = new Mock<ISecurityAdapter>();
            var controller = new AccountApiController(securityAdapter.Object);

            var model = new RegisterModel
            {
                FirstName = "first name",
                LastName = "last name",
                LoginEmail = "test@login.com",
                Password = "test password",
                ConfirmPassword = "test password",
                RememberMe = false
            };


            securityAdapter.Setup(x => x.UserExists(model.LoginEmail)).Returns(_BoolFromTaskFunction(false));
            securityAdapter.Setup(x => x.Register(model.LoginEmail, model.FirstName, model.LastName, model.Password)).Returns(_BoolFromTaskFunction(true));

            // Act
            var result = await controller.Register(model);

            // Assert
            Assert.IsType<OkObjectResult>(result);

            securityAdapter.Verify(x => x.UserExists(model.LoginEmail), Times.Once);
            securityAdapter.Verify(x => x.Register(model.LoginEmail, model.FirstName, model.LastName, model.Password), Times.Once);
            securityAdapter.Verify(x => x.Login(model.LoginEmail, model.Password, model.RememberMe));
        }

        [Fact]
        public async void Register_fail()
        {
            // Arrange
            var securityAdapter = new Mock<ISecurityAdapter>();
            var controller = new AccountApiController(securityAdapter.Object);

            var model = new RegisterModel
            {
                FirstName = "",
                LastName = "",
                LoginEmail = "",
                Password = "",
                ConfirmPassword = ""
            };

            // Act
            var result = await controller.Register(model);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);

            var objectResult = result as ObjectResult;
            var errors = objectResult.Value as ICollection<string>;

            Assert.NotNull(errors);

            Assert.True(errors.Count == 1);
        }

        [Fact]
        public async void ChangePassword_success()
        {
            // Arrange
            var securityAdapter = new Mock<ISecurityAdapter>();
            var controller = new AccountApiController(securityAdapter.Object);

            var userIdentity = new ClaimsIdentity();
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = userPrincipal }
            };

            var model = new ChangePasswordModel
            {
                OldPassword = "Old pass",
                NewPassword = "New Pass",
                ConfirmPassword = "New Pass"
            };

            securityAdapter.Setup(x => x.CheckPassword(userIdentity.Name, model.OldPassword)).Returns(_BoolFromTaskFunction(true));
            securityAdapter.Setup(x => x.ChangePassword(userIdentity.Name, model.OldPassword, model.NewPassword)).Returns(_BoolFromTaskFunction(true));

            // Act
            var result = await controller.ChangePassword(model);

            // Assert
            Assert.IsType<OkResult>(result);

            securityAdapter.Verify(x => x.CheckPassword(userIdentity.Name, model.OldPassword), Times.Once);
            securityAdapter.Verify(x => x.ChangePassword(userIdentity.Name, model.OldPassword, model.NewPassword), Times.Once);
        }

        [Fact]
        public async void ChangePassword_fail()
        {
            // Arrange
            var securityAdapter = new Mock<ISecurityAdapter>();
            var controller = new AccountApiController(securityAdapter.Object);

            var userIdentity = new ClaimsIdentity();
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = userPrincipal }
            };

            // Act
            var result = await controller.ChangePassword(new ChangePasswordModel());

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
