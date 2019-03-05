using System.Collections.Generic;
using BLL.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _entity;


        public Repository(DbSet<T> entity)
        {
            _entity = entity;
        }


        public void CreateSync(T entity)
        {
            _entity.Add(entity);
        }

        public void DeleteSync(T entity)
        {
            _entity.Remove(entity);
        }

        public void UpdateSync(T entity)
        {
            _entity.Update(entity);

            //_entity.Attach(entity);
            //_dbContext.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void CreateSync(IEnumerable<T> entities)
        {
            _entity.AddRange(entities);
        }

        public void DeleteSync(IEnumerable<T> entities)
        {
            _entity.RemoveRange(entities);
        }

        public void UpdateSync(IEnumerable<T> entities)
        {
            _entity.UpdateRange(entities);
        }
    }
}
