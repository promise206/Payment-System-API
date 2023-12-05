using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PaymentSystem.Domain.Entities;
using PaymentSystem.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PaymentSystem.Infrastructure.Repository
{
    public class MerchantRepository : GenericRepository<Merchant>, IMerchantRepository
    {
        private readonly CustomerDbContext _dbContext;
        private readonly DbSet<Merchant> _db;
        public MerchantRepository(CustomerDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _db = dbContext.Set<Merchant>();
        }

        /// <summary>
        /// Get merchant by number
        /// </summary>
        /// <param name="MerchantNumber"></param>
        /// <returns></returns>
        public async Task<Merchant?> GetByMerchantNumber(long merchantNumber)
        {
            try
            {
                return await _dbContext.Merchant.Where(x => x.MerchantNumber == merchantNumber).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Delete merchant
        /// </summary>
        /// <param name="merchantNumber"></param>
        /// <returns></returns>
        public async Task DeleteMerchantByMerchantNumber(long merchantNumber)
        {
            try
            {
                _db.Remove(await _dbContext.Merchant.Where(x => x.MerchantNumber == merchantNumber).FirstOrDefaultAsync());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
