using AutoMapper;
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
    public class MerchantService : IMerchantService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MerchantService> _logger;

        public MerchantService(IMapper mapper, IUnitOfWork unitOfWork, ILogger<MerchantService> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Get merchant details
        /// </summary>
        /// <param name="merchantNumber"></param>
        /// <returns></returns>
        public async Task<ResponseDto<MerchantResponseDto>> GetMerchantAsync(long merchantNumber)
        {
            try
            {

                _logger.LogInformation($"Getting customer details...NationalId: {merchantNumber}");
                var merchant = await _unitOfWork.Merchant.GetByMerchantNumber(merchantNumber);
                if (merchant == null)
                    return ResponseDto<MerchantResponseDto>.Fail("customer does not exist", (int)HttpStatusCode.NotFound);

                var merchantDetails = _mapper.Map<MerchantResponseDto>(merchant);
                _logger.LogInformation($"Successful merchant Details: {JsonConvert.SerializeObject(merchantDetails)}");

                await _unitOfWork.Commit();
                return ResponseDto<MerchantResponseDto>.Success("", merchantDetails);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                await _unitOfWork.Rollback();
                return ResponseDto<MerchantResponseDto>.Fail("An error occurred", (int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Insert merchant details
        /// </summary>
        /// <param name="merchantDetails"></param>
        /// <returns></returns>
        public async Task<ResponseDto<bool>> InsertMerchantAsync(MerchantRequestDto merchantDetails)
        {
            try
            {
                if (_unitOfWork.Merchant.CountAsync(x => x.MerchantNumber == merchantDetails.MerchantNumber) > 0)
                {
                    _logger.LogError($"Merchant with merchant Id: {merchantDetails.MerchantNumber} already exist!");
                    return ResponseDto<bool>.Fail("Merchant already Exists", (int)HttpStatusCode.NotFound);
                }
                var merchantModel = _mapper.Map<Merchant>(merchantDetails);

                var insertDetails = _unitOfWork.Merchant.InsertAsync(merchantModel);
                await _unitOfWork.Save();

                _logger.LogInformation($"Merchant detials successfully saved: {JsonConvert.SerializeObject(merchantModel)}");
                return ResponseDto<bool>.Success("Merchant details Succesfully saved", true, (int)HttpStatusCode.Created);
            }
            catch (Exception exception)
            {
                await _unitOfWork.Rollback();
                _logger.LogError(exception, exception.Message);
                return ResponseDto<bool>.Fail("An error occurred", (int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Update merchant details
        /// </summary>
        /// <param name="merchantNumber"></param>
        /// <param name="updateDetails"></param>
        /// <returns></returns>
        public async Task<ResponseDto<bool>> UpdateMerchantDetailsAsync(long merchantNumber, MerchantUpdateRequestDto updateDetails)
        {
            try
            {
                var existingMerchant = await _unitOfWork.Merchant.GetByMerchantNumber(merchantNumber);

                if (_unitOfWork.Merchant.CountAsync(x => x.MerchantNumber == merchantNumber) < 1)
                {
                    _logger.LogError($"Merchant with NationalId: {merchantNumber} not found!");
                    return ResponseDto<bool>.Fail("Merchant not found!", (int)HttpStatusCode.NotFound);
                }

                _mapper.Map(updateDetails, existingMerchant);

                _unitOfWork.Merchant.Update(existingMerchant);
                await _unitOfWork.Save();

                return ResponseDto<bool>.Success("Merchant details updated Succesfully", true, (int)HttpStatusCode.OK);
            }
            catch (Exception exception)
            {
                await _unitOfWork.Rollback();
                _logger.LogError(exception, exception.Message);
                return ResponseDto<bool>.Fail("An error occurred", (int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Delete merchant
        /// </summary>
        /// <param name="merchantNumber"></param>
        /// <returns></returns>
        public async Task<ResponseDto<bool>> DeleteMerchantAsync(long merchantNumber)
        {
            try
            {
                if (_unitOfWork.Merchant.CountAsync(x => x.MerchantNumber == merchantNumber) < 1)
                {
                    _logger.LogError($"Merchant with merchant number: {merchantNumber} not found!");
                    return ResponseDto<bool>.Fail("Customer not found!", (int)HttpStatusCode.NotFound);
                }

                await _unitOfWork.Merchant.DeleteMerchantByMerchantNumber(merchantNumber);
                await _unitOfWork.Save();

                _logger.LogError($"Merchant with merchant number: {merchantNumber} deleted successfully");
                return ResponseDto<bool>.Success("Merchant deleted Succesfully", true, (int)HttpStatusCode.OK);
            }
            catch (Exception exception)
            {
                await _unitOfWork.Rollback();
                _logger.LogError(exception, exception.Message);
                return ResponseDto<bool>.Fail("An error occurred", (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
