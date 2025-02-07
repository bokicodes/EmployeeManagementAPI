using EmployeeManagement.Application.DTOs.Zaposleni;
using EmployeeManagement.Application.DTOs.Zadatak;

namespace EmployeeManagement.Application.DTOs.RadnoMesto;

public class RadnoMestoDetaljnoDTO
{
    public int RadnoMestoId { get; set; }
    public string NazivRM { get; set; }
    public string OpisRM { get; set; }
    public ICollection<ZadatakDTO> Zadaci { get; set; }
    public ICollection<ZaposleniDTO> Zaposleni { get; set; }
}
