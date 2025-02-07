namespace EmployeeManagement.Domain.Models
{
    public partial class DodeljenZadatak
    {
        public int RadnoMestoId { get; set; }
        public int ZadatakId { get; set; }
        public int ZaposleniId { get; set; }
        public DateTime DatumZadavanja { get; set; }
        public DateTime RokIzvrsenja { get; set; }
        public DateTime? DatumZavrsetka { get; set; }

        public virtual Zadatak Zadatak { get; set; } = null!;
        public virtual Zaposleni Zaposleni { get; set; } = null!;
    }
}
