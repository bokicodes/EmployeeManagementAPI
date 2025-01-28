using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementAPI.DTOs.DodeljenZadatak;

public class UpdateDodeljenZadatakDTO
{
    [Required]
    public DateTime DatumZadavanja { get; set; }
    public DateTime? DatumZavrsetka { get; set; }
}
