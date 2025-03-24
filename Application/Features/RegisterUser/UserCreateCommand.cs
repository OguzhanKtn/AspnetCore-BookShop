using Domain.Entities;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TS.Result;

namespace Application.Features.RegisterUser;

public sealed record UserCreateCommand
(
   string FirstName,
   string LastName,
   string UserName,
   string Email,
   string Password,
   Address? Address
) : IRequest<Result<string>>;

public sealed class UserCreateCommandValidator : AbstractValidator<UserCreateCommand>
{
    public UserCreateCommandValidator()
    {
        RuleFor(x => x.FirstName).MinimumLength(3).WithMessage("FirstName must be at least 3 characters");
        RuleFor(x => x.LastName).MinimumLength(3).WithMessage("LastName must be at least 3 characters");
        RuleFor(x => x.UserName).MinimumLength(3).WithMessage("UserName must be at least 3 characters");
        RuleFor(x => x.Email).EmailAddress().WithMessage("Please enter a valid email address");
        RuleFor(x => x.Password).MinimumLength(6).WithMessage("Password must be at least 6 characters");
    }
}

internal sealed class UserCreateCommandHandler(UserManager<AppUser> userManager) : IRequestHandler<UserCreateCommand, Result<string>>
{
    public async Task<Result<string>> Handle(UserCreateCommand request, CancellationToken cancellationToken)
    {
        var isUserExist = userManager.Users.Any(p => p.Email == request.Email);

        if (isUserExist)
        {
            return Result<string>.Failure(400,"Kullanıcı daha önce kayıt olmuştur!");
        }

        AppUser user = request.Adapt<AppUser>();

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return Result<string>.Failure(500,result.Errors.Select(e => e.Description).ToList());
        }

        
        var roleResult = await userManager.AddToRoleAsync(user, "User");

        if (!roleResult.Succeeded)
        {
            return Result<string>.Failure(roleResult.Errors.Select(e => e.Description).ToList());
        }

        return Result<string>.Succeed("User created successfully!");
    }
}
