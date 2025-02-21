using EmployeeManagement.Application.DTOs.KorisnickiNalog;
using EmployeeManagement.Application.Identity;
using EmployeeManagement.Application.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeeManagement.API.Controllers;

[ApiController]
[AllowAnonymous]
[Route("/api")]
public class KorisnickiNalogController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ILogger<KorisnickiNalogController> _logger;
    private readonly IJwtService _jwtService;

    public KorisnickiNalogController(UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager,
        ILogger<KorisnickiNalogController> logger, IJwtService jwtService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _logger = logger;
        _jwtService = jwtService;
    }

    [HttpPost("registracija")]
    public async Task<IActionResult> Register([FromBody] RegistracijaDTO registracijaDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (await EmailVecRegistrovan(registracijaDTO.Email))
        {
            _logger.LogInformation("Neuspesna registracija jer korisnik sa datom email adresom vec postoji.");

            return Conflict(new { errorMsg = "Korisnik sa tom email adresom vec postoji u sistemu" });
        }

        ApplicationUser user = new ApplicationUser()
        {
            PersonName = registracijaDTO.ImeKorisnika,
            Email = registracijaDTO.Email,
            UserName = registracijaDTO.Email
        };

        var result = await _userManager.CreateAsync(user, registracijaDTO.Lozinka);

        if (result.Succeeded)
        {
            _logger.LogInformation("Korisnik je uspesno kreiran.");

            await _signInManager.SignInAsync(user, isPersistent: false);

            var autentikacioniOdgovor = _jwtService.KreirajJwtToken(user);

            _logger.LogInformation("Korisnik je uspesno prijavljen.");

            user.RefreshToken = autentikacioniOdgovor.RefreshToken;
            user.RefreshTokenExpirationDate = autentikacioniOdgovor.VremeIstekaRefreshTokena;

            await _userManager.UpdateAsync(user);

            return Ok(autentikacioniOdgovor);
        }
        else
        {
            string errorMessages = string.Join(" | ",
                result.Errors.Select(e => e.Description));

            _logger.LogInformation("Korisnik nije uspesno kreiran.");

            return Problem(errorMessages);
        }
    }

    [HttpPost("prijava")]
    public async Task<IActionResult> Login(PrijavaDTO prijavaDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _signInManager.PasswordSignInAsync(prijavaDTO.Email, prijavaDTO.Lozinka,
            isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            _logger.LogInformation("Korisnik je uspesno ulogovan.");

            var user = await _userManager.FindByEmailAsync(prijavaDTO.Email);

            var autentikacioniOdgovor = _jwtService.KreirajJwtToken(user);

            user.RefreshToken = autentikacioniOdgovor.RefreshToken;
            user.RefreshTokenExpirationDate = autentikacioniOdgovor.VremeIstekaRefreshTokena;

            await _userManager.UpdateAsync(user);

            return Ok(autentikacioniOdgovor);
        }

        _logger.LogInformation("Neuspesna prijava.");

        return Unauthorized();
    }

    [HttpPost("odjava")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        _logger.LogInformation("Korisnik je odjavljen. ");

        return NoContent();
    }

    [HttpPost("generisi-novi-token")]
    public async Task<IActionResult> GenerisiNoviToken(TokenModel tokenModel)
    {
        if(tokenModel is null)
        {
            return BadRequest();
        }

        ClaimsPrincipal? principal;
        
        try
        {
            principal = _jwtService.VratiInformacijeIzTokena(tokenModel.JwtToken);

        }
        catch (Exception)
        {
            return BadRequest("Los jwt token");
        }

        if(principal == null)
        {
            return BadRequest("Nevalidan jwt token");
        }

        string? email = principal.FindFirstValue(ClaimTypes.Email);

        ApplicationUser? user = await _userManager.FindByEmailAsync(email);
    
        if(user == null || user.RefreshToken != tokenModel.RefreshToken || 
            user.RefreshTokenExpirationDate <= DateTime.UtcNow)
        {
            return BadRequest("Nevalidan refresh token");
        }

        var autentikacioniOdgovor = _jwtService.KreirajJwtToken(user);

        user.RefreshToken = autentikacioniOdgovor.RefreshToken;
        user.RefreshTokenExpirationDate = autentikacioniOdgovor.VremeIstekaRefreshTokena;

        await _userManager.UpdateAsync(user);

        return Ok(autentikacioniOdgovor);
    }

    private async Task<bool> EmailVecRegistrovan(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if(user is null)
        {
            return false;
        }

        return true;
    }
}
