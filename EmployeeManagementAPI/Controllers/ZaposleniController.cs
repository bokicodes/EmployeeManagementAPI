using EmployeeManagementAPI.Data.Interfaces;
using EmployeeManagementAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementAPI.Controllers;

[Route("api/zaposleni")]
[ApiController]
public class ZaposleniController : ControllerBase
{
    private readonly IZaposleniRepository _zaposleniRepo;
    private readonly ILogger<ZaposleniController> _logger;

    public ZaposleniController(IZaposleniRepository zaposleniRepo, ILogger<ZaposleniController> logger)
    {
        _zaposleniRepo = zaposleniRepo;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<Zaposleni>> GetAll()
    {
        _logger.LogInformation("Poziva se metoda za vracanje svih zaposlenih...");

        return await _zaposleniRepo.GetAllAsync();
    }
}
