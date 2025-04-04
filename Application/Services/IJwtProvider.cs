﻿using Domain.Entities;

namespace Application.Services;

public interface IJwtProvider
{
    public Task<string> CreateTokenAsync(AppUser user,CancellationToken cancellationToken = default);
}
