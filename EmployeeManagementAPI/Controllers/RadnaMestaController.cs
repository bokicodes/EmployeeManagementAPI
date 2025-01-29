using EmployeeManagementAPI.CustomExceptions;
using EmployeeManagementAPI.DTOs.RadnoMesto;
using EmployeeManagementAPI.DTOs.TipZadatka;
using EmployeeManagementAPI.DTOs.Zaposleni;
using EmployeeManagementAPI.Services.Interfaces;
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


    // Tipovi zadataka

    [HttpPost("{id}/tipovi-zadataka")]
    public async Task<IActionResult> DodajTipZadatka(int id, [FromBody] DodajTipZadatkaDTO dodajTipZadatkaDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _radnoMestoService.DodajTipZadatkaZaRadnoMestoAsync(id, dodajTipZadatkaDto);

            return Ok(new { message = "Tip zadatka je uspesno dodat" });
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

    [HttpPut("{id}/tipovi-zadataka/{zadatakId}")]
    public async Task<IActionResult> AzurirajTipZadatka(int id, int zadatakId, [FromBody] AzurirajTipZadatkaDTO azurirajTipZadatkaDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _radnoMestoService.AzurirajTipZadatkaZaRadnoMestoAsync(id, zadatakId, azurirajTipZadatkaDto);

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

    [HttpDelete("{id}/tipovi-zadataka/{zadatakId}")]
    public async Task<IActionResult> ObrisiTipZadatka(int id, int zadatakId)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _radnoMestoService.ObrisiTipZadatkaZaRadnoMestoAsync(id, zadatakId);

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
