using BLL.Interfaces;
using Microsoft.Extensions.Configuration;

namespace BLL.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _configuration;


        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public string TokenSecretKey => _configuration["JwtTokenConfiguration:SecretKey"];
        public string TokenIssuer => _configuration["JwtTokenConfiguration:Issuer"];
        public string TokenAudience => _configuration["JwtTokenConfiguration:Audience"];
        public string TokenLifetime => _configuration["JwtTokenConfiguration:Lifetime"];

        public string ConnectionString => _configuration.GetConnectionString("ES");
    }
}
