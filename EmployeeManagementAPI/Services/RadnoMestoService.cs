using AutoMapper;
using EmployeeManagementAPI.CustomExceptions;
using EmployeeManagementAPI.Data.Interfaces;
using EmployeeManagementAPI.DTOs.RadnoMesto;
using EmployeeManagementAPI.DTOs.Zaposleni;
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
}
