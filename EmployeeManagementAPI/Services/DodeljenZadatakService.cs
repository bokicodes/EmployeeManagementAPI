using AutoMapper;
using EmployeeManagementAPI.CustomExceptions;
using EmployeeManagementAPI.Data.Interfaces;
using EmployeeManagementAPI.DTOs.DodeljenZadatak;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EmployeeManagementAPI.Services;

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

    public async Task<DodeljenZadatakDTO> AddDodeljenZadatakAsync(int zaposleniId,
        int zadatakId, AddDodeljenZadatakDTO addDodeljenZadatakDto)
    {
        await ProveraPostojanjaEntitetaAsync(zaposleniId, zadatakId);

        if((await _dodeljenZadatakRepo.GetAllAsync(zaposleniId))
            .Any(dz => dz.ZadatakId == zadatakId))
        {
            throw new InvalidOperationException("Taj zadatak je vec dodeljen tom zaposlenom");
        }

        var dodeljenZadatak = _mapper.Map<DodeljenZadatak>(addDodeljenZadatakDto);
        
        dodeljenZadatak.ZaposleniId = zaposleniId;
        dodeljenZadatak.ZadatakId = zadatakId;
        dodeljenZadatak.RadnoMestoId = (await _radnoMestoRepo
            .GetRadnoMestoByTipZadatkaIdAsync(zadatakId))!.RadnoMestoId;

        var noviDodeljenZadatak = await _dodeljenZadatakRepo.AddAsync(dodeljenZadatak);

        try
        {
            await _dodeljenZadatakRepo.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            throw;
        }

        return _mapper.Map<DodeljenZadatakDTO>(noviDodeljenZadatak);
    }

    public async Task DeleteDodeljenZadatakAsync(int zaposleniId, int zadatakId)
    {
        await ProveraPostojanjaEntitetaAsync(zaposleniId, zadatakId);

        var dodeljenZadatak = await _dodeljenZadatakRepo.GetByIdsAsync(zaposleniId, zadatakId);

        if (dodeljenZadatak is null)
        {
            throw new EntityNotFoundException("Taj zadatak nije dodeljen tom zaposlenom");
        }

        await _dodeljenZadatakRepo.DeleteAsync(zaposleniId, zadatakId);
        await _dodeljenZadatakRepo.SaveChangesAsync();
    }

    public async Task<IEnumerable<DodeljenZadatakDTO>> GetAllDodeljeniZadaciAsync(int zaposleniId)
    {
        if(await _zaposleniRepo.GetByIdAsync(zaposleniId) is null)
        {
            throw new EntityNotFoundException("Taj zaposleni ne postoji");
        }

        var listaDodeljenihZadataka = await _dodeljenZadatakRepo.GetAllAsync(zaposleniId);

        return _mapper.Map<IEnumerable<DodeljenZadatakDTO>>(listaDodeljenihZadataka);
    }

    public async Task<DodeljenZadatakDTO> GetDodeljenZadatakByIdsAsync(int zaposleniId, int zadatakId)
    {
        await ProveraPostojanjaEntitetaAsync(zaposleniId, zadatakId);

        var dodeljenZadatak = await _dodeljenZadatakRepo.GetByIdsAsync(zaposleniId, zadatakId);

        if (dodeljenZadatak is null)
        {
            throw new EntityNotFoundException("Taj zadatak nije dodeljen tom zaposlenom");
        }

        return _mapper.Map<DodeljenZadatakDTO>(dodeljenZadatak);
    }

    public async Task UpdateDodeljenZadatakAsync(int zaposleniId, int zadatakId,
        UpdateDodeljenZadatakDTO updateDodeljenZadatakDto)
    {
        await ProveraPostojanjaEntitetaAsync(zaposleniId, zadatakId);

        var dodeljenZadatak = await _dodeljenZadatakRepo.GetByIdsAsync(zaposleniId, zadatakId);

        if (dodeljenZadatak is null)
        {
            throw new EntityNotFoundException("Taj zadatak nije dodeljen tom zaposlenom");
        }

        _mapper.Map(updateDodeljenZadatakDto, dodeljenZadatak);
        _dodeljenZadatakRepo.Update(dodeljenZadatak);

        try
        {
            await _dodeljenZadatakRepo.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            throw;
        }
    }

    public async Task ProveraPostojanjaEntitetaAsync(int zaposleniId, int zadatakId)
    {
        if (await _zaposleniRepo.GetByIdAsync(zaposleniId) is null)
        {
            throw new EntityNotFoundException("Taj zaposleni ne postoji");
        }

        if (await _radnoMestoRepo.GetRadnoMestoByTipZadatkaIdAsync(zadatakId) is null)
        {
            throw new EntityNotFoundException("Taj tip zadatka ne postoji");
        }
    }
}
