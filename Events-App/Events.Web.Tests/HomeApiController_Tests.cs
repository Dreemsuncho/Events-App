using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;

using Xunit;
using Moq;

using Events.Data.Contracts;
using Events.Data.Entities;
using Events.Data.Repositories;
using Events.Web.Controllers;

namespace Events.Web.Tests
{
    public class HomeApiController_Tests
    {
        [Fact]
        public void Index_success_without_events()
        {
            // Arrange
            var repositoryFactory = new Mock<IDataRepositoryFactory>();
            var controller = new HomeApiController(repositoryFactory.Object);

            var queryDate = new DateTime();

            repositoryFactory.Setup(x => x.GetRepository<EventsRepository>()).Returns(new Mock<EventsRepository>().Object);

            // Act
            var result = controller.Index(queryDate, 0);

            // Assert
            Assert.IsType<OkObjectResult>(result);

            var okObject = result as OkObjectResult;

            Assert.IsType<List<Event>>(okObject.Value);
        }

        [Fact]
        public void Index_success_with_events()
        {
            // Arrange
            var repositoryFactory = new Mock<IDataRepositoryFactory>();
            var controller = new HomeApiController(repositoryFactory.Object);

            var queryDate = new DateTime();

            var repository = new Mock<EventsRepository>();
            var events = new List<Event> { new Event() };

            repository.Setup(x => x.GetAllWithComments(It.IsAny<Expression<Func<Event, bool>>>(), It.IsAny<int>(), It.IsAny<int>())).Returns(events);
            repositoryFactory.Setup(x => x.GetRepository<EventsRepository>()).Returns(repository.Object);

            // Act
            var result = controller.Index(queryDate, 0);

            // Assert
            Assert.IsType<OkObjectResult>(result);

            var okObject = result as OkObjectResult;

            Assert.IsType<List<Event>>(okObject.Value);

            Assert.Same(events, okObject.Value);
        }
    }
}
