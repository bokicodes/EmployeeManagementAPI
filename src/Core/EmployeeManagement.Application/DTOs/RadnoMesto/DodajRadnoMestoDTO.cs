using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Application.DTOs.RadnoMesto;

public class DodajRadnoMestoDTO
{
    [Required]
    public string NazivRM { get; set; }
    [Required]
    public string OpisRM { get; set; }
}
