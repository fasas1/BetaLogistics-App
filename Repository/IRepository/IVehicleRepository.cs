using BetaLogistics.Models;

namespace BetaLogistics.Repository.IRepository
{
    public interface IVehicleRepository: IRepository<Vehicle>
    {
        Task<Vehicle> UpdateAsync(Vehicle entity);
    }
}
