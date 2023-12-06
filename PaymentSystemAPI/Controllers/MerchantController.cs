using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Core.DTOs;
using PaymentSystem.Core.Interfaces;
using System.Net.Mime;

namespace PaymentSystemAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class MerchantController : ControllerBase
    {
        private readonly ILogger<MerchantController> _logger;
        private readonly IMerchantService _merchantService;

        public MerchantController(ILogger<MerchantController> logger, IMerchantService merchantService)
        {
            _logger = logger;
            _merchantService = merchantService;
        }

        /// <summary>
        /// Get merchant
        /// </summary>
        /// <param name="MerchantNumber"></param>
        /// <returns></returns>
        [HttpGet("get-merchant-details")]
        public async Task<IActionResult> GetMerchantByNumber(long merchantNumber)
        {
            _logger.LogInformation($"Getting merchant details...");
            var response = await _merchantService.GetMerchantAsync(merchantNumber);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Insert mercchant details
        /// </summary>
        /// <param name="merchantDetails"></param>
        /// <returns></returns>
        [HttpPost("insert-merchant-details")]
        public async Task<IActionResult> InsertMerchant(MerchantRequestDto merchantDetails)
        {
            _logger.LogInformation($"Inserting merchant details...");
            var response = await _merchantService.InsertMerchantAsync(merchantDetails);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// Delete merchant by merchant id
        /// </summary>
        /// <param name="merchantNumber"></param>
        /// <returns></returns>
        [HttpDelete("delete-merchant")]
        public async Task<IActionResult> DeleteMerchant(long merchantNumber)
        {
            _logger.LogInformation($"deleting merchant details...");
            var response = await _merchantService.DeleteMerchantAsync(merchantNumber);
            return StatusCode(response.StatusCode, response);
        }

        /// <summary>
        /// update merchant details
        /// </summary>
        /// <param name="merchantNumber"></param>
        /// <param name="merchantUpdatedetails"></param>
        /// <returns></returns>
        [HttpPut("update-merchant-details")]
        public async Task<IActionResult> UpdateMerchant(long merchantNumber, MerchantUpdateRequestDto merchantUpdatedetails)
        {
            _logger.LogInformation($"Updating merchant details...");
            var response = await _merchantService.UpdateMerchantDetailsAsync(merchantNumber, merchantUpdatedetails);
            return StatusCode(response.StatusCode, response);
        }
    }
}
