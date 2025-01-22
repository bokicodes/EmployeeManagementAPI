using AutoMapper;
using EmployeeManagementAPI.CustomExceptions;
using EmployeeManagementAPI.Data.Interfaces;
using EmployeeManagementAPI.DTOs.Zaposleni;
using EmployeeManagementAPI.Models;
using EmployeeManagementAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementAPI.Services;

public class ZaposleniService : IZaposleniService
{
    private readonly IZaposleniRepository _zaposleniRepo;
    private readonly IMapper _mapper;

    public ZaposleniService(IZaposleniRepository zaposleniRepo, IMapper mapper)
    {
        _zaposleniRepo = zaposleniRepo;
        _mapper = mapper;
    }

    public async Task<ZaposleniDTO> AddZaposleniAsync(AddZaposleniDTO addZaposleniDto)
    {
        var zaposleni = _mapper.Map<Zaposleni>(addZaposleniDto);

        var noviZaposleni = await _zaposleniRepo.AddAsync(zaposleni);

        try
        {
            await _zaposleniRepo.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            throw;
        }

        return _mapper.Map<ZaposleniDTO>(noviZaposleni);
    }

    public async Task DeleteZaposleniAsync(int id)
    {
        var zaposleni = await _zaposleniRepo.GetByIdAsync(id);

        if(zaposleni is null)
        {
            throw new EntityNotFoundException("Taj zaposleni ne postoji");
        }

        await _zaposleniRepo.DeleteAsync(id);
        await _zaposleniRepo.SaveChangesAsync();
    }

    public async Task<IEnumerable<ZaposleniDTO>> GetAllZaposleniAsync()
    {
        var listaZaposlenih = await _zaposleniRepo.GetAllAsync();

        return _mapper.Map<IEnumerable<ZaposleniDTO>>(listaZaposlenih);
    }

    public async Task<ZaposleniDTO> GetZaposleniByIdAsync(int id)
    {
        var zaposleni = await _zaposleniRepo.GetByIdAsync(id);

        return _mapper.Map<ZaposleniDTO>(zaposleni);
    }

    public async Task<ZaposleniMoreInfoDTO?> GetZaposleniWithAdditionalInfo(int id)
    {
        var zaposleni = await _zaposleniRepo.GetZaposleniWithAdditionalInfoAsync(id);

        return _mapper.Map<ZaposleniMoreInfoDTO>(zaposleni);
    }

    public async Task UpdateZaposleniAsync(UpdateZaposleniDTO updateZaposleniDto)
    {
        var zaposleni = await _zaposleniRepo.GetByIdAsync(updateZaposleniDto.ZaposleniId);

        if(zaposleni is null)
        {
            throw new EntityNotFoundException("Taj zaposleni ne postoji");
        }

        _mapper.Map(updateZaposleniDto, zaposleni); 
        _zaposleniRepo.Update(zaposleni);

        try
        {
            await _zaposleniRepo.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            throw;
        }
    }
}
