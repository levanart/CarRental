using FluentValidation;

namespace CarRental.Application.Features.CarFeatures.CreateCar;

public sealed class CreateCarValidator : AbstractValidator<CreateCarRequest>
{
    public CreateCarValidator()
    {
        RuleFor(x => x.PlateNumber).NotEmpty();
        RuleFor(x => x.Brand).NotEmpty();
        RuleFor(x => x.Model).NotEmpty();
        RuleFor(x => x.Power).GreaterThan(0);
        RuleFor(x => x.Mileage).GreaterThanOrEqualTo(0);
        RuleFor(x => x.ReleaseYear).InclusiveBetween(2000, DateTime.Now.Year);
    }
}