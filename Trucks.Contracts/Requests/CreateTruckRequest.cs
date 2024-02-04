namespace Trucks.Contracts.Requests;

public class CreateTruckRequest
{
    public required string Code { get; init; }

    public required string Name { get; init; }

    public required TruckStatus Status { get; init; }

    public string? Description { get; set; }
}
