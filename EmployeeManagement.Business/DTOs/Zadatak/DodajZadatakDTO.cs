using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Business.DTOs.Zadatak;

public class DodajZadatakDTO
{
    [Required]
    public string NazivZad { get; set; }
    [Required]
    public string OpisZad { get; set; }
}
