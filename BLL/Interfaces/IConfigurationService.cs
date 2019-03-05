namespace BLL.Interfaces
{
    public interface IConfigurationService
    {
        string TokenSecretKey { get; }
        string TokenIssuer { get; }
        string TokenAudience { get; }
        string TokenLifetime { get; }

        string ConnectionString { get; }
    }
}



