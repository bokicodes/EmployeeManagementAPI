using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Application.DTOs.Zadatak;

public class DodajZadatakDTO
{
    [Required]
    public string NazivZad { get; set; }
    [Required]
    public string OpisZad { get; set; }
    public string TipZadatka { get; set; }
}
