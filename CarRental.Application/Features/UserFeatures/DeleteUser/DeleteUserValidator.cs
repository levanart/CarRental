using CarRental.Application.Features.UserFeatures.CreateUser;
using FluentValidation;

namespace CarRental.Application.Features.UserFeatures.DeleteUser;

public sealed class DeleteUserValidator : AbstractValidator<CreateUserRequest>
{
    public DeleteUserValidator()
    {
        
    }
}