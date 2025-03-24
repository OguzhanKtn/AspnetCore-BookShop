using Domain.Abstractions;

namespace Domain.Entities;

public sealed class Comment : Entity
{
    public string Content { get; set; } = default!;
    public int? ParentCommentId { get; set; }
    public Guid BookId { get; set; }
    public Book Book { get; set; } = default!;
    public Guid UserId { get; set; }
    public AppUser User { get; set; } = default!;

    public Comment? ParentComment { get; set; }
    public List<Comment>? Replies { get; set; } = new();
}
