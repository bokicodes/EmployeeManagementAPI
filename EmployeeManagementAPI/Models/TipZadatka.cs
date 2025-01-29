namespace EmployeeManagementAPI.Models
{
    public partial class TipZadatka
    {
        public TipZadatka()
        {
            DodeljeniZadaci = new List<DodeljenZadatak>();
        }

        public int ZadatakId { get; set; }
        public int RadnoMestoId { get; set; }
        public string NazivZad { get; set; } = null!;
        public string OpisZad { get; set; } = null!;

        public virtual RadnoMesto RadnoMesto { get; set; } = null!;
        public virtual ICollection<DodeljenZadatak> DodeljeniZadaci { get; set; }

        public void Azuriraj(TipZadatka tipZadatka)
        {
            NazivZad = tipZadatka.NazivZad;
            OpisZad = tipZadatka.OpisZad;
        }
    }
}
