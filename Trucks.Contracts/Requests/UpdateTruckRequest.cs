namespace Trucks.Contracts.Requests;

public class UpdateTruckRequest
{
    public required string Name { get; init; }

    public required TruckStatus Status { get; init; }

    public string? Description { get; init; }
}
