using EmployeeManagement.Business.DTOs.Zaposleni;
using EmployeeManagement.Business.DTOs.Zadatak;

namespace EmployeeManagement.Business.DTOs.RadnoMesto;

public class RadnoMestoDetaljnoDTO
{
    public int RadnoMestoId { get; set; }
    public string NazivRM { get; set; }
    public string OpisRM { get; set; }
    public ICollection<ZadatakDTO> Zadaci { get; set; }
    public ICollection<ZaposleniDTO> Zaposleni { get; set; }
}
