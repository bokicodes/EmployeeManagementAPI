using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Business.DTOs.DodeljenZadatak;

public class AzurirajDodeljenZadatakDTO
{
    [Required]
    public DateTime DatumZadavanja { get; set; }
    public DateTime? DatumZavrsetka { get; set; }
}
