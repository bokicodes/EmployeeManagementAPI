namespace EmployeeManagement.Domain.Models
{
    public partial class Zadatak
    {
        public Zadatak()
        {
            DodeljeniZadaci = new List<DodeljenZadatak>();
        }

        public int ZadatakId { get; set; }
        public int RadnoMestoId { get; set; }
        public string NazivZad { get; set; } = null!;
        public string OpisZad { get; set; } = null!;
        public TipZadatka TipZadatka { get; set; }

        public virtual RadnoMesto RadnoMesto { get; set; } = null!;
        public virtual ICollection<DodeljenZadatak> DodeljeniZadaci { get; set; }

        public void Azuriraj(Zadatak zadatak)
        {
            NazivZad = zadatak.NazivZad;
            OpisZad = zadatak.OpisZad;
            TipZadatka = zadatak.TipZadatka;
        }
    }
}
