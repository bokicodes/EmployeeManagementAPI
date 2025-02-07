using EmployeeManagement.Application.DTOs.OrganizacionaCelina;
using EmployeeManagement.Application.ServiceInterfaces;
using EmployeeManagement.Domain.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Controllers;

[ApiController]
[Route("/api/organizacione-celine")]
public class OrgCelineController : ControllerBase
{
    private readonly IOrgCelinaService _orgCelinaService;
    private readonly ILogger<OrgCelineController> _logger;

    public OrgCelineController(IOrgCelinaService orgCelinaService, ILogger<OrgCelineController> logger)
    {
        _orgCelinaService = orgCelinaService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> VratiSveOrgCeline()
    {
        _logger.LogInformation("Poziva se metoda za vracanje svih organizacionih celina...");

        var listaOrgCelinaDTO = await _orgCelinaService.VratiSveOrgCelineAsync();

        return Ok(listaOrgCelinaDTO);
    }

    [HttpGet("{id}", Name = "VratiOrgCelinuPoId")]
    public async Task<IActionResult> VratiOrgCelinuPoId([FromRoute] int id)
    {
        _logger.LogInformation("Poziva se metoda za vracanje organizacione celine sa dodatnim informacijama...");

        var orgCelinaDto = await _orgCelinaService.VratiOrgCelinuPoIdAsync(id);

        if (orgCelinaDto is null)
        {
            _logger.LogInformation("Organizaciona celina nije pronadjena.");
            return NotFound();
        }

        _logger.LogInformation("Organizaciona celina je pronadjena.");
        return Ok(orgCelinaDto);
    }

    [HttpPost]
    public async Task<IActionResult> DodajOrgCelinu([FromBody] DodajOrgCelinuDTO dodajOrgCelinuDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var orgCelinaDto = await _orgCelinaService.DodajOrgCelinuAsync(dodajOrgCelinuDto);

            _logger.LogInformation("Organizaciona celina je dodata.");

            return CreatedAtRoute("VratiOrgCelinuPoId", new { id = orgCelinaDto.OrgCelinaId }, orgCelinaDto);
        }
        catch (DbUpdateException)
        {
            _logger.LogInformation("Doslo je do greske");
            return StatusCode(500, "Doslo je do greske prilikom dodavanje organizacione celine");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AzurirajOrgCelinu(int id, [FromBody] AzurirajOrgCelinuDTO azurirajOrgCelinuDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            azurirajOrgCelinuDto.OrgCelinaId = id;
            await _orgCelinaService.AzurirajOrgCelinuAsync(azurirajOrgCelinuDto);

            _logger.LogInformation("Organizaciona celina je azurirana.");

            return NoContent();
        }
        catch (EntityNotFoundException ex)
        {
            _logger.LogInformation("Organizaciona celina nije pronadjena.");
            return NotFound(new { errorMsg = ex.Message });
        }
        catch (DbUpdateException)
        {
            _logger.LogInformation("Doslo je do greske.");
            return StatusCode(500, "Doslo je do greske prilikom dodavanje organizacione celine");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> ObrisiOrgCelinu(int id)
    {
        try
        {
            await _orgCelinaService.ObrisiOrgCelinuAsync(id);

            _logger.LogInformation("Organizaciona celina je obrisana.");

            return NoContent();
        }
        catch (EntityNotFoundException ex)
        {
            _logger.LogInformation("Organizaciona celina nije pronadjena.");
            return NotFound(new { errorMsg = ex.Message });
        }
    }
}
