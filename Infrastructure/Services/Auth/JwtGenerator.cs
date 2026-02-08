using Domain.Entities;
using Infrastructure.Abstractions.Interfaces.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services.Auth;

public class JwtGenerator : IJwtGenerator
{
    private readonly IConfiguration _config;
    private readonly string _key;
    public JwtGenerator(IConfiguration config)
    {
        _config = config;
        _key = _config["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key пустой в конфигурации");
    }
    public string? GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                int.Parse(_config["Jwt:Expires"] ?? "30")
            ),
            audience: _config["Jwt:Audience"],
            issuer: _config["Jwt:Issuer"],
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}