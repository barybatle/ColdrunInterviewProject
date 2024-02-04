namespace Trucks.Contracts.Responses;

public class TrucksResponse
{
    public required int Count { get; init; } // Mainly done for my testing etc
    public required IEnumerable<TruckResponse> Items { get; init; } = Enumerable.Empty<TruckResponse>();
}
