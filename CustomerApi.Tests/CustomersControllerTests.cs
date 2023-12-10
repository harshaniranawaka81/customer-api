using CustomerApi.Controllers;
using CustomerApi.Domain.Interfaces;
using CustomerApi.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;

namespace CustomerApi.Tests
{
    public class CustomersControllerTests
    {
        private List<Customer> GetCustomerList()
        {
            return new List<Customer>()
            {
                new Customer()
                {
                    Id = 1,
                    Name = "Harshani",
                    Email = "harshani@email.com",
                    Address = "aaa"
                },
                new Customer()
                {
                     Id = 2,
                    Name = "Viraj",
                    Email = "viraj@email.com",
                    Address = "bbb"
                },
                new Customer()
                {
                    Id = 3,
                    Name = "saman",
                    Email = "saman@email.com",
                    Address = "ddd"
                }
            };
        }

        #region GetAllCustomers

        [Fact]
        public async Task GetAllCustomers_ReturnsOkResult()
        {
            // Arrange
            var customerServiceMock = new Mock<ICustomerService>();

            customerServiceMock.Setup(repo => repo.GetCustomers())
                .ReturnsAsync(new KeyValuePair<HttpStatusCode, List<Customer>>(HttpStatusCode.OK, GetCustomerList()));

            var controller = new CustomersController(customerServiceMock.Object);

            // Act
            var result = await controller.GetAllCustomers();

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task GetAllCustomers_ReturnsNoContentResult()
        {
            // Arrange
            var customerServiceMock = new Mock<ICustomerService>();

            customerServiceMock.Setup(repo => repo.GetCustomers())
                .ReturnsAsync(new KeyValuePair<HttpStatusCode, List<Customer>>(HttpStatusCode.NoContent, new List<Customer>()));

            var controller = new CustomersController(customerServiceMock.Object);

            // Act
            var result = await controller.GetAllCustomers();

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAllCustomers_ReturnsBadRequestResult()
        {
            // Arrange
            var customerServiceMock = new Mock<ICustomerService>();

            customerServiceMock.Setup(repo => repo.GetCustomers())
                .ReturnsAsync(new KeyValuePair<HttpStatusCode, List<Customer>>(HttpStatusCode.BadRequest, new List<Customer>()));

            var controller = new CustomersController(customerServiceMock.Object);

            // Act
            var result = await controller.GetAllCustomers();

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        #endregion

        #region GetCustomer

        [Fact]
        public async Task GetCustomer_ValidId_ReturnsOkResult()
        {
            // Arrange
            var customerId = 1;
            var customerServiceMock = new Mock<ICustomerService>();

            Customer? customer = new Customer()
            {
                Id = 1,
                Name = "Harshani",
                Email = "harshani@email.com",
                Address = "aaa"
            };

            _ = customerServiceMock.Setup(repo => repo.GetCustomer(customerId))
                .ReturnsAsync(new KeyValuePair<HttpStatusCode, Customer?>(HttpStatusCode.OK, customer));

            var controller = new CustomersController(customerServiceMock.Object);

            // Act
            var result = await controller.GetCustomer(customerId);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        #endregion

        #region SaveCustomer 

        [Fact]
        public async Task SaveCustomer_ValidRecord_ReturnsCreatedResult()
        {
            // Arrange
            var customerServiceMock = new Mock<ICustomerService>();

            var customer = new Customer { Id = 1, Name = "Harshani", Email = "harshani@email.com", Address = "aaa" };

            customerServiceMock.Setup(repo => repo.SaveCustomer(customer))
                .ReturnsAsync(new KeyValuePair<HttpStatusCode, Customer?>(HttpStatusCode.Created, customer));

            var controller = new CustomersController(customerServiceMock.Object);

            // Act
            var result = await controller.SaveCustomer(customer);

            // Assert
            Assert.IsType<CreatedResult>(result);
        }

        [Theory]
        [InlineData(HttpStatusCode.NoContent, HttpStatusCode.NoContent, null, null)]
        [InlineData(HttpStatusCode.BadRequest, HttpStatusCode.BadRequest, null, null)]
        public async Task SaveCustomer_InvalidRecords_ReturnsMultipleStatusCodes(HttpStatusCode expectedStatusCode, HttpStatusCode actualStatusCode, Customer? expectedValue, Customer? actualValue)
        {
            // Arrange
            var customerServiceMock = new Mock<ICustomerService>();

            var customer = new Customer { Id = 1, Name = "Harshani", Email = "harshani@email.com", Address = "aaa" };
            customerServiceMock.Setup(repo => repo.SaveCustomer(customer))
                .ReturnsAsync(new KeyValuePair<HttpStatusCode, Customer?>(actualStatusCode, actualValue));

            var controller = new CustomersController(customerServiceMock.Object);

            // Act
            var result = await controller.SaveCustomer(customer);

            // Assert
            Assert.Equal<HttpStatusCode>(expectedStatusCode, actualStatusCode);
            Assert.Equal<Customer?>(expectedValue, actualValue);
        }

        #endregion

    }
}