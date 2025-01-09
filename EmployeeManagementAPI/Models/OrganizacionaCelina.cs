using System;
using System.Collections.Generic;

namespace EmployeeManagementAPI.Models
{
    public partial class OrganizacionaCelina
    {
        public OrganizacionaCelina()
        {
            Zaposleni = new List<Zaposleni>();
        }

        public int OrgCelinaId { get; set; }
        public string NazivOC { get; set; } = null!;
        public string OpisOC { get; set; } = null!;

        public virtual ICollection<Zaposleni> Zaposleni { get; set; }
    }
}
