using Trucks.Application.Enums;

namespace Trucks.Api.Mapping;

public static class ContractMapping
{
    public static Truck MapToTruck(this CreateTruckRequest request)
    {
        return new Truck
        {
            Code = request.Code,
            Name = request.Name,
            Status = request.Status,
            Description = request.Description,
        };
    }

    public static TruckResponse MapToResponse(this Truck truck)
    {
        return new TruckResponse
        {
            Code = truck.Code,
            Name = truck.Name,
            Status = truck.Status,
            Description = truck.Description,
        };
    }

    public static TrucksResponse MapToResponse(this IEnumerable<Truck> trucks)
    {
        return new TrucksResponse
        {
            Count = trucks.Count(),
            Items = trucks.Select(truck => truck.MapToResponse()),
        };
    }

    public static Truck MapToTruck(this UpdateTruckRequest request, string code)
    {
        return new Truck
        {
            Code = code,
            Name = request.Name,
            Status = request.Status,
            Description = request.Description,
        };
    }

    public static GetAllTrucksOptions MapToOptions(this GetAllTrucksRequest request)
    {
        return new GetAllTrucksOptions
        {
            Name = request.Name,
            Status = request.Status,
            Description = request.Description,
            SortField = request.SortBy?.Trim('+', '-'),
            SortOrder = request.SortBy is null ? SortOrder.Unsorted :
                request.SortBy.StartsWith('-') ? SortOrder.Descending : SortOrder.Ascending,
        };
    }
}
