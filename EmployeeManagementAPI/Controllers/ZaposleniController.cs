using AutoMapper;
using EmployeeManagementAPI.Data.Interfaces;
using EmployeeManagementAPI.DTOs.Zaposleni;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

    [HttpGet("{id}")]
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
}
