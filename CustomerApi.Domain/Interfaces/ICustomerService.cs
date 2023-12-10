using CustomerApi.Domain.Models;
using System.Net;

namespace CustomerApi.Domain.Interfaces
{
    public interface ICustomerService
    {
        Task<KeyValuePair<HttpStatusCode, List<Customer>>> GetCustomers();

        Task<KeyValuePair<HttpStatusCode, Customer?>> GetCustomer(int id);

        Task<KeyValuePair<HttpStatusCode, bool>> EditCustomer(int id, Customer customer);

        Task<KeyValuePair<HttpStatusCode, Customer?>> SaveCustomer(Customer customer);

        Task<KeyValuePair<HttpStatusCode, bool>> DeleteCustomer(int id);
    }
}
