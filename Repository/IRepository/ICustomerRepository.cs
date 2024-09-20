using BetaLogistics.Models;

namespace BetaLogistics.Repository.IRepository
{
    public interface ICustomerRepository: IRepository<Customer>
    {
        Task<Customer> UpdateAsync(Customer entity);
    }
}
