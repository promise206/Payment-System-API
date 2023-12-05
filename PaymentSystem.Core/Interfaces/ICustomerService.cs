using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentSystem.Core.Interfaces
{
    public interface ICustomerService
    {
        /// <summary>
        /// Get Customer details
        /// </summary>
        /// <param name="NationalId"></param>
        /// <returns></returns>
        Task<ResponseDto<CustomerResponseDto>> GetCustomerAsync(string NationalId);

        /// <summary>
        /// Insert customer details
        /// </summary>
        /// <param name="customerDetails"></param>
        /// <returns></returns>
        Task<ResponseDto<bool>> InsertCustomerAsync(CustomerRequestDto customerDetails);

        /// <summary>
        /// delete customer
        /// </summary>
        /// <param name="NationalId"></param>
        /// <returns></returns>
        Task<ResponseDto<bool>> DeleteCustomerAsync(string NationalId);

        /// <summary>
        /// Update customer details
        /// </summary>
        /// <param name="nationalId"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        Task<ResponseDto<bool>> UpdateCustomerDetailsAsync(string nationalId, CustomerUpdateRequestDto details);
    }
}
