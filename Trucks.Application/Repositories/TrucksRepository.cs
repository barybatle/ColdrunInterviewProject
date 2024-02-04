using System.Data;

namespace Trucks.Application.Repositories;

public class TrucksRepository : ITrucksRepository // This is just an example in-memory repo for my testing purposes
{
    private readonly List<Truck> _trucks = new();
    public Task<bool> CreateAsync(Truck truck)
    {
        if (_trucks.Any(existingTruck => existingTruck.Code == truck.Code))
        {
            throw new DuplicateNameException($"Truck with {truck.Code} already exists");
        }

        _trucks.Add(truck);
        return Task.FromResult(true);
    }

    public Task<Truck?> GetByCodeAsync(string code)
    {
        var truck = _trucks.SingleOrDefault(truck => truck.Code == code);
        return Task.FromResult(truck);
    }

    public Task<bool> UpdateAsync(Truck truck)
    {
        var truckIndex = _trucks.FindIndex(toBeUpdatedTruck => toBeUpdatedTruck.Code == truck.Code);

        if (truckIndex == -1)
        {
            return Task.FromResult(false);
        }

        _trucks[truckIndex] = truck;
        return Task.FromResult(true);
    }

    public Task<bool> DeleteByCodeAsync(string code)
    {
        var removedCount = _trucks.RemoveAll(truck => truck.Code == code);
        var truckRemoved = removedCount > 0;
        return Task.FromResult(truckRemoved);
    }

    public Task<IEnumerable<Truck>> GetAllAsync(GetAllTrucksOptions getAllTrucksOptions)
    {
        var filteredTrucks = _trucks
            .Where(truck =>
                (getAllTrucksOptions.Name == null || truck.Name.Contains(getAllTrucksOptions.Name, StringComparison.OrdinalIgnoreCase)) &&
                (getAllTrucksOptions.Status == null || truck.Status == getAllTrucksOptions.Status) &&
                (getAllTrucksOptions.Description == null || truck.Description?.Contains(getAllTrucksOptions.Description, StringComparison.OrdinalIgnoreCase) == true)
            );

        switch (getAllTrucksOptions?.SortField?.ToLower())
        {
            case "code":
                filteredTrucks = getAllTrucksOptions.SortOrder == SortOrder.Ascending
                    ? filteredTrucks.OrderBy(truck => truck.Code)
                    : filteredTrucks.OrderByDescending(truck => truck.Code);
                break;

            case "name":
                filteredTrucks = getAllTrucksOptions.SortOrder == SortOrder.Ascending
                    ? filteredTrucks.OrderBy(truck => truck.Name)
                    : filteredTrucks.OrderByDescending(truck => truck.Name);
                break;

            case "status":
                filteredTrucks = getAllTrucksOptions.SortOrder == SortOrder.Ascending
                    ? filteredTrucks.OrderBy(truck => truck.Status)
                    : filteredTrucks.OrderByDescending(truck => truck.Status);
                break;

            // I dont think ordering by description is necessary
        }

        return Task.FromResult(filteredTrucks);
    }
}
