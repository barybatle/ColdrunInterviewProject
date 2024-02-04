namespace Trucks.Application.Services;

public interface ITruckService
{
    Task<IEnumerable<Truck>> GetTrucksAsync(GetAllTrucksOptions options);
    Task<Truck?> GetTruckByCodeAsync(string code);
    Task<bool> CreateTruckAsync(Truck truck);
    Task<bool> UpdateTruckAsync(Truck newTruck, string code);
    Task<bool> DeleteTruckAsync(string code);
}
