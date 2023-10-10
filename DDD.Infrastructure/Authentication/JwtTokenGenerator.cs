using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DDD.Application.Common.Interfaces.Authentication;
using DDD.Application.Common.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;

namespace DDD.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public string GenerateToken(Guid userId, string firstName, string lastName)
    {


        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes("longer-super-secret-phrase-for-key")),
            SecurityAlgorithms.HmacSha256
        );

        var claims = new[]{
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, firstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

        };

        var securityToken = new JwtSecurityToken(
            issuer: "DDD",
            expires: _dateTimeProvider.UtcNow.AddMinutes(60),
            claims: claims,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}