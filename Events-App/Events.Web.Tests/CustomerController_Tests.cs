using Events.Web.Controllers.MVC;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Events.Web.Tests
{
    public class CustomerController_Tests
    {
        [Fact]
        public void CreateEvent()
        {
            // Arrange
            var controller = new CustomerController();

            // Act
            var result = controller.CreateEvent();

            // Assert
            Assert.IsType<ViewResult>(result);
        }
    }
}
