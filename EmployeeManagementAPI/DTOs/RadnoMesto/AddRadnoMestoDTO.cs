using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementAPI.DTOs.RadnoMesto;

public class AddRadnoMestoDTO
{
    [Required]
    public string NazivRM { get; set; }
    [Required]
    public string OpisRM { get; set; }
}
