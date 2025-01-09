using System;
using System.Collections.Generic;

namespace EmployeeManagementAPI.Models
{
    public partial class Zaposleni
    {
        public Zaposleni()
        {
            DodeljeniZadaci = new List<DodeljenZadatak>();
        }

        public int ZaposleniId { get; set; }
        public string Ime { get; set; } = null!;
        public string Prezime { get; set; } = null!;
        public DateTime DatumZaposlenja { get; set; }
        public int RadnoMestoId { get; set; }
        public int? OrgCelinaId { get; set; }

        public virtual OrganizacionaCelina? OrgCelina { get; set; }
        public virtual RadnoMesto RadnoMesto { get; set; } = null!;
        public virtual ICollection<DodeljenZadatak> DodeljeniZadaci { get; set; }
    }
}
