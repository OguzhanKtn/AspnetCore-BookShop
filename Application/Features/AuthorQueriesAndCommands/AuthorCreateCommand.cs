using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Http;
using TS.Result;

namespace Application.Features.AuthorQueriesAndCommands;

public sealed record AuthorCreateCommand
(
    string Name,
    string SurName,
    string Authobiography,
    IFormFile Image
):IRequest<Result<string>>;

public sealed class AuthorCreateCommandValidator : AbstractValidator<AuthorCreateCommand>
{
    public AuthorCreateCommandValidator()
    {
        RuleFor(x => x.Name).MinimumLength(3).WithMessage("Name must be at least 3 characters");
        RuleFor(x => x.SurName).MinimumLength(3).WithMessage("SurName must be at least 3 characters");
    }
}

internal sealed class AuthorCreateCommandHandler(IAuthorRepository authorRepository, IAzureStorageService azureStorageService, IUnitOfWork unitOfWork) : IRequestHandler<AuthorCreateCommand, Result<string>>
{
    public async Task<Result<string>> Handle(AuthorCreateCommand request, CancellationToken cancellationToken)
    {
       var isAuthorExist = await authorRepository.AnyAsync(p => p.Name == request.Name && p.SurName == request.SurName);

        if (isAuthorExist) 
        {
            return Result<string>.Failure(400, "This author is already exist!");
        }

        var fileName = $"{Guid.NewGuid()}-{request.Image.FileName}";
        using var stream = request.Image.OpenReadStream();
        var imageUrl = await azureStorageService.UploadImageAsync(stream, fileName);

        var author = new Author
        {
            Name = request.Name,
            SurName = request.SurName,
            Authobiography = request.Authobiography,
            ImageUrl = imageUrl,
        };

        authorRepository.Add(author);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succeed("Author created successfully!");
    }
}