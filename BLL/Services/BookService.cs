using System.Threading.Tasks;
using BLL.Entities;
using EffectiveSoft.Data.Core;

namespace BLL.Services
{
    public class BookService
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Book> _repository;
        private readonly IFinder<Book> _finder;


        public BookService(IUnitOfWork uow, IRepository<Book> repository, IFinder<Book> finder)
        {
            _uow = uow;
            _repository = repository;
            _finder = finder;
        }


        public async Task Create(Book bookToCreate)
        {
            _repository.Create(bookToCreate);
            await _uow.Commit();
        }
    }
}
