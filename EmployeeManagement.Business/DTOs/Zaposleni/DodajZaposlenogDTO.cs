using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Business.DTOs.Zaposleni;

public class DodajZaposlenogDTO
{
    [Required]
    public string Ime { get; set; } = null!;
    [Required]
    public string Prezime { get; set; } = null!;
    [Required]
    public DateTime DatumZaposlenja { get; set; }
    [Required]
    public int RadnoMestoId { get; set; }
    public int? OrgCelinaId { get; set; }
}
