using BetaLogistics.Data;
using BetaLogistics.Models;
using BetaLogistics.Repository.IRepository;

namespace BetaLogistics.Repository
{
    public class LocationUpdateRepository :Repository<LocationUpdate>, ILocationUpdateRepository
    {
        private readonly ApplicationDbContext _db;


        public LocationUpdateRepository(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }
        public async Task<LocationUpdate> UpdateAsync(LocationUpdate entity)
        {
            // entity.UpdatedDate = DateTime.Now;
            _db.LocationUpdates.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

    }
}
