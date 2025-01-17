namespace EmployeeManagementAPI.DTOs.Zaposleni;

public class AddZaposleniDTO
{
    public string Ime { get; set; } = null!;
    public string Prezime { get; set; } = null!;
    public DateTime DatumZaposlenja { get; set; }
    public int RadnoMestoId { get; set; }
    public int? OrganizacionaCelinaId { get; set; }
}
