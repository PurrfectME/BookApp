using System.Threading.Tasks;
using BLL.Interfaces;
using DAL.Context;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace BLL.Tests
{
    public class TokenServiceTest
    {
        private DbContextOptions<ApplicationContext> Options { get; } = new DbContextOptionsBuilder<ApplicationContext>().Options;


        [Fact]
        public async Task GenerateToken()
        {
            var contextMock = new DbContextMock<ApplicationContext>(Options);


            var serviceMock = new Mock<ITokenService>();
        }
    }
}
