using System.Linq;
using BLL.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class Finder<T> : IFinder<T> where T : class
    {
        private readonly DbSet<T> _entity;


        public Finder(DbSet<T> entity)
        {
            _entity = entity;
        }


        public IQueryable<T> Find()
        {
            return _entity.AsQueryable();
        }
    }
}
