using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DAL.Context;
using EffectiveSoft.Data.Core;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace DAL.Tests
{
    public class ContextTest : BaseTest
    {
        private DbContextOptions<ApplicationContext> Options { get; } = new DbContextOptionsBuilder<ApplicationContext>().Options;


        [Fact]
        public async void TestCreatingContext()
        {
            //arrange
            var contextMock = new DbContextMock<ApplicationContext>(Options);
            contextMock.Setup(x => x.SaveChangesAsync(CancellationToken.None)).Returns(Task.FromResult(new int()));
            var unitOfWork = new UnitOfWork(contextMock.Object);

            //var r = new ApplicationContext(options);
            
            //act
            //await unitOfWork.Save();

            //assert
            contextMock.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once);
        }
    }
}
