using Microsoft.EntityFrameworkCore;
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
        public async Task<Customer?> Get(string NationalId)
        {
            return await _dbContext.Customer.Where(x => x.Id == NationalId).FirstOrDefaultAsync();
        }
    }
}
