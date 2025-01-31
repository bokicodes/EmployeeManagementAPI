using EmployeeManagement.Business.DTOs.TipZadatka;
using EmployeeManagement.Business.DTOs.Zaposleni;

namespace EmployeeManagement.Business.DTOs.RadnoMesto;

public class RadnoMestoDetaljnoDTO
{
    public int RadnoMestoId { get; set; }
    public string NazivRM { get; set; }
    public string OpisRM { get; set; }
    public ICollection<TipZadatkaDTO> TipoviZadataka { get; set; }
    public ICollection<ZaposleniDTO> Zaposleni { get; set; }
}
