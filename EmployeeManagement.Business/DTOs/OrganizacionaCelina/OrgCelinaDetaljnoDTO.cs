using EmployeeManagement.Business.DTOs.Zaposleni;

namespace EmployeeManagement.Business.DTOs.OrganizacionaCelina;

public class OrgCelinaDetaljnoDTO
{
    public int OrgCelinaId { get; set; }
    public string NazivOC { get; set; }
    public string OpisOC { get; set; }
    public virtual ICollection<ZaposleniDTO> Zaposleni { get; set; }
}
