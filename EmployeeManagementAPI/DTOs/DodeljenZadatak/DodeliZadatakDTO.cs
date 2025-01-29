using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementAPI.DTOs.DodeljenZadatak;

public class DodeliZadatakDTO
{
    [Required]
    public DateTime DatumZadavanja { get; set; }
    public DateTime? DatumZavrsetka { get; set; }
}
