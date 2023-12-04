using Microsoft.EntityFrameworkCore.Storage;
using PaymentSystem.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentSystem.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private bool _disposedValue;
        private readonly CustomerDbContext _context;
        private IDbContextTransaction _objTransaction;
        private ICustomerRepository _userRepository;
        public UnitOfWork(CustomerDbContext context)
        {
            _context = context;
        }

        public ICustomerRepository UserRepository => _userRepository ??= new CustomerRepository(_context);

        public async Task CreateTransaction()
        {
            _objTransaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            await _objTransaction.CommitAsync();
        }

        public async Task Rollback()
        {
            await _objTransaction?.RollbackAsync();
            await _objTransaction.DisposeAsync();
        }


        public async Task Save()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
