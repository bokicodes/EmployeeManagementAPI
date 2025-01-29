namespace EmployeeManagementAPI.Models
{
    public partial class RadnoMesto
    {
        public RadnoMesto()
        {
            TipoviZadataka = new List<TipZadatka>();
            Zaposleni = new List<Zaposleni>();
        }

        public int RadnoMestoId { get; set; }
        public string NazivRM { get; set; } = null!;
        public string OpisRM { get; set; } = null!;

        public virtual ICollection<TipZadatka> TipoviZadataka { get; set; }
        public virtual ICollection<Zaposleni> Zaposleni { get; set; }


        public void DodajTipZadatka(TipZadatka tipZadatka)
        {
            TipoviZadataka.Add(tipZadatka);
        }

        public void AzurirajTipZadatka(TipZadatka stariZadatak, TipZadatka noviZadatak)
        {
            stariZadatak.Azuriraj(noviZadatak);
        }

        public void ObrisiTipZadatka(TipZadatka tipZadatka)
        {
            TipoviZadataka.Remove(tipZadatka);
        }
    }
}
