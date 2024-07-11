using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SampleAPI.Controllers;
using SampleAPI.Entities;
using SampleAPI.Repositories;
using SampleAPI.Requests;
using Serilog.Core;

namespace SampleAPI.Tests.Controllers
{
    public class OrdersControllerTests
    {
        // TODO: Write controller unit tests
        [Fact]
        public async Task GetOrders_ReturnsOkResult_WithListOfOrders()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order { Id = 1, ProductName = "Product A", Description="Product A" ,EntryDate=Convert.ToDateTime("10/07/2024"), Invoiced=true, Deleted= true},
                new Order { Id = 2, ProductName = "Product B", Description="Product B",EntryDate=Convert.ToDateTime("10/07/2024"), Invoiced=true, Deleted= true}
             };

            var mockRepository = new Mock<IOrderRepository>();
            var logger = Mock.Of<ILogger<OrdersController>>(); 
            mockRepository.Setup(repo => repo.GetAll()).Returns(orders);

            var controller = new OrdersController(mockRepository.Object, logger);

            // Act
            var result = await controller.GetOrders();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnOrders = Assert.IsType<List<Order>>(okResult.Value);
            Assert.Equal(2, returnOrders.Count);
        }


        [Fact]
        public void AddOrder_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var mockRepository = new Mock<IOrderRepository>();
            var logger = Mock.Of<ILogger<OrdersController>>();
            var controller = new OrdersController(mockRepository.Object,logger);

            var newOrder = new Order
            {
                Id = 1,
                ProductName = "Product A",
                Description = "Product A",
                Invoiced = true,
                EntryDate = DateTime.UtcNow,
                Deleted = false
            };

            // Act
            var result = controller.AddOrder(newOrder);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            //Assert.Equal(nameof(controller.GetOrderById), createdAtActionResult.ActionName);
            Assert.Equal(newOrder.Id, createdAtActionResult.RouteValues["id"]);
            Assert.Equal(newOrder, createdAtActionResult.Value);
        }
    }
}
