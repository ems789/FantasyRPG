using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace API_Game_Server.Services;

public class JwtTokenService
{
    IConfiguration _config;

    public JwtTokenService(IConfiguration config)
    {
        _config = config;
    }

    public string CreateJwtAccessToken(long accountDbId)
    {
        long now = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
        long expired = now + 60;

        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, accountDbId.ToString()), // Subject
            new Claim(JwtRegisteredClaimNames.Iat, now.ToString()), // Issued At
            new Claim(JwtRegisteredClaimNames.Exp, expired.ToString()), // Expiration
        };
       

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])),
            SecurityAlgorithms.HmacSha256);

        var jwt = new JwtSecurityToken(claims: claims, signingCredentials: credentials);
        string token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return token;
    }

    public bool ValidateJwtAccessToken(string token, string key)
    {
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

        TokenValidationParameters validationParams = new TokenValidationParameters()
        {
            ValidateLifetime = true,
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
        };

        SecurityToken validateToken;
        try
        {
            var claims = handler.ValidateToken(token, validationParams, out validateToken);
            return true;
        }
        catch
        {
            return false;
        }
    }
}