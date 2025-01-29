using EmployeeManagementAPI.CustomExceptions;
using EmployeeManagementAPI.DTOs.DodeljenZadatak;
using EmployeeManagementAPI.DTOs.OrganizacionaCelina;
using EmployeeManagementAPI.DTOs.Zaposleni;
using EmployeeManagementAPI.Services;
using EmployeeManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Controllers;

[ApiController]
[Route("/api/zaposleni/{zaposleniId:int}/tipovi-zadataka")]
public class DodeljeniZadaciController : ControllerBase
{
    private readonly IDodeljenZadatakService _dodeljenZadatakService;
    private readonly ILogger<DodeljeniZadaciController> _logger;

    public DodeljeniZadaciController(IDodeljenZadatakService dodeljenZadatakService,
        ILogger<DodeljeniZadaciController> logger)
    {
        _dodeljenZadatakService = dodeljenZadatakService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> VratiSveDodeljeneZadatke(int zaposleniId)
    {
        _logger.LogInformation("Poziva se metoda za vracanje svih dodeljenih zadataka zaposlenom...");

        try
        {
            var listaDodeljenihZadataka = await _dodeljenZadatakService.VratiSveDodeljeneZadatkeAsync(zaposleniId);

            return Ok(listaDodeljenihZadataka);
        }
        catch(EntityNotFoundException ex)
        {
            _logger.LogInformation("Zaposleni nije pronadjen.");
            return NotFound(new { message = ex.Message });
        }

    }

    [HttpGet("{zadatakId:int}")]
    public async Task<IActionResult> VratiDodeljenZadatakPoId(int zaposleniId, int zadatakId)
    {
        _logger.LogInformation("Poziva se metoda za vracanje dodeljenog zadatka zaposlenom...");

        try
        {
            var dodeljenZadatakDto = await _dodeljenZadatakService
                .VratiDodeljenZadatakPoIdsAsync(zaposleniId, zadatakId);

            _logger.LogInformation("Dodeljen zadatak je pronadjen.");
            return Ok(dodeljenZadatakDto);

        }
        catch(EntityNotFoundException ex)
        {
            _logger.LogInformation("Entitet nije pronadjen.");
            return NotFound(new {message = ex.Message});
        } 
    }

    [HttpPost("{zadatakId:int}")]
    public async Task<IActionResult> DodeliZadatak(int zaposleniId, int zadatakId,
        [FromBody] DodeliZadatakDTO dodeliZadatakDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var dodeljenZadatakDto = await _dodeljenZadatakService
                .DodeliZadatakAsync(zaposleniId, zadatakId, dodeliZadatakDto);

            _logger.LogInformation("Zadatak je dodeljen zaposlenom.");

            return Ok();
        }
        catch (EntityNotFoundException ex)
        {
            _logger.LogInformation("Entitet nije pronadjen.");
            return NotFound(new { message = ex.Message });
        }
        catch(InvalidOperationException ex)
        {
            _logger.LogInformation("Zadatak je vec dodeljen.");
            return Conflict(new {message = ex.Message});
        }
        catch (DbUpdateException)
        {
            _logger.LogInformation("Doslo je do greske.");
            return StatusCode(500, "Doslo je do greske prilikom dodele zadatka zaposlenom");
        }
    }

    [HttpPut("{zadatakId:int}")]
    public async Task<IActionResult> AzurirajDodeljenZadatak(int zaposleniId, int zadatakId,
        [FromBody] AzurirajDodeljenZadatakDTO azurirajDodeljenZadatakDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _dodeljenZadatakService
                .AzurirajDodeljenZadatakAsync(zaposleniId, zadatakId, azurirajDodeljenZadatakDto);

            _logger.LogInformation("Dodeljen zadatak je azuriran.");

            return NoContent();
        }
        catch (EntityNotFoundException ex)
        {
            _logger.LogInformation("Entitet nije pronadjen.");
            return NotFound(new { message = ex.Message });
        }
        catch (DbUpdateException)
        {
            _logger.LogInformation("Doslo je do greske.");
            return StatusCode(500, "Doslo je do greske prilikom dodele zadatka zaposlenom");
        }
    }

    [HttpDelete("{zadatakId:int}")]
    public async Task<IActionResult> ObrisiDodeljenZadatak(int zaposleniId, int zadatakId)
    {
        try
        {
            await _dodeljenZadatakService.ObrisiDodeljenZadatakAsync(zaposleniId, zadatakId);

            _logger.LogInformation("Dodeljen zadatak je obrisan.");

            return NoContent();
        }
        catch (EntityNotFoundException ex)
        {
            _logger.LogInformation("Entitet nije pronadjen.");
            return NotFound(new { errorMsg = ex.Message });
        }
    }
}
