using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using PaymentSystem.Core.Interfaces;
using PaymentSystem.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentSystem.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposedValue;
        private readonly CustomerDbContext _context;
        private IDbContextTransaction _objTransaction;
        private CustomerRepository _customer;
        private MerchantRepository _Merchant;
        private readonly ILogger<UnitOfWork> _logger;

        public UnitOfWork(CustomerDbContext context, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
            _objTransaction = _context.Database.BeginTransaction();
        }

        public ICustomerRepository Customer => _customer ??= new CustomerRepository(_context);
        public IMerchantRepository Merchant => _Merchant ??= new MerchantRepository(_context);

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
            if (_objTransaction != null)
            {
                await _objTransaction.RollbackAsync();
                await _objTransaction.DisposeAsync();
            }
        }


        public async Task Save()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);
                throw;
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
