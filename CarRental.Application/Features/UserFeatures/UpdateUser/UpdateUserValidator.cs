using FluentValidation;

namespace CarRental.Application.Features.UserFeatures.UpdateUser;

public sealed class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
    }
}