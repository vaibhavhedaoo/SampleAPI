using SampleAPI.Entities;
using SampleAPI.Repositories;
using SampleAPI.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.Results;
using Serilog.Core;

namespace SampleAPI.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<OrdersController> _logger;
        // Add more dependencies as needed.

        public OrdersController(IOrderRepository orderRepository,ILogger<OrdersController> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        /// <summary>
        /// Get List of all Orders
        /// </summary>
        /// <returns>List<Order></returns>
        [HttpGet("")] // TODO: Change route, if needed.
        [ProducesResponseType(StatusCodes.Status200OK)] // TODO: Add all response types
        public async Task<ActionResult<List<Order>>> GetOrders()
        {
            _logger.LogInformation("GetOrder");
            return Ok(_orderRepository.GetAll());
         
        }
        /// <summary>
        /// Get All Deleted orders
        /// </summary>
        /// <returns>List<Order></returns>
        [HttpGet("DeletedOrders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Order>>> GetDeletedOrders()
        {
            _logger.LogInformation("GetDeletedOrders");
            return Ok(_orderRepository.GetAllDeleted());

        }
        /// <summary>
        /// Get all not deleted Orders
        /// </summary>
        /// <returns>List<Order></returns>
        [HttpGet("NotDeletedOrders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Order>>> GetNotDeletedOrders()
        {

            _logger.LogInformation("GetNotDeletedOrders");
            return Ok(_orderRepository.GetAllNotDeleted());

        }
        /// <summary>
        /// Get all recent orders
        /// </summary>
        /// <returns></returns>
        [HttpGet("RecentOrders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Order>>> GetRecentOrders()
        {
            _logger.LogInformation($"{nameof(GetRecentOrders)}");
            return Ok(_orderRepository.GetRecentOrder());
        }
        /// TODO: Add an endpoint to allow users to create an order using <see cref="CreateOrderRequest"/>.

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult AddOrder(Order order)
        {
            _logger.LogInformation("AddOrder");
            try
            {
                if (ModelState.IsValid)
                {
                    if (!_orderRepository.IsRecordExists(order.ProductName))
                    {
                        int orderid = 0;
                        orderid = _orderRepository.AddNewOrder(order);
                        if (orderid > 0)
                        {
                            _logger.LogInformation("Order Created with ID : " + orderid);
                            return Ok("Order Created with ID : " + orderid);
                        }
                        else
                        {
                            _logger.LogInformation("Internal Server Error");
                            return StatusCode(500);
                        }
                    }
                    else
                    {
                        _logger.LogInformation("Item Already Exixts");
                        return BadRequest("Item Already Exixts");
                    }
                }
                else {
                    _logger.LogInformation("Model Validation failed");
                    return BadRequest(ModelState); }
            }
            catch(Exception e)
            {
                _logger.LogInformation("Internal Server Error");
                return StatusCode(500);
            }

        }
    }
}
