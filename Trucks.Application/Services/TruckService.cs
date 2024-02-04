using FluentValidation;

namespace Trucks.Application.Services;

public class TruckService : ITruckService
{
    private readonly ITrucksRepository _trucksRepository;
    private readonly IValidator<Truck> _truckValidator;
    private readonly IValidator<GetAllTrucksOptions> _getAllTrucksOptionsValidator;

    public TruckService(ITrucksRepository trucksRepository,
                        IValidator<Truck> truckValidator,
                        IValidator<GetAllTrucksOptions> getAllTrucksOptionsValidator)
    {
        _trucksRepository = trucksRepository;
        _truckValidator = truckValidator;
        _getAllTrucksOptionsValidator = getAllTrucksOptionsValidator;
    }

    public async Task<IEnumerable<Truck>> GetTrucksAsync(GetAllTrucksOptions options)
    {
        await _getAllTrucksOptionsValidator.ValidateAndThrowAsync(options);

        return await _trucksRepository.GetAllAsync(options);
    }

    public async Task<Truck?> GetTruckByCodeAsync(string code)
    {
        return await _trucksRepository.GetByCodeAsync(code);
    }

    public async Task<bool> CreateTruckAsync(Truck truck)
    {
        await _truckValidator.ValidateAndThrowAsync(truck);

        return await _trucksRepository.CreateAsync(truck);
    }

    public async Task<bool> UpdateTruckAsync(Truck newTruck, string code)
    {
        var oldTruck = await _trucksRepository.GetByCodeAsync(code);

        if (oldTruck == null)
        {
            throw new KeyNotFoundException(); // I didn't bother to create custom exceptions
        }

        oldTruck.Name = newTruck.Name;
        oldTruck.Description = newTruck.Description;

        var isStatusUpdated = UpdateTruckStatus(oldTruck, newTruck.Status);

        if (!isStatusUpdated)
        {
            throw new InvalidOperationException();
        }

        await _truckValidator.ValidateAndThrowAsync(oldTruck);

        var isSuccess = await _trucksRepository.UpdateAsync(oldTruck);

        return isSuccess;
    }

    public async Task<bool> DeleteTruckAsync(string code)
    {
        return await _trucksRepository.DeleteByCodeAsync(code);
    }

    private bool UpdateTruckStatus(Truck truck, TruckStatus newStatus)
    {
        if (truck.Status == newStatus ||
            truck.Status == TruckStatus.OutOfService ||
            (truck.Status == TruckStatus.Returning && newStatus == TruckStatus.Loading) ||
            (int)newStatus - (int)truck.Status == 1) // This enforces so that statuses are only used in correct order
        {
            truck.Status = newStatus;
            return true;
        }

        if (newStatus == TruckStatus.OutOfService)
        {
            truck.Status = newStatus;
            return true;
        }

        return false;
    }
}
