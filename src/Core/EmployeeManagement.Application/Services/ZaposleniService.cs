using AutoMapper;
using EmployeeManagement.Application.DTOs.Zaposleni;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Application.ServiceInterfaces;
using EmployeeManagement.Domain.RepositoryInterfaces;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.Domain.CustomExceptions;

namespace EmployeeManagement.Application.Services;

public class ZaposleniService : IZaposleniService
{
    private readonly IZaposleniRepository _zaposleniRepo;
    private readonly IMapper _mapper;

    public ZaposleniService(IZaposleniRepository zaposleniRepo, IMapper mapper)
    {
        _zaposleniRepo = zaposleniRepo;
        _mapper = mapper;
    }

    public async Task<ZaposleniDTO> DodajZaposlenogAsync(DodajZaposlenogDTO dodajZaposlenogDto)
    {
        var zaposleni = _mapper.Map<Zaposleni>(dodajZaposlenogDto);

        var noviZaposleni = await _zaposleniRepo.DodajAsync(zaposleni);

        try
        {
            await _zaposleniRepo.SacuvajPromeneAsync();
        }
        catch (DbUpdateException)
        {
            throw;
        }

        return _mapper.Map<ZaposleniDTO>(noviZaposleni);
    }

    public async Task ObrisiZaposlenogAsync(int id)
    {
        var zaposleni = await _zaposleniRepo.VratiPoIdAsync(id);

        if(zaposleni is null)
        {
            throw new EntityNotFoundException("Taj zaposleni ne postoji");
        }

        await _zaposleniRepo.ObrisiAsync(id);
        await _zaposleniRepo.SacuvajPromeneAsync();
    }

    public async Task<IEnumerable<ZaposleniDTO>> VratiSveZaposleneAsync()
    {
        var listaZaposlenih = await _zaposleniRepo.VratiSveAsync();

        return _mapper.Map<IEnumerable<ZaposleniDTO>>(listaZaposlenih);
    }

    public async Task<ZaposleniDTO> VratiZaposlenogPoIdAsync(int id)
    {
        var zaposleni = await _zaposleniRepo.VratiPoIdAsync(id);

        return _mapper.Map<ZaposleniDTO>(zaposleni);
    }

    public async Task<ZaposleniDetaljnoDTO?> VratiZaposlenogSaDetaljimaAsync(int id)
    {
        var zaposleni = await _zaposleniRepo.VratiZaposlenogSaDetaljimaAsync(id);

        return _mapper.Map<ZaposleniDetaljnoDTO>(zaposleni);
    }

    public async Task AzurirajZaposlenogAsync(AzurirajZaposlenogDTO azurirajZaposlenogDto)
    {
        var zaposleni = await _zaposleniRepo.VratiPoIdAsync(azurirajZaposlenogDto.ZaposleniId);

        if(zaposleni is null)
        {
            throw new EntityNotFoundException("Taj zaposleni ne postoji");
        }

        _mapper.Map(azurirajZaposlenogDto, zaposleni); 
        _zaposleniRepo.Azuriraj(zaposleni);

        try
        {
            await _zaposleniRepo.SacuvajPromeneAsync();
        }
        catch (DbUpdateException)
        {
            throw;
        }
    }
}
