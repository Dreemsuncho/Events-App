using System.Collections.Generic;
using System.Net;
using System.Security.Claims;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;

using Events.Data.Contracts;
using Events.Data.Entities;
using Events.Web.Controllers;
using Events.Web.Models;


namespace Events.Web.Tests
{
    public class CustomerApiController_Tests
    {
        [Fact]
        public void CreateComment_fail()
        {
            // Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var controller = new CustomerApiController(unitOfWork.Object);

            var model = new CommentModel();

            var userIdentity = new ClaimsIdentity();
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = userPrincipal }
            };

            var eventsRepository = new Mock<IEventsRepository>();
            var accountRepository = new Mock<IAccountRepository>();

            unitOfWork.Setup(x => x.AccountRepository).Returns(accountRepository.Object);
            unitOfWork.Setup(x => x.EventsRepository).Returns(eventsRepository.Object);

            var commentsCollection = new List<Comment>();
            eventsRepository.Setup(x => x.Get(model.EventId)).Returns(new Event { Comments = commentsCollection });

            // Act
            var result = controller.CreateComment(model);

            // Assert
            Assert.IsType<ObjectResult>(result);
            
            ObjectResult objectResult = result as ObjectResult;

            Assert.True(objectResult.StatusCode == (int)HttpStatusCode.BadRequest);

            Assert.IsType<List<string>>(objectResult.Value);

            Assert.True((objectResult.Value as List<string>).Count > 0);
        }

        [Fact]
        public void CreateComment_success()
        {
            // Arrange
            var unitOfWork = new Mock<IUnitOfWork>();
            var controller = new CustomerApiController(unitOfWork.Object);

            var model = new CommentModel();

            var userIdentity = new ClaimsIdentity();
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = userPrincipal }
            };

            var eventsRepository = new Mock<IEventsRepository>();
            var accountRepository = new Mock<IAccountRepository>();

            unitOfWork.Setup(x => x.AccountRepository).Returns(accountRepository.Object);
            unitOfWork.Setup(x => x.EventsRepository).Returns(eventsRepository.Object);
            unitOfWork.Setup(x => x.Commit()).Returns(1);

            var commentsCollection = new List<Comment>();
            eventsRepository.Setup(x => x.Get(model.EventId)).Returns(new Event { Comments = commentsCollection });

            // Act
            var result = controller.CreateComment(model);

            // Assert
            Assert.IsType<ObjectResult>(result);

            ObjectResult objectResult = result as ObjectResult;

            Assert.True(objectResult.StatusCode == (int)HttpStatusCode.Created);

            Assert.IsType<Comment>(objectResult.Value);
        }
    }
}
