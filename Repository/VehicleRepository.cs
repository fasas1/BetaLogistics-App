using BetaLogistics.Data;
using BetaLogistics.Models;
using BetaLogistics.Repository.IRepository;

namespace BetaLogistics.Repository
{
    public class VehicleRepository :Repository<Vehicle>, IVehicleRepository
    {
        private readonly ApplicationDbContext _db;


        public VehicleRepository(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }
        public async Task<Vehicle> UpdateAsync(Vehicle entity)
        {
            // entity.UpdatedDate = DateTime.Now;
            _db.Vehicles.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

    }
}
