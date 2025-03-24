using Domain.Abstractions;

namespace Domain.Entities;

public sealed class Publisher : Entity
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public List<Book>? Books { get; set; } = new();
}
