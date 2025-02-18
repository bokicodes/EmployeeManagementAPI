using EmployeeManagement.Application.DTOs.KorisnickiNalog;
using EmployeeManagement.Application.Identity;
using EmployeeManagement.Application.ServiceInterfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
            new Claim(ClaimTypes.NameIdentifier, user.Email)
        };


        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));


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
            Token = token,
            VremeIstekaTokena = vremeIstekaTokena,
            Email = user.Email
        };
    }
}
