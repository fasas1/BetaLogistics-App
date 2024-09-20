using BetaLogistics.Data;
using BetaLogistics.Models;
using BetaLogistics.Repository.IRepository;

namespace BetaLogistics.Repository
{
    public class DriverRepository :Repository<Driver>, IDriverRepository
    {
        private readonly ApplicationDbContext _db;


        public DriverRepository(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }
        public async Task<Driver> UpdateAsync(Driver entity)
        {
            // entity.UpdatedDate = DateTime.Now;
            _db.Drivers.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

    }
}
