namespace EmployeeManagement.Infrastructure.Models
{
    public partial class RadnoMesto
    {
        public RadnoMesto()
        {
            Zadaci = new List<Zadatak>();
            Zaposleni = new List<Zaposleni>();
        }

        public int RadnoMestoId { get; set; }
        public string NazivRM { get; set; } = null!;
        public string OpisRM { get; set; } = null!;

        public virtual ICollection<Zadatak> Zadaci { get; set; }
        public virtual ICollection<Zaposleni> Zaposleni { get; set; }


        public void DodajZadatak(Zadatak zadatak)
        {
            Zadaci.Add(zadatak);
        }

        public void AzurirajZadatak(Zadatak stariZadatak, Zadatak noviZadatak)
        {
            stariZadatak.Azuriraj(noviZadatak);
        }

        public void ObrisiZadatak(Zadatak zadatak)
        {
            Zadaci.Remove(zadatak);
        }
    }
}
