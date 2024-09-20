using BetaLogistics.Models;

namespace BetaLogistics.Repository.IRepository
{
    public interface IDriverRepository: IRepository<Driver>
    {
        Task<Driver> UpdateAsync(Driver entity);
    }
}
