using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.Application.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public string? PersonName { get; set; }
}
