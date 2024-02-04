using FluentValidation;

namespace Trucks.Application.Validators;

public class TruckValidator : AbstractValidator<Truck>
{
    private readonly ITrucksRepository _trucksRepository;
    public TruckValidator(ITrucksRepository trucksRepository)
    {
        _trucksRepository = trucksRepository;

        RuleFor(truck => truck.Code)
            .MustAsync(ValidateCode)
            .NotEmpty();

        RuleFor(truck => truck.Name)
            .NotEmpty();

        RuleFor(truck => truck.Status)
            .NotNull()
            .IsInEnum();
    }

    private async Task<bool> ValidateCode(Truck truck, string code, CancellationToken token)
    {
        var existingTruck = await _trucksRepository.GetByCodeAsync(code);

        return existingTruck is null;
    }
}
