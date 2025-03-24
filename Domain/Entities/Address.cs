namespace Domain.Entities;

public sealed record Address
{
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? Town { get; set; }
    public string? FullAddress { get; set; }

    public Guid UserId { get; set; }
    public AppUser User { get; set; } = default!;
}
