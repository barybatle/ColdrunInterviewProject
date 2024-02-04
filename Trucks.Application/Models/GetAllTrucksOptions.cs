namespace Trucks.Application.Models;

public class GetAllTrucksOptions
{
    public required string? Name { get; init; }

    public required TruckStatus? Status { get; init; }

    public required string? Description { get; init; }

    public string? SortField { get; init; }

    public SortOrder? SortOrder { get; init; }
}
