using Domain.Abstractions;

namespace Domain.Entities;

public sealed class Basket : Entity
{
    public Guid UserId { get; set; }
    public AppUser User { get; set; } = default!;
    public decimal Price { get; set; }
    public List<Book> Books { get; set; } = new();
}
