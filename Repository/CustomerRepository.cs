using BetaLogistics.Data;
using BetaLogistics.Models;
using BetaLogistics.Repository.IRepository;

namespace BetaLogistics.Repository
{
    public class CustomerRepository :Repository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _db;


        public CustomerRepository(ApplicationDbContext db):base(db) 
        {
            _db = db;
        }
        public async Task<Customer> UpdateAsync(Customer entity)
        {
            // entity.UpdatedDate = DateTime.Now;
            _db.Customers.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }

    }
}
