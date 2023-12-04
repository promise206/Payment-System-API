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
    }
}
