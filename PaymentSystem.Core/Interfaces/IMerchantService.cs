using PaymentSystem.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentSystem.Core.Interfaces
{
    public interface IMerchantService
    {
        /// <summary>
        /// Get merchant details
        /// </summary>
        /// <param name="merchantNumber"></param>
        /// <returns></returns>
        Task<ResponseDto<MerchantResponseDto>> GetMerchantAsync(long merchantNumber);

        /// <summary>
        /// Insert merchant details
        /// </summary>
        /// <param name="merchantDetails"></param>
        /// <returns></returns>
        Task<ResponseDto<bool>> InsertMerchantAsync(MerchantRequestDto merchantDetails);

        /// <summary>
        /// Update merchant details
        /// </summary>
        /// <param name="merchantNumber"></param>
        /// <param name="updateDetails"></param>
        /// <returns></returns>
        Task<ResponseDto<bool>> UpdateMerchantDetailsAsync(long merchantNumber, MerchantUpdateRequestDto updateDetails);

        /// <summary>
        /// Delete merchant
        /// </summary>
        /// <param name="merchantNumber"></param>
        /// <returns></returns>
        Task<ResponseDto<bool>> DeleteMerchantAsync(long merchantNumber);
    }
}
