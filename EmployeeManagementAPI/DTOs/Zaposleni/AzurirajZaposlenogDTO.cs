using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeManagementAPI.DTOs.Zaposleni;

public class AzurirajZaposlenogDTO
{
    [JsonIgnore]
    public int ZaposleniId { get; set; }
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
