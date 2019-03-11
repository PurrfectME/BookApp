using Microsoft.Extensions.Configuration;

namespace BLL.AppStart
{
    public class ConfigurationSettings
    {
        private readonly IConfiguration _configuration;


        public ConfigurationSettings(IConfiguration configuration)
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
