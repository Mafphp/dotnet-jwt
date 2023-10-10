using DDD.Application.Authentication;
using DDD.Application.Common.Interfaces.Authentication;
using DDD.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace DDD.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{

    public readonly IAuthenticationService _authenticationService;
    public readonly IJwtTokenGenerator _jwtTokenGenerator;


    public AuthenticationController(IAuthenticationService authenticationService, IJwtTokenGenerator jwtTokenGenerator)
    {
        _authenticationService = authenticationService;
        _jwtTokenGenerator = jwtTokenGenerator;

    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var authResult = _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);

        var userId = Guid.NewGuid();
        var token = _jwtTokenGenerator.GenerateToken(userId, request.FirstName, request.LastName);
        var response = new AuthenticationResponse(
            userId,
            authResult.FirstName,
            authResult.LastName,
            authResult.Email,
            token
        );
        return Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var authResult = _authenticationService.Login(request.Email, request.Password);

        var response = new AuthenticationResponse(
            authResult.Id,
            authResult.FirstName,
            authResult.LastName,
            authResult.Email,
            authResult.Token
        );
        return Ok(response);
    }
}