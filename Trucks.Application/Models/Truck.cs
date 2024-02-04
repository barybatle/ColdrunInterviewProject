namespace Trucks.Application.Models;

public class Truck
{
    public required string Code { get; init; }

    public required string Name { get; set; }

    public required TruckStatus Status { get; set; }

    public string? Description { get; set; }
}
