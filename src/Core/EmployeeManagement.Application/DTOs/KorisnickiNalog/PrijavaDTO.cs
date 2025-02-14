using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Application.DTOs.KorisnickiNalog;

public class PrijavaDTO
{
    [Required(ErrorMessage = "Email je obavezan")]
    public string Email { get; set; } = String.Empty;

    [Required(ErrorMessage = "Lozinka je obavezna")]
    public string Lozinka { get; set; } = String.Empty;
}
