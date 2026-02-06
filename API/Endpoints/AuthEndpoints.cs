using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Features.Auth.Login;

namespace API.Endpoints;

public static class AuthEndpoints
{
    public static IEndpointRouteBuilder MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth")
            .WithTags("Аутентификация");
            // .MapSwagger();

        group.MapPost("/login", LoginAsync)
            .WithName("Login")
            .WithSummary("Вход в систему")
            .WithDescription("Позволяет пользователю войти в систему, предоставив имя пользователя и пароль. В случае успешной аутентификации возвращает JWT-токен для доступа к защищенным ресурсам API.")
            .Accepts<LoginUserCommand>("application/json")
            .Produces<AuthResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        return group;
    }
    private static async Task<IResult> LoginAsync(
        [FromServices] IMediator _mediator,
        [FromBody] LoginUserCommand command
    )
    {
        var result = await _mediator.Send(command);

        return result.ToApiResult();
    }
}