using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Core.DTOs;
using PaymentSystem.Core.Interfaces;
using System.Net.Mime;
using System.Security.Claims;

namespace PaymentSystemAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService  _customerService;

        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }
        /// <summary>
        /// Get customer details
        /// </summary>
        /// <param name="NationalId"></param>
        /// <returns></returns>
        [HttpGet(Name = "get-customer-details")]
        public async Task<IActionResult> GetCustomerById(string NationalId)
        {
            _logger.LogInformation($"Getting customer details...");
            var response = await _customerService.GetCustomerAsync(NationalId);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Insert customer details
        /// </summary>
        /// <param name="customerDetails"></param>
        /// <returns></returns>
        public async Task<IActionResult> InsertCustomer(CustomerRequestDto customerDetails)
        {
            _logger.LogInformation($"Inserting customer details...");
            var response = await _customerService.InsertCustomerAsync(customerDetails);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Delete customer by national id
        /// </summary>
        /// <param name="NationalId"></param>
        /// <returns></returns>
        public async Task<IActionResult> DeleteCustomer(string NationalId)
        {
            _logger.LogInformation($"deleting customer details...");
            var response = await _customerService.DeleteCustomerAsync(NationalId);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// update customer details
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        public async Task<IActionResult> UpdateCustomer(CustomerEditRequestDto details)
        {
            _logger.LogInformation($"Editing customer details...");
            var response = await _customerService.UpdateCustomerDetailsAsync(details);
            return StatusCode(response.StatusCode, response);
        }
    }
}