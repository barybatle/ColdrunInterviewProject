namespace Trucks.Application.Repositories;

public interface ITrucksRepository
{
    Task<bool> CreateAsync(Truck truck);

    Task<Truck?> GetByCodeAsync(string code);

    Task<bool> UpdateAsync(Truck truck);

    Task<bool> DeleteByCodeAsync(string code);

    Task<IEnumerable<Truck>> GetAllAsync(GetAllTrucksOptions getAllTrucksOptions);
}
