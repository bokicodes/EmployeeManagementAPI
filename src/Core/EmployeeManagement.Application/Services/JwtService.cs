using EmployeeManagement.Application.DTOs.KorisnickiNalog;
using EmployeeManagement.Application.Identity;
using EmployeeManagement.Application.ServiceInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EmployeeManagement.Application.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public AutentikacioniOdgovor KreirajJwtToken(ApplicationUser user)
    {
        DateTime vremeIstekaTokena = DateTime.UtcNow
            .AddMinutes(Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTES"]));

        Claim[] claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Email),
            new Claim(ClaimTypes.Email, user.Email)
        };


        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt_EmployeeManagement_SecretKey"]));


        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken tokenGenerator = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"], 
            claims,
            expires: vremeIstekaTokena,
            signingCredentials: signingCredentials);

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        
        string token = tokenHandler.WriteToken(tokenGenerator);

        return new AutentikacioniOdgovor()
        {
            Email = user.Email,
            JwtToken = token,
            VremeIstekaJwtTokena = vremeIstekaTokena,
            RefreshToken = GenerisiRefreshToken(),
            VremeIstekaRefreshTokena = DateTime.UtcNow.AddMinutes(
                Convert.ToInt32(_configuration["RefreshToken:EXPIRATION_MINUTES"]))
        };
    }

    private static string GenerisiRefreshToken()
    {
        Byte[] bytes = new byte[64];
        var randomNumberGen = RandomNumberGenerator.Create();

        randomNumberGen.GetBytes(bytes);

        return Convert.ToBase64String(bytes);
    }

    public ClaimsPrincipal? VratiInformacijeIzTokena(string? token)
    {
        try
        {
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Audience"],
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt_EmployeeManagement_SecretKey"]))
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            ClaimsPrincipal principal = jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Nevalidan token");
            }

            return principal;
        }
        catch (Exception)
        {
            throw;
        }
        
    }
}
