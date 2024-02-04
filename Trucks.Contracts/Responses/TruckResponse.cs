namespace Trucks.Contracts.Responses;

public class TruckResponse
{
    public required string Code { get; init; }

    public required string Name { get; init; }

    public required TruckStatus Status { get; init; }

    public string? Description { get; init; }
}
