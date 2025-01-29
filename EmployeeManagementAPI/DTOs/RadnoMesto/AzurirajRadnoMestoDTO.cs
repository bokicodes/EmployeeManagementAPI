using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeManagementAPI.DTOs.RadnoMesto;

public class AzurirajRadnoMestoDTO
{
    [JsonIgnore]
    public int RadnoMestoId { get; set; }
    [Required]
    public string NazivRM { get; set; }
    [Required]
    public string OpisRM { get; set; }
}
