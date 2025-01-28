using AutoMapper;
using EmployeeManagementAPI.CustomExceptions;
using EmployeeManagementAPI.Data.Interfaces;
using EmployeeManagementAPI.DTOs.RadnoMesto;
using EmployeeManagementAPI.DTOs.TipZadatka;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Services;

public class RadnoMestoService : IRadnoMestoService
{
    private readonly IRadnoMestoRepository _radnoMestoRepo;
    private readonly IMapper _mapper;

    public RadnoMestoService(IRadnoMestoRepository radnoMestoRepo, IMapper mapper)
    {
        _radnoMestoRepo = radnoMestoRepo;
        _mapper = mapper;
    }

    public async Task<RadnoMestoDTO> AddRadnoMestoAsync(AddRadnoMestoDTO addRadnoMestoDto)
    {
        var radnoMesto = _mapper.Map<RadnoMesto>(addRadnoMestoDto);

        var novoRadnoMesto = await _radnoMestoRepo.AddAsync(radnoMesto);

        try
        {
            await _radnoMestoRepo.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            throw;
        }

        return _mapper.Map<RadnoMestoDTO>(novoRadnoMesto);
    }

    public async Task DeleteRadnoMestoAsync(int id)
    {
        var radnoMesto = await _radnoMestoRepo.GetByIdAsync(id);

        if (radnoMesto is null)
        {
            throw new EntityNotFoundException("To radno mesto ne postoji");
        }

        await _radnoMestoRepo.DeleteAsync(id);
        await _radnoMestoRepo.SaveChangesAsync();
    }

    public async Task<IEnumerable<RadnoMestoDTO>> GetAllRadnoMestoAsync()
    {
        var listaRadnihMesta = await _radnoMestoRepo.GetAllAsync();

        return _mapper.Map<IEnumerable<RadnoMestoDTO>>(listaRadnihMesta);
    }

    public async Task<RadnoMestoMoreInfoDTO?> GetRadnoMestoByIdAsync(int id)
    {
        var radnoMesto = await _radnoMestoRepo.GetRadnoMestoWithAdditionalInfoAsync(id);

        return _mapper.Map<RadnoMestoMoreInfoDTO>(radnoMesto);
    }

    public async Task UpdateRadnoMestoAsync(UpdateRadnoMestoDTO updateRadnoMestoDto)
    {
        var radnoMesto = await _radnoMestoRepo.GetByIdAsync(updateRadnoMestoDto.RadnoMestoId);

        if (radnoMesto is null)
        {
            throw new EntityNotFoundException("To radno mesto ne postoji");
        }

        _mapper.Map(updateRadnoMestoDto, radnoMesto);
        _radnoMestoRepo.Update(radnoMesto);

        try
        {
            await _radnoMestoRepo.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            throw;
        }
    }

    public async Task AddTipZadatkaForRadnoMestoAsync(int id, AddTipZadatkaDTO tipZadatkaDTO)
    {
        var radnoMesto = await _radnoMestoRepo.GetByIdAsync(id);

        if(radnoMesto == null)
        {
            throw new EntityNotFoundException("To radno mesto ne postoji");
        }

        var tipZad = _mapper.Map<TipZadatka>(tipZadatkaDTO);

        radnoMesto.AddTipZadatka(tipZad);

        await _radnoMestoRepo.SaveChangesAsync();
    }

    public async Task UpdateTipZadatkaForRadnoMestoAsync(int id, int tipZadatkaId, UpdateTipZadatkaDTO updateTipZadatkaDTO)
    {
        var radnoMesto = await _radnoMestoRepo.GetRadnoMestoWithAdditionalInfoAsync(id);

        if (radnoMesto is null)
        {
            throw new EntityNotFoundException("To radno mesto ne postoji");
        }

        var tipZad = radnoMesto.TipoviZadataka.FirstOrDefault(z => z.ZadatakId == tipZadatkaId);

        if(tipZad is null)
        {
            throw new EntityNotFoundException("Taj tip zadatka ne postoji");
        }

        var noviZad = _mapper.Map<TipZadatka>(updateTipZadatkaDTO);

        radnoMesto.UpdateTipZadatka(tipZad, noviZad);

        await _radnoMestoRepo.SaveChangesAsync();
    }

    public async Task DeleteTipZadatkaForRadnoMestoAsync(int id, int tipZadatkaId)
    {
        var radnoMesto = await _radnoMestoRepo.GetRadnoMestoWithAdditionalInfoAsync(id);

        if (radnoMesto is null)
        {
            throw new EntityNotFoundException("To radno mesto ne postoji");
        }

        var tipZad = radnoMesto.TipoviZadataka.FirstOrDefault(z => z.ZadatakId == tipZadatkaId);

        if (tipZad is null)
        {
            throw new EntityNotFoundException("Taj tip zadatka ne postoji");
        }

        radnoMesto.DeleteTipZadatka(tipZad);

        await _radnoMestoRepo.SaveChangesAsync();
    }

    public async Task<RadnoMestoDTO> GetRadnoMestoByTipZadatkaIdAsync(int zadatakId)
    {
        var radnoMesto = await _radnoMestoRepo.GetRadnoMestoByTipZadatkaIdAsync(zadatakId);

        return _mapper.Map<RadnoMestoDTO>(radnoMesto);
    }
}
