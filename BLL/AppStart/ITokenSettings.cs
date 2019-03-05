namespace BLL.AppStart
{
    public interface ITokenSettings
    {
        string TokenSecretKey { get; }
        string TokenIssuer { get; }
        string TokenAudience { get; }
        string TokenLifetime { get; }

    }
}

