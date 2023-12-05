using PaymentSystem.Core.Interfaces;
using PaymentSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PaymentSystem.Infrastructure.Interfaces
{
    public interface IMerchantRepository : IGenericRepository<Merchant>
    {
        // <summary>
        /// Get merchant by number
        /// </summary>
        /// <param name="MerchantNumber"></param>
        /// <returns></returns>
        Task<Merchant?> GetByMerchantNumber(long merchantNumber);

        /// <summary>
        /// Delete merchant
        /// </summary>
        /// <param name="merchantNumber"></param>
        /// <returns></returns>
        Task DeleteMerchantByMerchantNumber(long merchantNumber);
    }
}
