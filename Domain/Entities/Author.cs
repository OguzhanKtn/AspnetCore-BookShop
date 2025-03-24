using Domain.Abstractions;

namespace Domain.Entities;

public sealed class Author : Entity 
{
    public string Name { get; set; } = default!;
    public string SurName { get; set; } = default!;
    public string Authobiography { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public List<Book>? Books { get; set; } = new();
}
