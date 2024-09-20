using BetaLogistics.Data;
using BetaLogistics.Models;
using BetaLogistics.Repository.IRepository;

namespace BetaLogistics.Repository
{
    public class ShipmentRepository :Repository<Shipment>, IShipmentRepository
    {
        private readonly ApplicationDbContext _db;


        public ShipmentRepository(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }
        public async Task<Shipment> UpdateAsync(Shipment entity)
        {
            entity.EstimatedDelivery = DateTime.Now;
            _db.Shipments.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

    }
}
