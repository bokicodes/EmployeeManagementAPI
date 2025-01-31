using AutoMapper;
using EmployeeManagement.Business.CustomExceptions;
using EmployeeManagement.Business.DTOs.RadnoMesto;
using EmployeeManagement.Business.DTOs.TipZadatka;
using EmployeeManagement.Business.ServiceInterfaces;
using EmployeeManagement.Infrastructure.Models;
using EmployeeManagement.Infrastructure.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Business.Services;

public class RadnoMestoService : IRadnoMestoService
{
    private readonly IRadnoMestoRepository _radnoMestoRepo;
    private readonly IMapper _mapper;

    public RadnoMestoService(IRadnoMestoRepository radnoMestoRepo, IMapper mapper)
    {
        _radnoMestoRepo = radnoMestoRepo;
        _mapper = mapper;
    }

    public async Task<RadnoMestoDTO> DodajRadnoMestoAsync(DodajRadnoMestoDTO dodajRadnoMestoDto)
    {
        var radnoMesto = _mapper.Map<RadnoMesto>(dodajRadnoMestoDto);

        var novoRadnoMesto = await _radnoMestoRepo.DodajAsync(radnoMesto);

        try
        {
            await _radnoMestoRepo.SacuvajPromeneAsync();
        }
        catch (DbUpdateException)
        {
            throw;
        }

        return _mapper.Map<RadnoMestoDTO>(novoRadnoMesto);
    }

    public async Task ObrisiRadnoMestoAsync(int id)
    {
        var radnoMesto = await _radnoMestoRepo.VratiPoIdAsync(id);

        if (radnoMesto is null)
        {
            throw new EntityNotFoundException("To radno mesto ne postoji");
        }

        await _radnoMestoRepo.ObrisiAsync(id);
        await _radnoMestoRepo.SacuvajPromeneAsync();
    }

    public async Task<IEnumerable<RadnoMestoDTO>> VratiSvaRadnaMestaAsync()
    {
        var listaRadnihMesta = await _radnoMestoRepo.VratiSveAsync();

        return _mapper.Map<IEnumerable<RadnoMestoDTO>>(listaRadnihMesta);
    }

    public async Task<RadnoMestoDetaljnoDTO?> VratiRadnoMestoPoIdAsync(int id)
    {
        var radnoMesto = await _radnoMestoRepo.VratiRadnoMestoSaDetaljimaAsync(id);

        return _mapper.Map<RadnoMestoDetaljnoDTO>(radnoMesto);
    }

    public async Task AzurirajRadnoMestoAsync(AzurirajRadnoMestoDTO azurirajRadnoMestoDto)
    {
        var radnoMesto = await _radnoMestoRepo.VratiPoIdAsync(azurirajRadnoMestoDto.RadnoMestoId);

        if (radnoMesto is null)
        {
            throw new EntityNotFoundException("To radno mesto ne postoji");
        }

        _mapper.Map(azurirajRadnoMestoDto, radnoMesto);
        _radnoMestoRepo.Azuriraj(radnoMesto);

        try
        {
            await _radnoMestoRepo.SacuvajPromeneAsync();
        }
        catch (DbUpdateException)
        {
            throw;
        }
    }

    public async Task DodajTipZadatkaZaRadnoMestoAsync(int id, DodajTipZadatkaDTO tipZadatkaDTO)
    {
        var radnoMesto = await _radnoMestoRepo.VratiPoIdAsync(id);

        if(radnoMesto == null)
        {
            throw new EntityNotFoundException("To radno mesto ne postoji");
        }

        var tipZad = _mapper.Map<TipZadatka>(tipZadatkaDTO);

        radnoMesto.DodajTipZadatka(tipZad);

        await _radnoMestoRepo.SacuvajPromeneAsync();
    }

    public async Task AzurirajTipZadatkaZaRadnoMestoAsync(int id, int tipZadatkaId, AzurirajTipZadatkaDTO azurirajTipZadatkaDTO)
    {
        var radnoMesto = await _radnoMestoRepo.VratiRadnoMestoSaDetaljimaAsync(id);

        if (radnoMesto is null)
        {
            throw new EntityNotFoundException("To radno mesto ne postoji");
        }

        var tipZad = radnoMesto.TipoviZadataka.FirstOrDefault(z => z.ZadatakId == tipZadatkaId);

        if(tipZad is null)
        {
            throw new EntityNotFoundException("Taj tip zadatka ne postoji");
        }

        var noviZad = _mapper.Map<TipZadatka>(azurirajTipZadatkaDTO);

        radnoMesto.AzurirajTipZadatka(tipZad, noviZad);

        await _radnoMestoRepo.SacuvajPromeneAsync();
    }

    public async Task ObrisiTipZadatkaZaRadnoMestoAsync(int id, int tipZadatkaId)
    {
        var radnoMesto = await _radnoMestoRepo.VratiRadnoMestoSaDetaljimaAsync(id);

        if (radnoMesto is null)
        {
            throw new EntityNotFoundException("To radno mesto ne postoji");
        }

        var tipZad = radnoMesto.TipoviZadataka.FirstOrDefault(z => z.ZadatakId == tipZadatkaId);

        if (tipZad is null)
        {
            throw new EntityNotFoundException("Taj tip zadatka ne postoji");
        }

        radnoMesto.ObrisiTipZadatka(tipZad);

        await _radnoMestoRepo.SacuvajPromeneAsync();
    }

    public async Task<RadnoMestoDTO> VratiRadnoMestoPoTipuZadatkaIdAsync(int zadatakId)
    {
        var radnoMesto = await _radnoMestoRepo.VratiRadnoMestoPoTipuZadatkaIdAsync(zadatakId);

        return _mapper.Map<RadnoMestoDTO>(radnoMesto);
    }
}
