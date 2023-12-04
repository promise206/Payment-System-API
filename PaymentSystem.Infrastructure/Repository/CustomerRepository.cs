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
        private readonly CustomerDbContext _context;
        private readonly DbSet<Customer> _db;
        public CustomerRepository(CustomerDbContext context) : base(context)
        {
            _context = context;
            _db = context.Set<Customer>();
        }
    }
}
