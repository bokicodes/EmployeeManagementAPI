﻿namespace EmployeeManagement.Application.DTOs.Zadatak;

public class ZadatakDTO
{
    public int ZadatakId { get; set; }
    public int RadnoMestoId { get; set; }
    public string NazivZad { get; set; }
    public string OpisZad { get; set; }
    public string TipZadatka { get; set; }
}
