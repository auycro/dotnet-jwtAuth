using dotnet_jwtAuth.Models;
using dotnet_jwtAuth.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace dotnet_jwtAuth.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController: ControllerBase
    {
      private readonly ILogger<AuthenticationController> _logger;
      private IAuthenticationService _authenticationService;

      public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationService authentication){
        _logger = logger;
        _authenticationService = authentication;
      }

      [Route("/authenticate")]
      [HttpPost]
      public IActionResult Authenticate(AuthenticateRequest model)
      {
        var response = _authenticationService.Authenticate(model);

        if (response == null)
          return BadRequest(new { message = "Username or password is incorrect" });

        return Ok(response);
      }
    }
}