using AutoMapper;
using EmployeeManagement.Application.DTOs.OrganizacionaCelina;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Application.ServiceInterfaces;
using EmployeeManagement.Domain.RepositoryInterfaces;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.Domain.CustomExceptions;

namespace EmployeeManagement.Application.Services;

public class OrgCelinaService : IOrgCelinaService
{
    private readonly IOrgCelinaRepository _orgCelinaRepo;
    private readonly IMapper _mapper;

    public OrgCelinaService(IOrgCelinaRepository orgCelinaRepo, IMapper mapper)
    {
        _orgCelinaRepo = orgCelinaRepo;
        _mapper = mapper;
    }

    public async Task<OrgCelinaDTO> DodajOrgCelinuAsync(DodajOrgCelinuDTO dodajOrgCelinuDto)
    {
        var orgCelina = _mapper.Map<OrganizacionaCelina>(dodajOrgCelinuDto);

        var novaOrgCelina = await _orgCelinaRepo.DodajAsync(orgCelina);

        try
        {
            await _orgCelinaRepo.SacuvajPromeneAsync();
        }
        catch (DbUpdateException)
        {
            throw;
        }

        return _mapper.Map<OrgCelinaDTO>(novaOrgCelina);
    }

    public async Task ObrisiOrgCelinuAsync(int id)
    {
        var orgCelina = await _orgCelinaRepo.VratiPoIdAsync(id);

        if (orgCelina is null)
        {
            throw new EntityNotFoundException("Ta organizaciona celina ne postoji");
        }

        await _orgCelinaRepo.ObrisiAsync(id);
        await _orgCelinaRepo.SacuvajPromeneAsync();
    }

    public async Task<IEnumerable<OrgCelinaDTO>> VratiSveOrgCelineAsync()
    {
        var listaOrgCelina = await _orgCelinaRepo.VratiSveAsync();

        return _mapper.Map<IEnumerable<OrgCelinaDTO>>(listaOrgCelina);
    }

    public async Task<OrgCelinaDetaljnoDTO?> VratiOrgCelinuPoIdAsync(int id)
    {
        var orgCelina = await _orgCelinaRepo.VratiOrgCelinuSaDetaljimaAsync(id);

        return _mapper.Map<OrgCelinaDetaljnoDTO>(orgCelina);
    }

    public async Task AzurirajOrgCelinuAsync(AzurirajOrgCelinuDTO azurirajOrgCelinuDto)
    {
        var orgCelina = await _orgCelinaRepo.VratiPoIdAsync(azurirajOrgCelinuDto.OrgCelinaId);

        if (orgCelina is null)
        {
            throw new EntityNotFoundException("Ta organizaciona celina ne postoji");
        }

        _mapper.Map(azurirajOrgCelinuDto, orgCelina);
        _orgCelinaRepo.Azuriraj(orgCelina);

        try
        {
            await _orgCelinaRepo.SacuvajPromeneAsync();
        }
        catch (DbUpdateException)
        {
            throw;
        }
    }
}
