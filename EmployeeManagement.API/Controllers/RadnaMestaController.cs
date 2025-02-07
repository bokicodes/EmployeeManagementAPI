using EmployeeManagement.Business.CustomExceptions;
using EmployeeManagement.Business.DTOs.RadnoMesto;
using EmployeeManagement.Business.DTOs.Zadatak;
using EmployeeManagement.Business.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Controller;

[Route("api/radna-mesta")]
[ApiController]
public class RadnaMestaController : ControllerBase
{
    private readonly IRadnoMestoService _radnoMestoService;
    private readonly ILogger<RadnaMestaController> _logger;

    public RadnaMestaController(IRadnoMestoService radnoMestoService, ILogger<RadnaMestaController> logger)
    {
        _radnoMestoService = radnoMestoService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> VratiSvaRadnaMesta()
    {
        _logger.LogInformation("Poziva se metoda za vracanje svih radnih mesta...");

        var listaRadnihMestaDTO = await _radnoMestoService.VratiSvaRadnaMestaAsync();

        return Ok(listaRadnihMestaDTO);
    }

    [HttpGet("{id}", Name = "VratiRadnoMestoPoId")]
    public async Task<IActionResult> VratiRadnoMestoPoId([FromRoute] int id)
    {
        _logger.LogInformation("Poziva se metoda za vracanje radnog mesta sa dodatnim informacijama...");

        var radnoMestoDto = await _radnoMestoService.VratiRadnoMestoPoIdAsync(id);

        if (radnoMestoDto is null)
        {
            _logger.LogInformation("Radno mesto nije pronadjeno.");
            return NotFound();
        }

        _logger.LogInformation("Radno mesto je pronadjeno.");
        return Ok(radnoMestoDto);
    }

    [HttpPost]
    public async Task<IActionResult> DodajRadnoMesto([FromBody] DodajRadnoMestoDTO dodajRadnoMestoDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var radnoMestoDto = await _radnoMestoService.DodajRadnoMestoAsync(dodajRadnoMestoDto);

            _logger.LogInformation("Radno mesto je dodato.");

            return CreatedAtRoute("VratiRadnoMestoPoId", new { id = radnoMestoDto.RadnoMestoId }, radnoMestoDto);
        }
        catch (DbUpdateException)
        {
            _logger.LogInformation("Doslo je do greske");
            return StatusCode(500, "Doslo je do greske prilikom dodavanja radnog mesta");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AzurirajRadnoMesto(int id, [FromBody] AzurirajRadnoMestoDTO azurirajRadnoMestoDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            azurirajRadnoMestoDto.RadnoMestoId = id;
            await _radnoMestoService.AzurirajRadnoMestoAsync(azurirajRadnoMestoDto);

            _logger.LogInformation("Radno mesto je azurirano.");

            return NoContent();
        }
        catch (EntityNotFoundException ex)
        {
            _logger.LogInformation("Radno mesto nije pronadjeno.");
            return NotFound(new { errorMsg = ex.Message });
        }
        catch (DbUpdateException)
        {
            _logger.LogInformation("Doslo je do greske.");
            return StatusCode(500, "Doslo je do greske prilikom dodavanja radnog mesta.");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> ObrisiRadnoMesto(int id)
    {
        try
        {
            await _radnoMestoService.ObrisiRadnoMestoAsync(id);

            _logger.LogInformation("Radno mesto je obrisano.");

            return NoContent();
        }
        catch (EntityNotFoundException ex)
        {
            _logger.LogInformation("Radno mesto nije pronadjeno.");
            return NotFound(new { errorMsg = ex.Message });
        }
    }


    // Zadaci

    [HttpPost("{id}/zadaci")]
    public async Task<IActionResult> DodajZadatak(int id, [FromBody] DodajZadatakDTO dodajZadatakDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _radnoMestoService.DodajZadatakZaRadnoMestoAsync(id, dodajZadatakDto);

            return Ok(new { message = "Zadatak je uspesno dodat" });
        }
        catch(EntityNotFoundException ex)
        {
            _logger.LogInformation("Radno mesto nije pronadjeno.");
            return NotFound(new { errorMsg = ex.Message });
        }
        catch (DbUpdateException)
        {
            _logger.LogInformation("Doslo je do greske.");
            return StatusCode(500, "Doslo je do greske prilikom dodavanja zadatka.");
        }
    }

    [HttpPut("{radnoMestoId}/zadaci/{zadatakId}")]
    public async Task<IActionResult> AzurirajTipZadatka(int radnoMestoId, int zadatakId, [FromBody] AzurirajZadatakDTO azurirajZadatakDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _radnoMestoService.AzurirajZadatakZaRadnoMestoAsync(radnoMestoId, zadatakId, azurirajZadatakDto);

            return NoContent();
        }
        catch (EntityNotFoundException ex)
        {
            _logger.LogInformation("Entitet nije pronadjen.");
            return NotFound(new { errorMsg = ex.Message });
        }
        catch (DbUpdateException)
        {
            _logger.LogInformation("Doslo je do greske.");
            return StatusCode(500, "Doslo je do greske prilikom azuriranja zadatka.");
        }
    }

    [HttpDelete("{radnoMestoId}/zadaci/{zadatakId}")]
    public async Task<IActionResult> ObrisiTipZadatka(int radnoMestoId, int zadatakId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _radnoMestoService.ObrisiZadatakZaRadnoMestoAsync(radnoMestoId, zadatakId);

            return NoContent();
        }
        catch (EntityNotFoundException ex)
        {
            _logger.LogInformation("Entitet nije pronadjen.");
            return NotFound(new { errorMsg = ex.Message });
        }
        catch (DbUpdateException)
        {
            _logger.LogInformation("Doslo je do greske.");
            return StatusCode(500, "Doslo je do greske prilikom brisanja zadatka.");
        }
    }
}
