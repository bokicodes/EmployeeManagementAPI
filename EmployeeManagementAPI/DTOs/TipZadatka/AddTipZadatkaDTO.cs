using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementAPI.DTOs.TipZadatka;

public class AddTipZadatkaDTO
{
    [Required]
    public string NazivZad { get; set; }
    [Required]
    public string OpisZad { get; set; }
}
