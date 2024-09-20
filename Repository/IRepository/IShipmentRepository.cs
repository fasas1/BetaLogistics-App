using BetaLogistics.Models;

namespace BetaLogistics.Repository.IRepository
{
    public interface IShipmentRepository: IRepository<Shipment>
    {
        Task<Shipment> UpdateAsync(Shipment entity);
    }
}
