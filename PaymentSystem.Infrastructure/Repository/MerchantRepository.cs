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
        public MerchantRepository(CustomerDbContext context) : base(context)
        {
        }
    }
}
