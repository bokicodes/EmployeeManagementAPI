using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementAPI.DTOs.OrganizacionaCelina;

public class AddOrgCelinaDTO
{
    [Required]
    public string NazivOC { get; set; }
    [Required]
    public string OpisOC { get; set; }
}
