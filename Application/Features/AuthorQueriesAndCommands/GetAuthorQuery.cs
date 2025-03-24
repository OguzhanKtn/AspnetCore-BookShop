using Application.Interfaces;
using Domain.Entities;
using MediatR;
using TS.Result;

namespace Application.Features.AuthorQueriesAndCommands;

public sealed record GetAuthorQuery(Guid Id):IRequest<Result<Author>>;

internal sealed class GetAuthorQueryHandler(IAuthorRepository authorRepository) : IRequestHandler<GetAuthorQuery, Result<Author>>
{
    public async Task<Result<Author>> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
    {
        var author = await authorRepository.FirstOrDefaultAsync(p => p.Id == request.Id,cancellationToken);

        if(author is null)
        {
            return Result<Author>.Failure(500,"There is no any matched user!");
        }

        return author;
    }
}

