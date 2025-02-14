using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Application.DTOs.KorisnickiNalog;

public class RegistracijaDTO
{
    [Required(ErrorMessage = "Ime je obavezno")]
    public string ImeKorisnika { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email je obavezan")]
    [EmailAddress(ErrorMessage = "Nevazeca email adresa")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Lozinka je obavezna")]
    public string Lozinka { get; set; } = string.Empty;

    [Required(ErrorMessage = "Potvrda lozinke je obavezna")]
    [Compare("Lozinka", ErrorMessage = "Sifra i potvrda sifre su razliciti")]
    public string PotvrdaLozinke { get; set; } = string.Empty;
}
