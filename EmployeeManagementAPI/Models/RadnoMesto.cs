using System;
using System.Collections.Generic;

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


        public void AddTipZadatka(TipZadatka tipZadatka)
        {
            TipoviZadataka.Add(tipZadatka);
        }

        public void UpdateTipZadatka(TipZadatka stariZadatak, TipZadatka noviZadatak)
        {
            stariZadatak.Update(noviZadatak);
        }

        public void DeleteTipZadatka(TipZadatka tipZadatka)
        {
            TipoviZadataka.Remove(tipZadatka);
        }
    }
}
