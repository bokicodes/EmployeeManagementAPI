using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Business.DTOs.OrganizacionaCelina;

public class DodajOrgCelinuDTO
{
    [Required]
    public string NazivOC { get; set; }
    [Required]
    public string OpisOC { get; set; }
}
