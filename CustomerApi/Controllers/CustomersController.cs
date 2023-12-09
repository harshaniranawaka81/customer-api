using CustomerApi.Business.Interfaces;
using CustomerApi.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _CustomerService;

        /// <summary>
        /// Customer controller 
        /// </summary>
        /// <param name="CustomerService"></param>
        public CustomersController(ILogger<CustomersController> logger, ICustomerService CustomerService)
        {
            _CustomerService = CustomerService;
        }

        /// <summary>
        /// Get all Customer details
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCustomers()
        {
            var result = await _CustomerService.GetCustomers();

            return result.Key switch
            {
                HttpStatusCode.OK => Ok(result.Value),
                HttpStatusCode.NoContent => NoContent(),
                HttpStatusCode.BadRequest => BadRequest()
            };
        }

        /// <summary>
        /// Get Customer details using the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var result = await _CustomerService.GetCustomer(id);

            return result.Key switch
            {
                HttpStatusCode.OK => Ok(result.Value),
                HttpStatusCode.NotFound => NotFound(),
                HttpStatusCode.BadRequest => BadRequest()
            };
        }

        /// <summary>
        /// Save a new Customer in the database
        /// </summary>
        /// <param name="Customer"></param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SaveCustomer(Customer customer)
        {
            var result = await _CustomerService.SaveCustomer(customer);

            return result.Key switch
            {
                HttpStatusCode.Created => Created(string.Empty, result.Value),
                HttpStatusCode.NoContent => NoContent(),
                HttpStatusCode.BadRequest => BadRequest()
            };
        }

        /// <summary>
        /// Update customer details in the database
        /// </summary>
        /// <param name="Customer"></param>
        /// <returns></returns>
        [HttpPut()]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCustomer(int id, Customer customer)
        {
            var result = await _CustomerService.EditCustomer(id, customer);

            return result.Key switch
            {
                HttpStatusCode.OK => Ok(result.Value),
                HttpStatusCode.NoContent => NoContent(),
                HttpStatusCode.BadRequest => BadRequest()
            };
        }

        /// <summary>
        /// Delete a Customer from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var result = await _CustomerService.DeleteCustomer(id);

            return result.Key switch
            {
                HttpStatusCode.NoContent => NoContent(),
                HttpStatusCode.NotFound => NotFound(),
                HttpStatusCode.BadRequest => BadRequest()
            };
        }
    }
}
