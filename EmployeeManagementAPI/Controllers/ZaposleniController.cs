using EmployeeManagementAPI.CustomExceptions;
using EmployeeManagementAPI.DTOs.Zaposleni;
using EmployeeManagementAPI.Services.Interfaces;
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
    public async Task<ActionResult<IEnumerable<ZaposleniDTO>>> GetAllZaposleni()
    {
        _logger.LogInformation("Poziva se metoda za vracanje svih zaposlenih...");

        var listaZaposlenihDto = await _zaposleniService.GetAllZaposleniAsync();

        return Ok(listaZaposlenihDto);
    }

    [HttpGet("{id}", Name = "GetZaposleniById")]
    public async Task<ActionResult<ZaposleniDTO>> GetZaposleniById([FromRoute] int id)
    {
        _logger.LogInformation("Poziva se metoda za vracanje zaposlenog...");

        var zaposleniDto = await _zaposleniService.GetZaposleniByIdAsync(id);

        if(zaposleniDto is null)
        {
            return NotFound();
        }

        return Ok(zaposleniDto);
    }

    [HttpGet("{id}/with-details")]
    public async Task<ActionResult<ZaposleniMoreInfoDTO>> GetZaposleniWithInfoById([FromRoute] int id)
    {
        _logger.LogInformation("Poziva se metoda za vracanje zaposlenog sa dodatnim informacijama...");

        var zaposleniDto = await _zaposleniService.GetZaposleniWithAdditionalInfo(id);

        if (zaposleniDto is null)
        {
            return NotFound();
        }

        return Ok(zaposleniDto);
    }

    [HttpPost]
    public async Task<IActionResult> AddZaposleni([FromBody] AddZaposleniDTO addZaposleniDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var zaposleniDto = await _zaposleniService.AddZaposleniAsync(addZaposleniDto);

            return CreatedAtRoute("GetZaposleniById", new { id = zaposleniDto.ZaposleniId }, zaposleniDto);
        }
        catch (DbUpdateException)
        {
            return NotFound(new { errorMsg = "Ne postojece radno mesto ili organizaciona celina" });
        } 
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateZaposleni(int id, [FromBody] UpdateZaposleniDTO updateZaposleniDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            updateZaposleniDto.ZaposleniId = id;
            await _zaposleniService.UpdateZaposleniAsync(updateZaposleniDto);

            return NoContent();
        }
        catch(EntityNotFoundException ex)
        {
            return NotFound(new { errorMsg = ex.Message });
        }
        catch (DbUpdateException)
        {
            return NotFound(new { errorMsg = "Ne postojece radno mesto ili organizaciona celina" });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteZaposleni(int id)
    {
        try
        {
            await _zaposleniService.DeleteZaposleniAsync(id);

            return NoContent();
        }
        catch (EntityNotFoundException ex)
        {
            return NotFound(new { errorMsg = ex.Message });
        }
    }
}
