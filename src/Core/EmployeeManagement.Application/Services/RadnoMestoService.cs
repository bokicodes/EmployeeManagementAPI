﻿using AutoMapper;
using EmployeeManagement.Application.DTOs.RadnoMesto;
using EmployeeManagement.Application.DTOs.Zadatak;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Application.ServiceInterfaces;
using EmployeeManagement.Domain.RepositoryInterfaces;
using EmployeeManagement.Domain.Models;
using EmployeeManagement.Domain.CustomExceptions;

namespace EmployeeManagement.Application.Services;

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

    public async Task DodajZadatakZaRadnoMestoAsync(int id, DodajZadatakDTO dodajZadatakDto)
    {
        var radnoMesto = await _radnoMestoRepo.VratiPoIdAsync(id);

        if(radnoMesto == null)
        {
            throw new EntityNotFoundException("To radno mesto ne postoji");
        }

        if(!Enum.TryParse(typeof(TipZadatka), dodajZadatakDto.TipZadatka, out _))
        {
            throw new ArgumentException("Ne postojeca vrednost za tip zadatka");    
        }

        var zad = _mapper.Map<Zadatak>(dodajZadatakDto);

        radnoMesto.DodajZadatak(zad);

        await _radnoMestoRepo.SacuvajPromeneAsync();
    }

    public async Task AzurirajZadatakZaRadnoMestoAsync(int id, int zadatakId, AzurirajZadatakDTO azurirajZadatakDto)
    {
        var radnoMesto = await _radnoMestoRepo.VratiRadnoMestoSaDetaljimaAsync(id);

        if (radnoMesto is null)
        {
            throw new EntityNotFoundException("To radno mesto ne postoji");
        }

        var zad = radnoMesto.Zadaci.FirstOrDefault(z => z.ZadatakId == zadatakId);

        if(zad is null)
        {
            throw new EntityNotFoundException("Taj zadatak ne postoji");
        }

        if(!Enum.TryParse(typeof(TipZadatka), azurirajZadatakDto.TipZadatka, out _))
        {
            throw new ArgumentException("Ne postojeca vrednost za tip zadatka");
        }

        var noviZad = _mapper.Map<Zadatak>(azurirajZadatakDto);

        radnoMesto.AzurirajZadatak(zad, noviZad);

        await _radnoMestoRepo.SacuvajPromeneAsync();
    }

    public async Task ObrisiZadatakZaRadnoMestoAsync(int id, int zadatakId)
    {
        var radnoMesto = await _radnoMestoRepo.VratiRadnoMestoSaDetaljimaAsync(id);

        if (radnoMesto is null)
        {
            throw new EntityNotFoundException("To radno mesto ne postoji");
        }

        var zad = radnoMesto.Zadaci.FirstOrDefault(z => z.ZadatakId == zadatakId);

        if (zad is null)
        {
            throw new EntityNotFoundException("Taj zadatak ne postoji");
        }

        radnoMesto.ObrisiZadatak(zad);

        await _radnoMestoRepo.SacuvajPromeneAsync();
    }

    public async Task<RadnoMestoDTO> VratiRadnoMestoPoZadatkuIdAsync(int zadatakId)
    {
        var radnoMesto = await _radnoMestoRepo.VratiRadnoMestoPoZadatkuIdAsync(zadatakId);

        return _mapper.Map<RadnoMestoDTO>(radnoMesto);
    }
}
