using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Business.DTOs.RadnoMesto;

public class DodajRadnoMestoDTO
{
    [Required]
    public string NazivRM { get; set; }
    [Required]
    public string OpisRM { get; set; }
}
