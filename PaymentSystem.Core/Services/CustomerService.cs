using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
            try
            {
                if (string.IsNullOrWhiteSpace(NationalId))
                    return ResponseDto<CustomerResponseDto>.Fail("Invalid credentials", (int)HttpStatusCode.BadRequest);

                _logger.LogInformation($"Getting customer details...NationalId: {NationalId}");
                var customer = await _unitOfWork.Customer.GetByNationalId(NationalId);
                if (customer == null)
                    return ResponseDto<CustomerResponseDto>.Fail("customer does not exist", (int)HttpStatusCode.NotFound);

                var customerDetails = _mapper.Map<CustomerResponseDto>(customer);
                _logger.LogInformation($"Successful Customer Details: {JsonConvert.SerializeObject(customerDetails)}");

                return ResponseDto<CustomerResponseDto>.Success("", customerDetails);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                await _unitOfWork.Rollback();
                throw;
            }
        }

        /// <summary>
        /// Insert customer details
        /// </summary>
        /// <param name="customerDetails"></param>
        /// <returns></returns>
        public async Task<ResponseDto<bool>> InsertCustomerAsync(CustomerRequestDto customerDetails)
        {
            try
            {
                if (_unitOfWork.Customer.Count(x => x.NationalId == customerDetails.NationalId)> 0)
                {
                    _logger.LogError($"Customer with National Id: {customerDetails.NationalId} already exist!");
                    return ResponseDto<bool>.Fail("Customer already Exists", (int)HttpStatusCode.NotFound);
                }
                var userModel = _mapper.Map<Customer>(customerDetails);

                var insertDetails = _unitOfWork.Customer.InsertAsync(userModel);
                await _unitOfWork.Save();

                _logger.LogInformation($"Customer detials successfully saved: {JsonConvert.SerializeObject(insertDetails)}");
                return ResponseDto<bool>.Success("Customer details Succesfully saved", true, (int)HttpStatusCode.Created);
            }
            catch (Exception exception)
            {
                await _unitOfWork.Rollback();
                _logger.LogError(exception, exception.Message);
                throw;
            }
        }

        public async Task<ResponseDto<bool>> UpdateCustomerDetailsAsync(CustomerEditRequestDto details)
        {
            try
            {
                if (_unitOfWork.Customer.Count(x => x.NationalId == details.NationalId) < 1)
                {
                    _logger.LogError($"Customer with NationalId: {details.NationalId} not found!");
                    return ResponseDto<bool>.Fail("Customer not found!", (int)HttpStatusCode.NotFound);
                }

                var userModel = _mapper.Map<Customer>(details);

                _unitOfWork.Customer.Update(userModel);
                await _unitOfWork.Save();

                return ResponseDto<bool>.Success("Customer details updated Succesfully", true, (int)HttpStatusCode.OK);
            }
            catch (Exception exception)
            {
                await _unitOfWork.Rollback();
                _logger.LogError(exception, exception.Message);
                throw;
            }
        }

        /// <summary>
        /// delete customer
        /// </summary>
        /// <param name="nationalId"></param>
        /// <returns></returns>
        public async Task<ResponseDto<bool>> DeleteCustomerAsync(string nationalId)
        {
            try
            {
                if (_unitOfWork.Customer.Count(x => x.NationalId == nationalId) < 1)
                {
                    _logger.LogError($"Customer with NationalId: {nationalId} not found!");
                    return ResponseDto<bool>.Fail("Customer not found!", (int)HttpStatusCode.NotFound);
                }

                await _unitOfWork.Customer.DeleteCustomerByNationalId(nationalId);
                await _unitOfWork.Save();

                _logger.LogError($"Customer with national Id: {nationalId} deleted successfully");
                return ResponseDto<bool>.Success("Customer deleted Succesfully", true, (int)HttpStatusCode.OK);
            }
            catch (Exception exception)
            {
                await _unitOfWork.Rollback();
                _logger.LogError(exception, exception.Message);
                throw;
            }
        }
    }
}
