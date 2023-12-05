using PaymentSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentSystem.Core.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        /// <summary>
        /// Get customer by National Id
        /// </summary>
        /// <param name="NationalId"></param>
        /// <returns></returns>
        Task<Customer?> GetByNationalId(string nationalId);

        /// <summary>
        /// Delet Customer by national Id
        /// </summary>
        /// <param name="NationalId"></param>
        /// <returns></returns>
        Task DeleteCustomerByNationalId(string nationalId);
    }
}
