using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PaymentSystem.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PaymentSystem.Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly CustomerDbContext _context;
        private readonly DbSet<T> _db;
        public GenericRepository(CustomerDbContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }
        /// <summary>
        /// Deletes an Object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(string id)
        {
            try
            {
                _db.Remove(await _db.FindAsync(id));
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Deletes List of objects
        /// </summary>
        /// <param name="entities"></param>
        public void DeleteRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                _db.RemoveRange(entities);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Inserts An Object
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task InsertAsync(T entity)
        {
            try
            {
                await _db.AddAsync(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(T item)
        {
            try
            {
                // attaches instance to the contex
                // t, then sets the state
                // as modified
                _db.Attach(item);
                _context.Entry(item).State = EntityState.Modified;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int CountAsync(Expression<Func<T, bool>> lambda)
        {
            try
            {
                return _db.Count(lambda);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
