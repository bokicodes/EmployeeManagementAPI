namespace EmployeeManagementAPI.DTOs.Zaposleni;

public class ZaposleniDTO
{
    public int ZaposleniId { get; set; }
    public string Ime { get; set; } = null!;
    public string Prezime { get; set; } = null!;
    public DateTime DatumZaposlenja { get; set; }
}
