using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeManagement.Application.DTOs.RadnoMesto;

public class AzurirajRadnoMestoDTO
{
    [JsonIgnore]
    public int RadnoMestoId { get; set; }
    [Required]
    public string NazivRM { get; set; }
    [Required]
    public string OpisRM { get; set; }
}
