using AutoMapper;
using EmployeeManagementAPI.Data.Interfaces;
using EmployeeManagementAPI.DTOs.Zaposleni;
using EmployeeManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers;

[Route("api/zaposleni")]
[ApiController]
public class ZaposleniController : ControllerBase
{
    private readonly IZaposleniRepository _zaposleniRepo;
    private readonly ILogger<ZaposleniController> _logger;
    private readonly IMapper _mapper;

    public ZaposleniController(IZaposleniRepository zaposleniRepo, ILogger<ZaposleniController> logger, IMapper mapper)
    {
        _zaposleniRepo = zaposleniRepo;
        _logger = logger;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<ZaposleniDTO>> GetAll()
    {
        _logger.LogInformation("Poziva se metoda za vracanje svih zaposlenih...");

        var listaZaposlenih = await _zaposleniRepo.GetAllAsync();
        var listaZaposlenihDto = _mapper.Map<IEnumerable<ZaposleniDTO>>(listaZaposlenih);

        return Ok(listaZaposlenihDto);
    }
}
