using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Application.DTOs.OrganizacionaCelina;

public class DodajOrgCelinuDTO
{
    [Required]
    public string NazivOC { get; set; }
    [Required]
    public string OpisOC { get; set; }
}
