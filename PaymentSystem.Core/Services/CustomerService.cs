using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using PaymentSystem.Core.DTOs;
using PaymentSystem.Core.Interfaces;
using PaymentSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PaymentSystem.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(IMapper mapper, IUnitOfWork unitOfWork, ILogger<CustomerService> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Get Customer details
        /// </summary>
        /// <param name="NationalId"></param>
        /// <returns></returns>
        public async Task<ResponseDto<CustomerResponseDto>> GetCustomerAsync(string NationalId)
        {
            if (string.IsNullOrWhiteSpace(NationalId))
                return ResponseDto<CustomerResponseDto>.Fail("Invalid credentials", (int)HttpStatusCode.BadRequest);

            _logger.LogInformation($"Getting customer details...NationalId: {NationalId}");
            var customer = await _unitOfWork.Customer.Get(NationalId);
            if (customer == null)
                return ResponseDto<CustomerResponseDto>.Fail("customer does not exist");

            var customerDetails = _mapper.Map<CustomerResponseDto>(customer);
            _logger.LogInformation($"Successful Customer Details: {customerDetails}");

            return ResponseDto<CustomerResponseDto>.Success("", customerDetails);
        }

        /// <summary>
        /// Insert customer details
        /// </summary>
        /// <param name="customerDetails"></param>
        /// <returns></returns>
        public async Task<ResponseDto<bool>> InsertCustomerAsync(CustomerRequestDto customerDetails)
        {
            var checkEmail = await _unitOfWork.Customer.Get(customerDetails.NationalId);
            if (checkEmail != null)
            {
                _logger.LogInformation($"Customer with National Id: {customerDetails.NationalId} already exist!");
                return ResponseDto<bool>.Fail("Customer already Exists", (int)HttpStatusCode.BadRequest);
            }
            var userModel = _mapper.Map<Customer>(customerDetails);

            var insertDetails = _unitOfWork.Customer.InsertAsync(userModel);
            await _unitOfWork.Save();

            _logger.LogInformation($"Customer detials successfully saved: {insertDetails}");
            return ResponseDto<bool>.Success("Customer details Succesfully saved", true, (int)HttpStatusCode.Created);
        }

        public async Task<bool> UpdateCustomerDetailsAsync(CustomerRequestDto customerDetails)
        {
            return true;
        }

        
        public async Task<bool> DeleteCustomerAsync(CustomerRequestDto customerDetails)
        {
            return true;
        }
    }
}
