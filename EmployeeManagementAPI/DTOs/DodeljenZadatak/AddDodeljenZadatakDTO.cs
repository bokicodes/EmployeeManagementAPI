using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementAPI.DTOs.DodeljenZadatak;

public class AddDodeljenZadatakDTO
{
    [Required]
    public DateTime DatumZadavanja { get; set; }
    public DateTime? DatumZavrsetka { get; set; }
}
