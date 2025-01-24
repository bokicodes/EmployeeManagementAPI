using EmployeeManagementAPI.CustomExceptions;
using EmployeeManagementAPI.DTOs.OrganizacionaCelina;
using EmployeeManagementAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Controllers;

[ApiController]
[Route("/api/organizacione-celine")]
public class OrgCelinaController : ControllerBase
{
    private readonly IOrgCelinaService _orgCelinaService;
    private readonly ILogger<OrgCelinaController> _logger;

    public OrgCelinaController(IOrgCelinaService orgCelinaService, ILogger<OrgCelinaController> logger)
    {
        _orgCelinaService = orgCelinaService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrgCelina()
    {
        _logger.LogInformation("Poziva se metoda za vracanje svih organizacionih celina...");

        var listaOrgCelinaDTO = await _orgCelinaService.GetAllOrgCelinaAsync();

        return Ok(listaOrgCelinaDTO);
    }

    [HttpGet("{id}", Name = "GetOrgCelinaById")]
    public async Task<IActionResult> GetOrgCelinaById([FromRoute] int id)
    {
        _logger.LogInformation("Poziva se metoda za vracanje organizacione celine sa dodatnim informacijama...");

        var orgCelinaDto = await _orgCelinaService.GetOrgCelinaByIdAsync(id);

        if (orgCelinaDto is null)
        {
            _logger.LogInformation("Organizaciona celina nije pronadjena.");
            return NotFound();
        }

        _logger.LogInformation("Organizaciona celina je pronadjena.");
        return Ok(orgCelinaDto);
    }

    [HttpPost]
    public async Task<IActionResult> AddOrgCelina([FromBody] AddOrgCelinaDTO addOrgCelinaDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var orgCelinaDto = await _orgCelinaService.AddOrgCelinaAsync(addOrgCelinaDto);

            _logger.LogInformation("Organizaciona celina je dodato.");

            return CreatedAtRoute("GetOrgCelinaById", new { id = orgCelinaDto.OrgCelinaId }, orgCelinaDto);
        }
        catch (DbUpdateException)
        {
            _logger.LogInformation("Doslo je do greske");
            return StatusCode(500, "Doslo je do greske prilikom dodavanje organizacione celine");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrgCelina(int id, [FromBody] UpdateOrgCelinaDTO updateOrgCelinaDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            updateOrgCelinaDto.OrgCelinaId = id;
            await _orgCelinaService.UpdateOrgCelinaAsync(updateOrgCelinaDto);

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
    public async Task<IActionResult> DeleteOrgCelina(int id)
    {
        try
        {
            await _orgCelinaService.DeleteOrgCelinaAsync(id);

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
