namespace EmployeeManagement.Application.DTOs.KorisnickiNalog;

public class AutentikacioniOdgovor
{
    public string? Email { get; set; } = string.Empty;
    public string? JwtToken { get; set; } = string.Empty;
    public DateTime VremeIstekaJwtTokena { get; set; }
    public string? RefreshToken { get; set; } = string.Empty;
    public DateTime VremeIstekaRefreshTokena { get; set; }
}
