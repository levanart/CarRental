using FluentValidation;

namespace CarRental.Application.Features.CarFeatures.UpdateCar;

public sealed class UpdateCarValidator : AbstractValidator<UpdateCarRequest>
{
    public UpdateCarValidator()
    {
        RuleFor(x => x.Power).GreaterThan(0);
        RuleFor(x => x.Mileage).GreaterThanOrEqualTo(0);
        RuleFor(x => x.ReleaseYear).InclusiveBetween(2000, DateTime.Now.Year);
    }
}