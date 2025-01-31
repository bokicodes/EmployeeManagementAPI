using AutoMapper;
using EmployeeManagement.Business.CustomExceptions;
using EmployeeManagement.Business.DTOs.DodeljenZadatak;
using EmployeeManagement.Business.ServiceInterfaces;
using EmployeeManagement.Infrastructure.Models;
using EmployeeManagement.Infrastructure.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Business.Services;

public class DodeljenZadatakService : IDodeljenZadatakService
{
    private readonly IDodeljenZadatakRepository _dodeljenZadatakRepo;
    private readonly IZaposleniRepository _zaposleniRepo;
    private readonly IRadnoMestoRepository _radnoMestoRepo;
    private readonly IMapper _mapper;

    public DodeljenZadatakService(IDodeljenZadatakRepository dodeljenZadatakRepo,
        IZaposleniRepository zaposleniRepo, IRadnoMestoRepository radnoMestoRepo,
        IMapper mapper)
    {
        _dodeljenZadatakRepo = dodeljenZadatakRepo;
        _zaposleniRepo = zaposleniRepo;
        _radnoMestoRepo = radnoMestoRepo;
        _mapper = mapper;
    }

    public async Task<DodeljenZadatakDTO> DodeliZadatakAsync(int zaposleniId,
        int zadatakId, DodeliZadatakDTO dodeliZadatakDto)
    {
        await ProveraPostojanjaEntitetaAsync(zaposleniId, zadatakId);

        if ((await _dodeljenZadatakRepo.VratiSveAsync(zaposleniId))
            .Any(dz => dz.ZadatakId == zadatakId))
        {
            throw new InvalidOperationException("Taj zadatak je vec dodeljen tom zaposlenom");
        }

        var dodeljenZadatak = _mapper.Map<DodeljenZadatak>(dodeliZadatakDto);

        dodeljenZadatak.ZaposleniId = zaposleniId;
        dodeljenZadatak.ZadatakId = zadatakId;
        dodeljenZadatak.RadnoMestoId = (await _radnoMestoRepo
            .VratiRadnoMestoPoTipuZadatkaIdAsync(zadatakId))!.RadnoMestoId;

        var noviDodeljenZadatak = await _dodeljenZadatakRepo.DodajAsync(dodeljenZadatak);

        try
        {
            await _dodeljenZadatakRepo.SacuvajPromeneAsync();
        }
        catch (DbUpdateException)
        {
            throw;
        }

        return _mapper.Map<DodeljenZadatakDTO>(noviDodeljenZadatak);
    }

    public async Task ObrisiDodeljenZadatakAsync(int zaposleniId, int zadatakId)
    {
        await ProveraPostojanjaEntitetaAsync(zaposleniId, zadatakId);

        var dodeljenZadatak = await _dodeljenZadatakRepo.VratiPoIdsAsync(zaposleniId, zadatakId);

        if (dodeljenZadatak is null)
        {
            throw new EntityNotFoundException("Taj zadatak nije dodeljen tom zaposlenom");
        }

        await _dodeljenZadatakRepo.ObrisiAsync(zaposleniId, zadatakId);
        await _dodeljenZadatakRepo.SacuvajPromeneAsync();
    }

    public async Task<IEnumerable<DodeljenZadatakDTO>> VratiSveDodeljeneZadatkeAsync(int zaposleniId)
    {
        if (await _zaposleniRepo.VratiPoIdAsync(zaposleniId) is null)
        {
            throw new EntityNotFoundException("Taj zaposleni ne postoji");
        }

        var listaDodeljenihZadataka = await _dodeljenZadatakRepo.VratiSveAsync(zaposleniId);

        return _mapper.Map<IEnumerable<DodeljenZadatakDTO>>(listaDodeljenihZadataka);
    }

    public async Task<DodeljenZadatakDTO> VratiDodeljenZadatakPoIdsAsync(int zaposleniId, int zadatakId)
    {
        await ProveraPostojanjaEntitetaAsync(zaposleniId, zadatakId);

        var dodeljenZadatak = await _dodeljenZadatakRepo.VratiPoIdsAsync(zaposleniId, zadatakId);

        if (dodeljenZadatak is null)
        {
            throw new EntityNotFoundException("Taj zadatak nije dodeljen tom zaposlenom");
        }

        return _mapper.Map<DodeljenZadatakDTO>(dodeljenZadatak);
    }

    public async Task AzurirajDodeljenZadatakAsync(int zaposleniId, int zadatakId,
        AzurirajDodeljenZadatakDTO azurirajDodeljenZadatakDto)
    {
        await ProveraPostojanjaEntitetaAsync(zaposleniId, zadatakId);

        var dodeljenZadatak = await _dodeljenZadatakRepo.VratiPoIdsAsync(zaposleniId, zadatakId);

        if (dodeljenZadatak is null)
        {
            throw new EntityNotFoundException("Taj zadatak nije dodeljen tom zaposlenom");
        }

        _mapper.Map(azurirajDodeljenZadatakDto, dodeljenZadatak);
        _dodeljenZadatakRepo.Azuriraj(dodeljenZadatak);

        try
        {
            await _dodeljenZadatakRepo.SacuvajPromeneAsync();
        }
        catch (DbUpdateException)
        {
            throw;
        }
    }

    public async Task ProveraPostojanjaEntitetaAsync(int zaposleniId, int zadatakId)
    {
        if (await _zaposleniRepo.VratiPoIdAsync(zaposleniId) is null)
        {
            throw new EntityNotFoundException("Taj zaposleni ne postoji");
        }

        if (await _radnoMestoRepo.VratiRadnoMestoPoTipuZadatkaIdAsync(zadatakId) is null)
        {
            throw new EntityNotFoundException("Taj tip zadatka ne postoji");
        }
    }
}
