using BetaLogistics.Models;

namespace BetaLogistics.Repository.IRepository
{
    public interface ILocationUpdateRepository: IRepository<LocationUpdate>
    {
        Task<LocationUpdate> UpdateAsync(LocationUpdate entity);
    }
}
