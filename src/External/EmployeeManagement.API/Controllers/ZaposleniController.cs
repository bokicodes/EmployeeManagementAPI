using EmployeeManagement.Application.DTOs.Zaposleni;
using EmployeeManagement.Application.ServiceInterfaces;
using EmployeeManagement.Domain.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Controllers;

[Route("api/zaposleni")]
[ApiController]
public class ZaposleniController : ControllerBase
{
    private readonly IZaposleniService _zaposleniService;
    private readonly ILogger<ZaposleniController> _logger;

    public ZaposleniController(IZaposleniService zaposleniService, ILogger<ZaposleniController> logger)
    {
        _zaposleniService = zaposleniService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> VratiSveZaposlene()
    {
        _logger.LogInformation("Poziva se metoda za vracanje svih zaposlenih...");

        var listaZaposlenihDto = await _zaposleniService.VratiSveZaposleneAsync();

        return Ok(listaZaposlenihDto);
    }

    [HttpGet("{id}", Name = "VratiZaposlenogPoId")]
    public async Task<IActionResult> VratiZaposlenogPoId([FromRoute] int id)
    {
        _logger.LogInformation("Poziva se metoda za vracanje zaposlenog...");

        var zaposleniDto = await _zaposleniService.VratiZaposlenogPoIdAsync(id);

        if(zaposleniDto is null)
        {
            _logger.LogInformation("Zaposleni nije pronadjen.");
            return NotFound();
        }

        _logger.LogInformation("Zaposleni je pronadjen.");
        return Ok(zaposleniDto);
    }

    [HttpGet("{id}/detaljno")]
    public async Task<IActionResult> VratiZaposlenogSaDetaljima([FromRoute] int id)
    {
        _logger.LogInformation("Poziva se metoda za vracanje zaposlenog sa dodatnim informacijama...");

        var zaposleniDto = await _zaposleniService.VratiZaposlenogSaDetaljimaAsync(id);

        if (zaposleniDto is null)
        {
            _logger.LogInformation("Zaposleni nije pronadjen.");
            return NotFound();
        }

        _logger.LogInformation("Zaposleni je pronadjen.");
        return Ok(zaposleniDto);
    }

    [HttpPost]
    public async Task<IActionResult> DodajZaposlenog([FromBody] DodajZaposlenogDTO dodajZaposlenogDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var zaposleniDto = await _zaposleniService.DodajZaposlenogAsync(dodajZaposlenogDto);

            _logger.LogInformation("Zaposleni je dodat.");

            return CreatedAtRoute("VratiZaposlenogPoId", new { id = zaposleniDto.ZaposleniId }, zaposleniDto);
        }
        catch (DbUpdateException)
        {
            _logger.LogInformation("Doslo je do greske");
            return NotFound(new { errorMsg = "Ne postojece radno mesto ili organizaciona celina" });
        } 
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AzurirajZaposlenog(int id, [FromBody] AzurirajZaposlenogDTO azurirajZaposlenogDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            azurirajZaposlenogDto.ZaposleniId = id;
            await _zaposleniService.AzurirajZaposlenogAsync(azurirajZaposlenogDto);

            _logger.LogInformation("Zaposleni je azuriran.");

            return NoContent();
        }
        catch(EntityNotFoundException ex)
        {
            _logger.LogInformation("Zaposleni nije pronadjen.");
            return NotFound(new { errorMsg = ex.Message });
        }
        catch (DbUpdateException)
        {
            _logger.LogInformation("Doslo je do greske.");
            return NotFound(new { errorMsg = "Ne postojece radno mesto ili organizaciona celina" });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> ObrisiZaposlenog(int id)
    {
        try
        {
            await _zaposleniService.ObrisiZaposlenogAsync(id);

            _logger.LogInformation("Zaposleni je obrisan.");

            return NoContent();
        }
        catch (EntityNotFoundException ex)
        {
            _logger.LogInformation("Zaposleni nije pronadjen.");
            return NotFound(new { errorMsg = ex.Message });
        }
    }
}
