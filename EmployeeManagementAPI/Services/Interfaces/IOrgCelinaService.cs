using EmployeeManagementAPI.DTOs.OrganizacionaCelina;

namespace EmployeeManagementAPI.Services.Interfaces;

public interface IOrgCelinaService
{
    Task<IEnumerable<OrgCelinaDTO>> GetAllOrgCelinaAsync();
    Task<OrgCelinaMoreInfoDTO?> GetOrgCelinaByIdAsync(int id);
    Task<OrgCelinaDTO> AddOrgCelinaAsync(AddOrgCelinaDTO addOrgCelinaDto);
    Task UpdateOrgCelinaAsync(UpdateOrgCelinaDTO updateOrgCelinaDto);
    Task DeleteOrgCelinaAsync(int id);
}
