using AutoMapper;
using EmployeeManagementAPI.CustomExceptions;
using EmployeeManagementAPI.Data.Interfaces;
using EmployeeManagementAPI.DTOs.OrganizacionaCelina;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Services;

public class OrgCelinaService : IOrgCelinaService
{
    private readonly IOrgCelinaRepository _orgCelinaRepo;
    private readonly IMapper _mapper;

    public OrgCelinaService(IOrgCelinaRepository orgCelinaRepo, IMapper mapper)
    {
        _orgCelinaRepo = orgCelinaRepo;
        _mapper = mapper;
    }

    public async Task<OrgCelinaDTO> AddOrgCelinaAsync(AddOrgCelinaDTO addOrgCelinaDto)
    {
        var orgCelina = _mapper.Map<OrganizacionaCelina>(addOrgCelinaDto);

        var novaOrgCelina = await _orgCelinaRepo.AddAsync(orgCelina);

        try
        {
            await _orgCelinaRepo.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            throw;
        }

        return _mapper.Map<OrgCelinaDTO>(novaOrgCelina);
    }

    public async Task DeleteOrgCelinaAsync(int id)
    {
        var orgCelina = await _orgCelinaRepo.GetByIdAsync(id);

        if (orgCelina is null)
        {
            throw new EntityNotFoundException("Ta organizaciona celina ne postoji");
        }

        await _orgCelinaRepo.DeleteAsync(id);
        await _orgCelinaRepo.SaveChangesAsync();
    }

    public async Task<IEnumerable<OrgCelinaDTO>> GetAllOrgCelinaAsync()
    {
        var listaOrgCelina = await _orgCelinaRepo.GetAllAsync();

        return _mapper.Map<IEnumerable<OrgCelinaDTO>>(listaOrgCelina);
    }

    public async Task<OrgCelinaMoreInfoDTO?> GetOrgCelinaByIdAsync(int id)
    {
        var orgCelina = await _orgCelinaRepo.GetOrgCelinaWithAdditionalInfoAsync(id);

        return _mapper.Map<OrgCelinaMoreInfoDTO>(orgCelina);
    }

    public async Task UpdateOrgCelinaAsync(UpdateOrgCelinaDTO updateOrgCelinaDto)
    {
        var orgCelina = await _orgCelinaRepo.GetByIdAsync(updateOrgCelinaDto.OrgCelinaId);

        if (orgCelina is null)
        {
            throw new EntityNotFoundException("Ta organizaciona celina ne postoji");
        }

        _mapper.Map(updateOrgCelinaDto, orgCelina);
        _orgCelinaRepo.Update(orgCelina);

        try
        {
            await _orgCelinaRepo.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            throw;
        }
    }
}
