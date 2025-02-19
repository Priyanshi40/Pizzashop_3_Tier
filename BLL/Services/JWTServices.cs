using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DAL.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


namespace BLL.Services;

public class JWTServices
{
    private readonly string _secretKey;
    private readonly int _tokenExpiry;
    private readonly int _rememberMeExpiryDays;
    private readonly string _issuer;

    public JWTServices(IConfiguration config)
    {
        _secretKey = config["Jwt:Key"];
        _issuer = config["Jwt:Issuer"];
        _tokenExpiry = int.Parse(config["Jwt:TokenExpiry"]);
        _rememberMeExpiryDays = int.Parse(config["Jwt:RememberMeExpiryDays"]);
    }

    public string GenerateToken(string email, string rememberMe)
    // public string GenerateToken(string email)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        // var expiry = rememberMe!= null ? DateTime.UtcNow.AddDays(_rememberMeExpiryDays) : DateTime.UtcNow.AddMinutes(_tokenExpiry);
        var expiry = rememberMe=="True" ? DateTime.UtcNow.AddDays(_rememberMeExpiryDays) : DateTime.UtcNow.AddMinutes(_tokenExpiry);
        // var expiry = DateTime.UtcNow.AddMinutes(_tokenExpiry);

        Console.WriteLine("Service : " + expiry);
        var authClaims = new List<Claim> {
            new("Remember", rememberMe),
            new(JwtRegisteredClaimNames.Email, email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _issuer,
            expires: expiry,
            claims: authClaims,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
