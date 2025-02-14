using EmployeeManagement.Application.DTOs.KorisnickiNalog;
using EmployeeManagement.Application.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers;

[ApiController]
[Route("/api")]
public class KorisnickiNalogController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ILogger<KorisnickiNalogController> _logger;

    public KorisnickiNalogController(UserManager<ApplicationUser> userManager, 
        SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager,
        ILogger<KorisnickiNalogController> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _logger = logger;
    }

    [HttpPost("registracija")]
    public async Task<IActionResult> Register(RegistracijaDTO registracijaDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (await IsEmailAlreadyRegistered(registracijaDTO.Email))
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

            _logger.LogInformation("Korisnik je uspesno prijavljen.");

            return Ok(user);
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

            return Ok(new { korisnickoIme = user.PersonName, email = user.Email });
        }

        _logger.LogInformation("Neuspesna prijava.");

        return Unauthorized();
    }

    [HttpPost("odjava")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return NoContent();
    }

    private async Task<bool> IsEmailAlreadyRegistered(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if(user is null)
        {
            return false;
        }

        return true;
    }
}
