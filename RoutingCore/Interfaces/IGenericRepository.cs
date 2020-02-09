using WebServiceDemo.Core.Specifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebServiceDemo.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetById(object id, IEnumerable<string> includes = null);
        Task<IEnumerable<T>> Find(
            Specification<T> specification = null,
            IEnumerable<string> includes = null, 
            int page = 0, 
            int pageSize = 0);
        Task<IEnumerable<T>> GetAll();
        Task Insert(T obj);
        Task Update(T obj);
        Task Delete(object id);
        Task Save();
    }
}
