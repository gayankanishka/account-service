using System.Collections.Generic;
using System.Threading.Tasks;

namespace Account.Service.Core
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<bool> Add(T entity);
        Task<T> GetById(int id);
        Task<bool> Update(T entity);
        Task<bool> Delete(int id);
    }
}
