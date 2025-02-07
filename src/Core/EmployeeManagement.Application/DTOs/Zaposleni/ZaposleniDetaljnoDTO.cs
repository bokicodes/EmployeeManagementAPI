namespace EmployeeManagement.Application.DTOs.Zaposleni;

public class ZaposleniDetaljnoDTO
{
    public int ZaposleniId { get; set; }
    public string Ime { get; set; } = null!;
    public string Prezime { get; set; } = null!;
    public DateTime DatumZaposlenja { get; set; }
    public string RadnoMesto { get; set; } = null!;
    public string OrganizacionaCelina { get; set; } = null!;
}
