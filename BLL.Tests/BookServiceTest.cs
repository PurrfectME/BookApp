using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BLL.Entities;
using BLL.Services;
using EffectiveSoft.Data.Core;
using Moq;
using Xunit;

namespace BLL.Tests
{
    public class BookServiceTest
    {
        [Fact]
        public async Task CreateTest()
        {
            var book = new Book {Id = Guid.NewGuid(), Name = "book"};

            var uowMock = new Mock<IUnitOfWork>();
            var repoMock = new Mock<IRepository<Book>>();
            var findMock = new Mock<IFinder<Book>>();

            repoMock.Setup(x => x.Create(book));
            

            var service = new BookService(uowMock.Object, repoMock.Object, findMock.Object);

            await service.Create(book);

            repoMock.Verify(x => x.Create(It.IsAny<Book>()), Times.Once);
            uowMock.Verify(x => x.Commit(It.IsAny<CancellationToken>()), Times.Once);

        }
    }
}
