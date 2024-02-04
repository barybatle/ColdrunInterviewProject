using FluentValidation;

namespace Trucks.Application.Validators;

public class GetAllTrucksOptionsValidator : AbstractValidator<GetAllTrucksOptions>
{
    private static readonly string[] AcceptableSortFields =
    {
        "code", "name", "status",
    };
    public GetAllTrucksOptionsValidator()
    {
        RuleFor(options => options.Status)
            .IsInEnum();

        RuleFor(options => options.SortField)
            .Must(sortField => sortField is null || AcceptableSortFields.Contains(sortField, StringComparer.OrdinalIgnoreCase))
            .WithMessage("You can only sort by 'code', 'name' or 'status'");
    }
}
