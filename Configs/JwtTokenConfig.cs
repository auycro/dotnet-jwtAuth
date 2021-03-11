namespace dotnet_jwtAuth.Configs
{
    public class JwtTokenConfig
    {
      public string Secret { get; set; }
      public string Issuer { get; set; }
      public string Audience { get; set; }
      public int AccessTokenExpiration { get; set; }        
    }
}