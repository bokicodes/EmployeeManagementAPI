namespace EmployeeManagement.Application.DTOs.KorisnickiNalog;

public class AutentikacioniOdgovor
{
    public string? Email { get; set; } = string.Empty;
    public string? Token { get; set; } = string.Empty;
    public DateTime VremeIstekaTokena { get; set; }
}
