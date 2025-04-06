using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public sealed class AppUser : IdentityUser<Guid>
{
    public AppUser()
    {
        Id = Guid.NewGuid();
    }

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string FullName => $"{FirstName} {LastName}";

    public List<Address>? Addresses { get; set; } = new();
    public List<Comment>? Comments { get; set; } = new() ;
    public List<Order>? Orders { get; set; } = new();
}
