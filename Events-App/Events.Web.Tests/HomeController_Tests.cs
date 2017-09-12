using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;

using Xunit;
using Moq;

using Events.Data.Repositories;
using Events.Web.Controllers;
using Events.Web.Models;
using Events.Data.Contracts;
using Events.Data.Entities;

namespace Events.Web.Tests
{
    public class HomeController_Tests
    {
        [Fact]
        public void Index_default()
        {
            // Arrange
            var repositoryFactory = new Mock<IDataRepositoryFactory>();
            var controller = new HomeController(repositoryFactory.Object);

            var repository = new Mock<EventsRepository>();
            repositoryFactory.Setup(x => x.GetRepository<EventsRepository>()).Returns(repository.Object);

            // Act
            var result = controller.Index(null);

            // Assert
            Assert.IsType<ViewResult>(result);

            var actionResult = result as ViewResult;

            Assert.IsType<IndexModel>(actionResult.Model);

            var model = actionResult.Model as IndexModel;

            Assert.True(model.Page == 0);
        }

        [Fact]
        public void Index_with_page_param_return_events()
        {
            // Arrange
            var repositoryFactory = new Mock<IDataRepositoryFactory>();
            var controller = new HomeController(repositoryFactory.Object);

            var repository = new Mock<EventsRepository>();

            var events = new[] { new Event() };
            repository.Setup(x => x.GetAllWithComments(It.IsAny<Expression<Func<Event, bool>>>(), It.IsAny<int>(), It.IsAny<int>())).Returns(events);
            repositoryFactory.Setup(x => x.GetRepository<EventsRepository>()).Returns(repository.Object);

            // Act
            var result = controller.Index(null, 1);

            // Assert
            Assert.IsType<ViewResult>(result);

            var actionResult = result as ViewResult;

            Assert.IsType<IndexModel>(actionResult.Model);

            var model = actionResult.Model as IndexModel;

            Assert.True(model.Page == 1);
            Assert.Same(events, model.Events);
        }
    }
}
