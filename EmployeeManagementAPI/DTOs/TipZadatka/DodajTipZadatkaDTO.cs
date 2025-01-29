using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementAPI.DTOs.TipZadatka;

public class DodajTipZadatkaDTO
{
    [Required]
    public string NazivZad { get; set; }
    [Required]
    public string OpisZad { get; set; }
}
