namespace Trucks.Contracts.Requests;

public class GetAllTrucksRequest
{
    public required string? Name { get; init; }

    public required TruckStatus? Status { get; init; }

    public required string? Description { get; init; }

    public required string? SortBy { get; init; }
}
