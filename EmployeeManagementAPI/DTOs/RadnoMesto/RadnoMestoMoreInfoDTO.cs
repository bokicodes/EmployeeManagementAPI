using EmployeeManagementAPI.DTOs.TipZadatka;
using EmployeeManagementAPI.DTOs.Zaposleni;

namespace EmployeeManagementAPI.DTOs.RadnoMesto;

public class RadnoMestoMoreInfoDTO
{
    public int RadnoMestoId { get; set; }
    public string NazivRM { get; set; }
    public string OpisRM { get; set; }
    public ICollection<TipZadatkaDTO> TipoviZadataka { get; set; }
    public ICollection<ZaposleniDTO> Zaposleni { get; set; }
}
