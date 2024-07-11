using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using SampleAPI.Entities;
using SampleAPI.Repositories;
using SampleAPI.Requests;

namespace SampleAPI.Tests.Repositories
{
    public class OrderRepositoryTests
    {
        // TODO: Write repository unit tests
        private SampleApiDbContext GetInMemoryOrderContext()
        {
            var options = new DbContextOptionsBuilder<SampleApiDbContext>()
                .UseInMemoryDatabase(databaseName: "OrderDatabase")
                .Options;

            var context = new SampleApiDbContext(options);
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            return context;
        }

        [Fact]
        public void GetAllOrders_ReturnsAllOrders()
        {
            // Arrange
            var context = GetInMemoryOrderContext();
            var repository = new OrderRepository();

            var order1 = new Order { Id = 1, ProductName = "Product A",Description="Product A", EntryDate= Convert.ToDateTime( "10/07/2024"), Invoiced= true, Deleted= false };
            var order2 = new Order { Id = 2, ProductName = "Product B", Description = "Product B", EntryDate = Convert.ToDateTime( "10/07/2024"), Invoiced = true, Deleted = true };
            var order3 = new Order { Id = 3, ProductName = "Product C", Description = "Product C", EntryDate = Convert.ToDateTime("10/07/2024"), Invoiced = true, Deleted = false };

            context.Orders.AddRange(order1, order2, order3);
            context.SaveChanges();

            // Act
            var orders = repository.GetAll();

            // Assert
            Assert.Equal(3, orders.Count);
            Assert.Contains(orders, o => o.Id == 1);
            Assert.Contains(orders, o => o.Id == 2);
            Assert.Contains(orders, o => o.Id == 3);
            Assert.DoesNotContain(orders, o => o.Id == 4);
        }

        [Fact]
        public void GetAllDeleted_ReturnsAllDeletedOrders()
        {
            // Arrange
            var context = GetInMemoryOrderContext();
            var repository = new OrderRepository();


            // Act
            var orders = repository.GetAllDeleted();

            // Assert
            //Assert.Equals(1,orders.Count);
            Assert.Contains(orders, o => o.Id == 2);
            Assert.DoesNotContain(orders, o => o.Id == 1);
            Assert.DoesNotContain(orders, o => o.Id == 3);
        }

        [Fact]
        public void GetAllNotDeleted_ReturnsAllNotDeletedOrders()
        {
            // Arrange
            var context = GetInMemoryOrderContext();
            var repository = new OrderRepository();


            // Act
            var orders = repository.GetAllNotDeleted();

            // Assert
            Assert.Equal(2,orders.Count);
            Assert.Contains(orders, o => o.Id == 1);
            Assert.Contains(orders, o => o.Id == 3);
            Assert.DoesNotContain(orders, o => o.Id == 2);
        }
        [Fact]
        public void AddOrder_ShouldAddOrderToDatabase()
        {
            // Arrange
            var context = GetInMemoryOrderContext();
            var repository = new OrderRepository();

            var order = new Order
            {

                ProductName = "iPhone",
                Description = "Phone",
                EntryDate = Convert.ToDateTime("10/07/2024"),
                Deleted = false,
                Invoiced = true,
            };

            // Act
            repository.AddNewOrder(order);

            // Assert
            var ordersInDb = context.Orders.ToList();
            Assert.Single(ordersInDb);

            var addedOrder = ordersInDb.First();
            Assert.Equal(order.Id, addedOrder.Id);
            Assert.Equal(order.ProductName, addedOrder.ProductName);
            Assert.Equal(Convert.ToDateTime("10/07/2024"), addedOrder.EntryDate.Date); // Ensure CreatedAt is set to today
            Assert.False(addedOrder.Deleted);
        }
    }
}