using System.ComponentModel.DataAnnotations;

namespace dotnet_jwtAuth.Models
{
    public class AuthenticateRequest
    {
      [Required]
      public string Username { get; set; }

      [Required]
      public string Password { get; set; }
    }
}