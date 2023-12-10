
using CustomerApi.Domain.Models;

namespace CustomerApi.Domain.Interfaces
{    
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetCustomers();

        Task<Customer?> GetCustomer(int id);

        Task<bool> UpdateCustomer(int id, Customer customer);

        Task<Customer> SaveCustomer(Customer customer);

        Task<bool> DeleteCustomer(int id);
      
    }
}
