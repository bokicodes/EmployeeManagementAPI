using EmployeeManagement.Application.DTOs.Zaposleni;

namespace EmployeeManagement.Application.DTOs.OrganizacionaCelina;

public class OrgCelinaDetaljnoDTO
{
    public int OrgCelinaId { get; set; }
    public string NazivOC { get; set; }
    public string OpisOC { get; set; }
    public virtual ICollection<ZaposleniDTO> Zaposleni { get; set; }
}
