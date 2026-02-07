using Application.Abstractions.Interfaces.Auth;
using Application.Features.Auth.Common;
using Domain.Abstractions.Result;
using Infrastructure.Abstractions.Interfaces.Auth;
using MediatR;

namespace Application.Features.Auth.Refresh;

public class RefreshTokenQueryHandler : IRequestHandler<RefreshTokenQuery, Result<AuthResponse>>
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly ICurrentUserService _currentUserService;

    public RefreshTokenQueryHandler(
        IJwtGenerator jwtGenerator,
        ICurrentUserService currentUserService
    )
    {
        _jwtGenerator = jwtGenerator;
        _currentUserService = currentUserService;
    }

    public async Task<Result<AuthResponse>> Handle(RefreshTokenQuery request, CancellationToken ct)
    {
        if (string.IsNullOrEmpty(_currentUserService.JwtToken))
            return Result.Failed(ErrorCode.InvalidToken);

        if (DateTime.UtcNow > _currentUserService.ExpiresAt)
            return Result.Failed(ErrorCode.TokenExpired);

        // var newJwtToken = _jwtGenerator.GenerateToken();
        throw new NotImplementedException();

        // return Result<AuthResponse>.Success(new AuthResponse(
        //     JwtToken: _currentUserService.JwtToken ?? string.Empty
        // ));
    }
}