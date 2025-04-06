using Domain.Abstractions;

namespace Domain.Entities;

public sealed class Address : Entity
{
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? Town { get; set; }
    public string? FullAddress { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}
