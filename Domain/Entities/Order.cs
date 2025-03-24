using Domain.Abstractions;

namespace Domain.Entities;

public sealed class Order : Entity
{
    public int UserId { get; set; }
    public AppUser User { get; set; } = default!;
    public decimal OrderPrice { get; set; }
    public DateTime OrderDate { get; set; }
    public int BasketId { get; set; }
    public Basket Basket { get; set; } = default!;
}
