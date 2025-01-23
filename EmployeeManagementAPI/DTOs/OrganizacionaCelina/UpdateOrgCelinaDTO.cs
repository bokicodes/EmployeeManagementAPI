using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeManagementAPI.DTOs.OrganizacionaCelina;

public class UpdateOrgCelinaDTO
{
    [JsonIgnore]
    public int OrgCelinaId { get; set; }
    [Required]
    public string NazivOC { get; set; }
    [Required]
    public string OpisOC { get; set; }
}
