#nullable disable

namespace PortalGalaxy.Common.Configuration;

public class AppSettings
{
    public Jwt Jwt { get; set; }
}

public class Jwt
{
    public string SecretKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}