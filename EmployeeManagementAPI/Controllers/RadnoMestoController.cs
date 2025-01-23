using EmployeeManagementAPI.CustomExceptions;
using EmployeeManagementAPI.DTOs.RadnoMesto;
using EmployeeManagementAPI.DTOs.Zaposleni;
using EmployeeManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Controller;

[Route("api/radnamesta")]
[ApiController]
public class RadnoMestoController : ControllerBase
{
    private readonly IRadnoMestoService _radnoMestoService;
    private readonly ILogger<RadnoMestoController> _logger;

    public RadnoMestoController(IRadnoMestoService radnoMestoService, ILogger<RadnoMestoController> logger)
    {
        _radnoMestoService = radnoMestoService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RadnoMestoDTO>>> GetAllRadnoMesto()
    {
        _logger.LogInformation("Poziva se metoda za vracanje svih radnih mesta...");

        var listaRadnihMestaDTO = await _radnoMestoService.GetAllRadnoMestoAsync();

        return Ok(listaRadnihMestaDTO);
    }

    [HttpGet("{id}", Name = "GetRadnoMestoById")]
    public async Task<ActionResult<RadnoMestoMoreInfoDTO>> GetRadnoMestoById([FromRoute] int id)
    {
        _logger.LogInformation("Poziva se metoda za vracanje radnog mesta sa dodatnim informacijama...");

        var radnoMestoDto = await _radnoMestoService.GetRadnoMestoByIdAsync(id);

        if (radnoMestoDto is null)
        {
            _logger.LogInformation("Radno mesto nije pronadjeno.");
            return NotFound();
        }

        _logger.LogInformation("Radno mesto je pronadjeno.");
        return Ok(radnoMestoDto);
    }

    [HttpPost]
    public async Task<IActionResult> AddRadnoMesto([FromBody] AddRadnoMestoDTO addRadnoMestoDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var radnoMestoDto = await _radnoMestoService.AddRadnoMestoAsync(addRadnoMestoDto);

            _logger.LogInformation("Radno mesto je dodato.");

            return CreatedAtRoute("GetRadnoMestoById", new { id = radnoMestoDto.RadnoMestoId }, radnoMestoDto);
        }
        catch (DbUpdateException)
        {
            _logger.LogInformation("Doslo je do greske");
            return StatusCode(500, "Doslo je do greske prilikom dodavanja radnog mesta");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRadnoMesto(int id, [FromBody] UpdateRadnoMestoDTO updateRadnoMestoDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            updateRadnoMestoDto.RadnoMestoId = id;
            await _radnoMestoService.UpdateRadnoMestoAsync(updateRadnoMestoDto);

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
            return StatusCode(500, "Doslo je do greske prilikom dodavanja radnog mesta");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRadnoMesto(int id)
    {
        try
        {
            await _radnoMestoService.DeleteRadnoMestoAsync(id);

            _logger.LogInformation("Radno mesto je obrisano.");

            return NoContent();
        }
        catch (EntityNotFoundException ex)
        {
            _logger.LogInformation("Radno mesto nije pronadjeno.");
            return NotFound(new { errorMsg = ex.Message });
        }
    }
}
