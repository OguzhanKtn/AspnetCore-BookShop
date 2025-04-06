using Domain.Abstractions;

namespace Domain.Entities;

public sealed class Order : Entity
{
    public Guid UserId { get; set; }
    public AppUser User { get; set; } = default!;
    public decimal OrderPrice { get; set; }
    public DateTime OrderDate { get; set; }
    public Guid BasketId { get; set; }
}
