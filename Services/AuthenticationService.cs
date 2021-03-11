using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using dotnet_jwtAuth.Entities;
using dotnet_jwtAuth.Models;
using Microsoft.IdentityModel.Tokens;

namespace dotnet_jwtAuth.Services
{
    public interface IAuthenticationService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest request);
        //AuthenticateResponse RefreshToken(RefreshTokenRequest request);
        //IEnumerable<User> GetAll();
        //User GetById(int id);
    }
  
    public class AuthenticationService: IAuthenticationService
    {
      private IAuthenticationService _authenService;

      //Temporary
      private List<User> _users = new List<User>
      {
          new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" },
          new User { Id = 2, FirstName = "Foo", LastName = "Bar", Username = "foobar", Password = "password" }
      };   

      public AuthenticateResponse Authenticate(AuthenticateRequest request)
      {
          var user = _users.SingleOrDefault(x => x.Username == request.Username && x.Password == request.Password);

          // return null if user not found
          if (user == null) return null;

          // authentication successful so generate jwt token
          var token = generateJwtToken(user);

          return new AuthenticateResponse(user, token);
      }

      private const string Secret = "my-ultra-secure-and-ultra-long-secret";
      private string generateJwtToken(User user)
      {
        // generate token that is valid for 1 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Secret); //SomeSecrets
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = @"auycro",
            Audience = @"https://localhost:5001",
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);        
      }
    }
}