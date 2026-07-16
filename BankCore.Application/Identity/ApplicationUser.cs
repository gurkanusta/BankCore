using Microsoft.AspNetCore.Identity;

namespace BankCore.Application.Identity;

public class ApplicationUser : IdentityUser
{
    public Guid CustomerId { get; set; }
}