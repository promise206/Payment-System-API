using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentSystem.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task InsertAsync(T entity);
        Task DeleteAsync(string id);
        void DeleteRangeAsync(IEnumerable<T> entities);
        void Update(T item);
    }
}
