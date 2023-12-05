using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PaymentSystem.Core.Interfaces;
using PaymentSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentSystem.Infrastructure.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly CustomerDbContext _dbContext;
        private readonly DbSet<Customer> _db;
        public CustomerRepository(CustomerDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _db = dbContext.Set<Customer>();
        }

        /// <summary>
        /// Get customer by National Id
        /// </summary>
        /// <param name="NationalId"></param>
        /// <returns></returns>
        public async Task<Customer?> GetByNationalId(string nationalId)
        {
            try
            {
                return await _dbContext.Customer.Where(x => x.NationalId == nationalId).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Delet Customer by national Id
        /// </summary>
        /// <param name="NationalId"></param>
        /// <returns></returns>
        public async Task DeleteCustomerByNationalId(string nationalId)
        {
            try
            {
                _db.Remove(await _dbContext.Customer.Where(x => x.NationalId == nationalId).FirstOrDefaultAsync());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
