﻿using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Application.DTOs.DodeljenZadatak;

public class DodeliZadatakDTO
{
    [Required]
    public DateTime DatumZadavanja { get; set; }
    public DateTime? DatumZavrsetka { get; set; }
    [Required]
    public DateTime RokIzvrsenja { get; set; }
}
