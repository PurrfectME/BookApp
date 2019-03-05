using System.Linq;

namespace BLL.DataAccess
{
    public interface IFinder<out T> where T : class
    {
        IQueryable<T> Find();
    }
}
