using Application.Services;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace Application.Features.AuthCommand;

public sealed record LoginCommand(
    string UserNameOrEmail,
    string Password) : IRequest<Result<LoginCommandResponse>>;

public sealed record LoginCommandResponse
{
    public string AccessToken { get; set; } = default!;
}

internal sealed class LoginCommandHandler(
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager,
    IJwtProvider jwtProvider) : IRequestHandler<LoginCommand, Result<LoginCommandResponse>>
{
    public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        AppUser? user = await userManager.Users.FirstOrDefaultAsync(p => p.Email == request.UserNameOrEmail || p.UserName == request.UserNameOrEmail, cancellationToken);

        if (user is null)
        {
            return Result<LoginCommandResponse>.Failure("User cannot found!");
        }

        SignInResult signInResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, true);

        if (signInResult.IsLockedOut)
        {
            TimeSpan? timeSpan = user.LockoutEnd - DateTime.UtcNow;
            if (timeSpan is not null)
                return (500, $"Your account has been blocked for {Math.Ceiling(timeSpan.Value.TotalMinutes)} minutes due to 5 wrong attempts.");
            else
                return (500, "Your account has been blocked for 5 minutes due to 5 wrong attempts.");
        }

        if (signInResult.IsNotAllowed)
        {
            return (500, "Your email address is not confirmed!");
        }

        if (!signInResult.Succeeded)
        {
            return (500, "Email or password is wrong!");
        }

        var token = await jwtProvider.CreateTokenAsync(user,cancellationToken);

        var response = new LoginCommandResponse()
        {
            AccessToken = token
        };

        return response;
    }
}
