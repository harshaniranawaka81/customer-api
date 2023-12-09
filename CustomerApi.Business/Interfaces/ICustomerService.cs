using CustomerApi.Business.Models;
using CustomerApi.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CustomerApi.Business.Interfaces
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
