using System.Collections.Generic;

namespace BLL.DataAccess
{
    public interface IRepository<in T> where T : class
    {
        void CreateSync(T entity);
        void DeleteSync(T entity);
        void UpdateSync(T entity);
        
        void CreateSync(IEnumerable<T> entities);
        void DeleteSync(IEnumerable<T> entities);
        void UpdateSync(IEnumerable<T> entities);
    }
}
