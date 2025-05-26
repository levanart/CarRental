using CarRental.Application.Features.CarFeatures.CreateCar;
using FluentValidation;

namespace CarRental.Application.Features.CarFeatures.DeleteCar;

public sealed class DeleteCarValidator : AbstractValidator<CreateCarRequest>
{
    public DeleteCarValidator()
    {
        
    }
}