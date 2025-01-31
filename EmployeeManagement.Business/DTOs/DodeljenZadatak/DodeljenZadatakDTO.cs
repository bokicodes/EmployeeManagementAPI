namespace EmployeeManagement.Business.DTOs.DodeljenZadatak;

public class DodeljenZadatakDTO
{
    public int ZaposleniId { get; set; }
    public string NazivZaposlenog { get; set; }
    public int ZadatakId { get; set; }
    public string NazivZadatka { get; set; }
    public DateTime DatumZadavanja { get; set; }
    public DateTime? DatumZavrsetka { get; set; }
}
