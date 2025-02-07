using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeManagement.Application.DTOs.OrganizacionaCelina;

public class AzurirajOrgCelinuDTO
{
    [JsonIgnore]
    public int OrgCelinaId { get; set; }
    [Required]
    public string NazivOC { get; set; }
    [Required]
    public string OpisOC { get; set; }
}
