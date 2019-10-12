using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Service.Core
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> SelectAll();
        Task<bool> InsertSingle(T entity);
        Task<T> SelectById(T entity);
        Task<bool> UpdateSingle(T entity);
        Task<bool> DeleteSingle(T entity);
    }
}
