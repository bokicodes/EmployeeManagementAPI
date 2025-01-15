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

    public ZaposleniController(IZaposleniRepository zaposleniRepo)
    {
        _zaposleniRepo = zaposleniRepo;
    }

    [HttpGet]
    public async Task<IEnumerable<Zaposleni>> GetAll()
    {
        return await _zaposleniRepo.GetAllAsync();
    }
}
