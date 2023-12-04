using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentSystem.Core.Interfaces
{
    public interface IUnitOfWork
    {
        ICustomerRepository UserRepository { get; }

        Task Commit();
        Task CreateTransaction();
        void Dispose();
        Task Rollback();
        Task Save();
    }
}
