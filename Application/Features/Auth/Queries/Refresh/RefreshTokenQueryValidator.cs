using Domain.Abstractions.Result;
using FluentValidation;

namespace Application.Features.Auth.Refresh;

public class RefreshTokenQueryValidator : AbstractValidator<RefreshTokenQuery>
{
    public RefreshTokenQueryValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty().WithState(_ => ErrorCode.InvalidToken)
            .WithMessage("Поле {PropertyName} не может быть пустым");
    }
}
