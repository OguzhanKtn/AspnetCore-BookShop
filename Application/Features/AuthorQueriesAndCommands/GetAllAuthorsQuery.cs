using Application.Interfaces;
using Mapster;
using MediatR;
using TS.Result;

namespace Application.Features.AuthorQueriesAndCommands;

public sealed record GetAllAuthorsQuery() : IRequest<Result<IQueryable<GetAllAuthorsQueryResponse>>>;

public sealed record GetAllAuthorsQueryResponse
(
   Guid Id,
   string Name,
   string SurName,
   string ImageUrl
);

internal sealed class GetAllAuthorsQueryHandler(IAuthorRepository authorRepository) : IRequestHandler<GetAllAuthorsQuery, Result<IQueryable<GetAllAuthorsQueryResponse>>>
{

    Task<Result<IQueryable<GetAllAuthorsQueryResponse>>> IRequestHandler<GetAllAuthorsQuery, Result<IQueryable<GetAllAuthorsQueryResponse>>>.Handle(GetAllAuthorsQuery request, CancellationToken cancellationToken)
    {
        var authors = authorRepository.GetAll();

        if (authors is null || !authors.Any())
        {
            return Task.FromResult(Result<IQueryable<GetAllAuthorsQueryResponse>>.Failure(500,"There is no any author!"));
        }

        var response = authors.ProjectToType<GetAllAuthorsQueryResponse>();

        return Task.FromResult(Result<IQueryable<GetAllAuthorsQueryResponse>>.Succeed(response));
    }
}
