using Application.Features.Auth.Common;
using Domain.Abstractions.Result;
using Infrastructure.Abstractions.Interfaces.Auth;
using MediatR;

namespace Application.Features.Auth.Refresh;

public class RefreshTokenQueryHandler : IRequestHandler<RefreshTokenQuery, Result<AuthResponse>>
{
    private readonly IJwtGenerator _jwtGenerator;

    public RefreshTokenQueryHandler(
        IJwtGenerator jwtGenerator)
    {
        _jwtGenerator = jwtGenerator;
    }

    public async Task<Result<AuthResponse>> Handle(RefreshTokenQuery request, CancellationToken ct)
    {
        throw new NotImplementedException();

        // return Result<AuthResponse>.Success(new AuthResponse(
        //     JwtToken: "token"
        // ));
    }
}