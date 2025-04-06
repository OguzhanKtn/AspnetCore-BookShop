using Domain.Abstractions;

namespace Domain.Entities;

public sealed class Book : Entity
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public int Rating { get; set; }
    public decimal Price { get; set; }
    public int Pages { get; set; }
    public DateTime PublishYear { get; set; }
    public Guid AuthorId { get; set; }
    public Author Author { get; set; } = default!;
    public Guid PublisherId { get; set; }
    public Publisher Publisher { get; set; } = default!;

    public List<Comment>? Comments { get; set; } = new();
}
